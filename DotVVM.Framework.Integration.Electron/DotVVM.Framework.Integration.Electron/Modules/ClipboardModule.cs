using DotVVM.Framework.Integration.Electron.Helpers;
using DotVVM.Framework.Integration.Electron.Modules.Options;
using DotVVM.Framework.Integration.Electron.Services;
using System.Threading.Tasks;

namespace DotVVM.Framework.Integration.Electron.Modules
{
    public class ClipboardModule : ElectronModule
    {
        public ClipboardModule(ElectronMessageHandler handler) : base(handler)
        {
        }

        public async Task<ReadBookmarkOptions> ReadBookmarkAsync()
        {
            var result = await SendActionAsync();
            return result.ToObject<ReadBookmarkOptions>();
        }

        public async Task WriteBookMarkAsync(WriteBookmarkOptions options)
        {
            dynamic obj = options;
            await WriteAsync(obj);
        }
        private async Task WriteAsync(dynamic obj)
        {
            await SendActionAsync("WriteAsync", obj);
        }
        public async Task<string> ReadTextAsync(string type = null)
        {
            var result = await SendActionAsync();
            return result.ToObject<string>();
        }
        public async Task WriteTextAsync(string text, string type = null)
        {
            await SendActionAsync(arguments: ParamHelpers.GetParams(text, type));
        }

        public async Task<string> ReadHtmlAsync(string type = null)
        {
            var result = await SendActionAsync(arguments: ParamHelpers.GetParams(type));
            return result.ToObject<string>();
        }

        public async Task WriteHtmlAsync(string markup, string type = null)
        {
            await SendActionAsync(arguments: ParamHelpers.GetParams(markup, type));
        }

        public async Task<string> ReadRTFAsync(string type = null)
        {
            var result = await SendActionAsync(arguments: ParamHelpers.GetParams(type));
            return result.ToObject<string>();
        }
        public async Task WriteRTFAsync(string text, string type = null)
        {
            await SendActionAsync(arguments: ParamHelpers.GetParams(text, type));
        }

        public async Task ClearAsync(string type = null)
        {
            await SendActionAsync(arguments: ParamHelpers.GetParams(type));
        }

    }
}