using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Electron.Modules.Options;
using DotVVM.Electron.Services;
using DotVVM.Framework.ViewModel;


namespace WebApp.ViewModels
{
    public class DefaultViewModel : DotvvmViewModelBase
    {
        private ElectronService _electronService;
        public string ClipBoardReadTextProperty { get; set; }
        public ReadBookmarkOptions ReadBookMarkOptions { get; set; }
        public string Title { get; set; }
        public DefaultViewModel(ElectronService electronService)
        {
            _electronService = electronService;
            Title = "Hello from DotVVM!";

        }
        public async Task BrowserWindowFocus()
        {
            await _electronService.App.BrowserWindowFocus(() =>
            {
                //some logic
            });
        }

        public async Task OpenExternal()
        {
            var result = await _electronService.Shell.OpenExternalAsync(@"https://github.com/riganti/dotvvm-electron");
        }

        public async Task ShowMessageBox()
        {
            var options = new ShowMessageBoxOptions
            {
                Title = "TEST"
            };
            await _electronService.Dialog.ShowMessageBox(options);
        }
        public async Task MinimizeWindow()
        {
            await _electronService.MainWindow.MinimizeAsync();
        }

        public async Task CloseWindow()
        {
            await _electronService.MainWindow.CloseAsync();
        }
    }
}
