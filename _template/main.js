var dotvvmElectron = require('dotvvm-electron');

var integration = dotvvmElectron.run(__dirname, {
    browserWindowCreated: function (window) {
        window.maximize();
    },
    relativeWebAppPath: 'C:/Users/adamj/source/repos/web/src/DotvvmWeb/DotvvmWeb.Docs.AspNetCore/bin/dist/DotvvmWeb.Docs.AspNetCore',
    indexPagePath: '/status'
});

const template = [
    {
        label: 'DotVVM',
        submenu: [
            {
                label: 'Learn More',
                click() { require('electron').shell.openExternal('https://dotvvm.com') }
            }
        ]
    },
    {
        label: 'View',
        submenu: [
            { role: 'reload' },
            { role: 'forcereload' },
            { role: 'toggledevtools' },
            { type: 'separator' },
            { role: 'resetzoom' },
            { role: 'zoomin' },
            { role: 'zoomout' },
            { type: 'separator' },
            { role: 'togglefullscreen' }
        ]
    }
]

const { Menu } = require('electron')

const menu = Menu.buildFromTemplate(template)
Menu.setApplicationMenu(menu)