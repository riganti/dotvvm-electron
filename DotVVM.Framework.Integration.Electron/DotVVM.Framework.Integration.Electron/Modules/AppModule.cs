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

        public async Task BeforeQuit(Func<Task> eventAction, bool usePreventDefault)
        {
            await SubscribeEventAsync(eventAction, usePreventDefault);
        }

        public async Task BrowserWindowFocus(Func<Task> eventAction)
        {
            await SubscribeEventAsync(eventAction);
        }
     
    }
}