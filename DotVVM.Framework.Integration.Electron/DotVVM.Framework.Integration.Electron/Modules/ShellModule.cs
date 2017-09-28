using DotVVM.Electron.Helpers;
using DotVVM.Electron.Services;
using System.Threading.Tasks;


namespace DotVVM.Electron.Modules
{
    public class ShellModule : ElectronModule
    {
        public ShellModule(ElectronMessageHandler handler) : base(handler)
        {
        }


        public async Task<bool> ShowItemInFolderAsync(string fullPath)
        {
            var result = await SendActionAsync(arguments: ParamHelpers.GetParams(fullPath));
            return result.ToObject<bool>();
        }   

        public async Task<bool> OpenItemAsync(string fullPath)
        {
            var result = await SendActionAsync(arguments: ParamHelpers.GetParams(fullPath));
            return result.ToObject<bool>();
        }

        public async Task<bool> OpenExternalAsync(string url)
        {
            var result = await SendActionAsync(arguments:ParamHelpers.GetParams(url));
            return result.ToObject<bool>();
        }

        public async Task<bool> MoveItemToTrashAsync(string fullPath)
        {
            var result = await SendActionAsync(arguments:ParamHelpers.GetParams(fullPath));
            return result.ToObject<bool>();
        }

    }
}