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

        public string Title { get; set; }

        public Guid ActionId {get;set;}

        public DefaultViewModel(ElectronService electronService)
        {
            _electronService = electronService;
            Title = "Hello from DotVVM!";
        }

        public async Task SubscribeCloseAsync()
        {
            ActionId = await _electronService.MainWindow.SubscribeCloseAsync(async () =>
            {
                var buttons = new List<string>{"Ok", "Cancel"};
                var options = new ShowMessageBoxOptions
                {
                    Title = "Are you sure",
                    Buttons = buttons
                };
                var result = await _electronService.Dialog.ShowMessageBox(options);   
                if(buttons[result] == "Ok")
                {
                    await _electronService.MainWindow.UnSubscribeEventAsync(ActionId);
                    await _electronService.MainWindow.CloseAsync();
                }
            }, true);
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
