# dotvvm-electron-sample

### Run sample app
```
$ dotnet publish -o bin/dist -r win10-x64 
$ npm start
```
For Visual Studio Code users:
+ Run **publish** task
+ Launch **Electron Main**

Replace runtime identifier (RID) with your operating system RID. For list of RID check [documentation](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog).

## Implemented API:
**Methods**
* **[Clipboard](https://electron.atom.io/docs/api/clipboard/)**
  * **Methods**
    * readText
    * writeText
    * readHTML
    * writeHTML
    * readRTF
    * writeRTP
    * readBookmark
    * writeBookmark
    * clear
* **[Dialog](https://electron.atom.io/docs/api/dialog/)**
  * **Methods**
    * showOpenDialog
    * showSaveDialog
    * showMessageBox
    * showErrorBox
* **[Shell](https://electron.atom.io/docs/api/shell/)**
  * **Methods**
    * showItemInFolder
    * openItem
    * openExternal
    * moveItemToTrash
* **[App](https://electron.atom.io/docs/api/app/)**
  * **Events**
    * before-quit
    * browser-window-focus

    
