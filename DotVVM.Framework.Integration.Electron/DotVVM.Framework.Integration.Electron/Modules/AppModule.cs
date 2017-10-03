using DotVVM.Electron.Services;
using System;
using System.Threading.Tasks;


namespace DotVVM.Electron.Modules
{
    public class AppModule : ElectronModule
    {
        public AppModule(ElectronMessageHandler handler) : base(handler)
        {
        }

        public async Task BeforeQuit(Action eventAction, bool usePreventDefault)
        {
            await SubscribeEventAsync(usePreventDefault);
            eventAction?.Invoke();
        }

        public async Task BrowserWindowFocus(Action eventAction = null)
        {
            await SubscribeEventAsync();
            eventAction?.Invoke();
        }
     
    }
}