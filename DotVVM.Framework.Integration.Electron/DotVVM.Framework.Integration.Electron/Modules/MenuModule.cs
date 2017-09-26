using DotVVM.Framework.Integration.Electron.Modules.Options;
using DotVVM.Framework.Integration.Electron.Services;
using System.Threading.Tasks;


namespace DotVVM.Framework.Integration.Electron.Modules
{
    public class MenuModule : ElectronModule
    {
        public MenuModule(ElectronMessageHandler handler) : base(handler)
        {
        }

        public async Task PopupAsync(PopupMenuOptions options = null)
        {
            await SendActionAsync(arguments:options);
        }

        public async Task ClosePopupAsync()
        {
            await SendActionAsync();
        }

    }
}