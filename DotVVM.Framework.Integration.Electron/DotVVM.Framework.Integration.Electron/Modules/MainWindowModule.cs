using DotVVM.Electron.Services;
using System.Threading.Tasks;
using System;
using DotVVM.Electron.Helpers;
using DotVVM.Electron.Modules.Options;

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

        public async Task ReloadAsync()
        {
            await SendActionAsync();
        }

        public async Task SetProgressBarAsync(double progress, ProgressBarOptions options = null)
        {
            await SendActionAsync(arguments: ParamHelpers.GetParams(progress, options));
        }

        public async Task LoadUrlAsync(string url)
        {
            await SendActionAsync(arguments: url);
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