module.exports.run = function (dirName) {
  const electron = require('electron')
  const os = require('os');
  const path = require('path')
  const url = require('url')

  // Module to control application life.
  const app = electron.app
  // Module to create native browser window.
  const BrowserWindow = electron.BrowserWindow

  // Keep a global reference of the window object, if you don't, the window will
  // be closed automatically when the JavaScript object is garbage collected.
  let mainWindow

  function createWindow() {
    // Create the browser window.
    mainWindow = new BrowserWindow({ width: 800, height: 600 })

    // and load the index.html of the app.
    mainWindow.loadURL(url.format({
      pathname: path.join(dirName, 'index.html'),
      protocol: 'file:',
      slashes: true
    }))

    // Open the DevTools.
    // mainWindow.webContents.openDevTools()

    // Emitted when the window is closed.
    mainWindow.on('closed', function () {
      // Dereference the window object, usually you would store windows
      // in an array if your app supports multi windows, this is the time
      // when you should delete the corresponding element.
      mainWindow = null
    })
  }

  // This method will be called when Electron has finished
  // initialization and is ready to create browser windows.
  // Some APIs can only be used after this event occurs.
  app.on('ready', startWebApp);
  app.on('ready', createWindow);

  // Quit when all windows are closed.
  app.on('window-all-closed', function () {
    // On OS X it is common for applications and their menu bar
    // to stay active until the user quits explicitly with Cmd + Q
    if (process.platform !== 'darwin') {
      app.quit()
    }
  })

  app.on('activate', function () {
    // On OS X it's common to re-create a window in the app when the
    // dock icon is clicked and there are no other windows open.
    if (mainWindow === null) {
      createWindow()
    }
  })


  // In this file you can include the rest of your app's specific main process
  // code. You can also put them in separate files and require them here.
  let webAppProcess;

  function startWebApp() {
    const freeport = require('freeport');

    freeport(function (err, port) {
      if (err) throw err
      console.log(port);

      spawnWebServer(port);
    });
  }

  function spawnWebServer(port) {
    var proc = require('child_process').spawn;

    var webAppFile = path.join(dirName, '/webapp/bin/dist/webapp');

    if (os.platform() == 'win32') {
      webAppFile += '.exe';
    }

    webAppProcess = proc(webAppFile,
      {
        cwd: path.dirname(webAppFile),
        env: { 'ASPNETCORE_URLS': `http://+:${port}` },
      }
    );

    console.log(`webapp started`);

    webAppProcess.stdout.on('data', (data) => {
      console.log(`webapp: ${data}`);

      if (data.includes('Application started. Press Ctrl+C to shut down.')) {
        if (mainWindow != null) {
          initializeConnection(port);
        }
      }
    });

    webAppProcess.stderr.on('data', (data) => {
      console.log(`webapp: ${data}`);
    });
  }


  //Kill process when electron exits
  process.on('exit', function () {
    console.log('killing webApp');
    if (webAppProcess) {
      webAppProcess.kill();
      webAppProcess = null;
    }
  });

  function initializeConnection(serverPort) {
    webSocketConnect(`ws://localhost:${serverPort}/ws`);

    console.log('redirecting to web page');

    var indexPageUrl = url.format({
      hostname: 'localhost',
      port: serverPort,
      protocol: 'http'
    });

    mainWindow.loadURL(indexPageUrl);
  }

  function webSocketConnect(url) {
    const WebSocket = require('ws');

    var ws = new WebSocket(url, {
      perMessageDeflate: false
    });

    ws.on('open', function open() {
      ws.send(JSON.stringify({ result: 'Connection opened', type: 'Event' }));
    });

    ws.on('close', function close(data) {
      console.log('Connection closed');
    })

    ws.on('message', function incoming(data) {
      console.log('Received: ' + data);

      var electronAction = JSON.parse(data);
      /* Method */
      if (electronAction.type == 0) {
        var electronModule;
        if (electronAction.module == 'mainWindow') {
          electronModule = mainWindow;
        }
        else {
          electronModule = electron[electronAction.module];
        }

        var result = electronModule[electronAction.method].apply(electronModule, electronAction.arguments);

        var response = {
          type: "Response",
          actionId: electronAction.id,
          result: result
        };

        console.log('Sending: ' + JSON.stringify(response));

        ws.send(JSON.stringify(response));

      }
      /* Event */
      else if (electronAction.type == 1) {
        if (electronAction.module == "app") {
          app.on(electronAction.method, (event) => {
            var response = {
              type: "Event",
              actionId: electronAction.id,
              result: event
            };

            ws.send(JSON.stringify(response));

            if (electronAction.usePreventDefault) {
              event.preventDefault();
            }
          });
        }


      }


    });
  }
}