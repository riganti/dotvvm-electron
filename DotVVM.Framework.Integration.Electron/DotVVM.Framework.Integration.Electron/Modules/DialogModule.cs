using DotVVM.Framework.Integration.Electron.Helpers;
using DotVVM.Framework.Integration.Electron.Modules.Options;
using DotVVM.Framework.Integration.Electron.Services;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace DotVVM.Framework.Integration.Electron.Modules
{
    public class DialogModule : ElectronModule
    {
        public DialogModule(ElectronMessageHandler handler) : base(handler)
        {
        }

        public async Task<IEnumerable<string>> ShowOpenDialogAsync(ShowOpenDialogOptions options = null)
        {
            var result = await SendActionAsync(arguments: options);

            return result.ToObject<List<string>>();
        }

        public async Task<string> ShowSaveDialog(ShowSaveDialogOptions options = null)
        {
            var result = await SendActionAsync(arguments: options);
            return result.ToObject<string>();
        }

        public async Task<int> ShowMessageBox(ShowMessageBoxOptions options = null)
        {
            var result = await SendActionAsync(arguments: options);
            return result.ToObject<int>();
        }

        public async Task ShowErrorBox(ShowErrorBoxOptions options = null)
        {
            await SendActionAsync(arguments: ParamHelpers.GetParams(options.Title, options.Content));
        }
    }
}