using DotVVM.Framework.Integration.Electron.Services;
using System;
using System.Threading.Tasks;


namespace DotVVM.Framework.Integration.Electron.Modules
{
    public class AppModule : ElectronModule
    {
        public AppModule(ElectronMessageHandler handler) : base(handler)
        {
        }

        public async Task BeforeQuit(Action eventAction = null)
        {
            await SendEventAsync(usePreventDefault: true);
            eventAction?.Invoke();
        }

        public async Task BrowserWindowFocus(Action eventAction = null)
        {
            await SendEventAsync();
            eventAction?.Invoke();
        }
     
    }
}