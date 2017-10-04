using DotVVM.Electron.Services;
using System.Threading.Tasks;
using System;

namespace DotVVM.Electron.Modules
{
    public class MainWindowModule : ElectronModule
    {
        public MainWindowModule(ElectronMessageHandler handler) : base(handler)
        {
        }

        public async Task CloseAsync()
        {
            await SendActionAsync();
        }

        public async Task MinimizeAsync()
        {
            await SendActionAsync();
        }

        public Task<Guid> SubscribeCloseAsync(Func<Task> eventAction, bool usePreventDefault)
        {
            return SubscribeEventAsync(eventAction, usePreventDefault);
        }
    }
}