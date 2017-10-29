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

        public async Task SubscribeBeforeQuit(Func<Task> eventAction, bool usePreventDefault = false, bool isPageEvent = true)
        {
            await SubscribeEventAsync(eventAction, usePreventDefault, isPageEvent);
        }

        public async Task SubscribeBrowserWindowFocus(Func<Task> eventAction, bool usePreventDefault = false, bool isPageEvent = true)
        {
            await SubscribeEventAsync(eventAction, usePreventDefault, isPageEvent);
        }

    }
}