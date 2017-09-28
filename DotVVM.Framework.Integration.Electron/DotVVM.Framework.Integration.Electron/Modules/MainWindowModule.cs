using DotVVM.Electron.Services;
using System.Threading.Tasks;

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
    }
}