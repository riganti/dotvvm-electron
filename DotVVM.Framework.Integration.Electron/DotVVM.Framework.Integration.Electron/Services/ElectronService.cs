using DotVVM.Framework.Integration.Electron.Modules;

namespace DotVVM.Framework.Integration.Electron.Services
{
    public class ElectronService
    {
        public ElectronService(DialogModule dialogModule, MainWindowModule mainWindowModule, ClipboardModule clipBoardModule, AppModule appModule,
        ShellModule shellModule, MenuModule menuModule)
        {
            Menu = menuModule;
            Dialog = dialogModule;
            ClipBoard = clipBoardModule;
            MainWindow = mainWindowModule;
            App = appModule;
            Shell = shellModule;
        }
        public MenuModule Menu { get; set; }
        public ShellModule Shell { get; set; }
        public AppModule App { get; set; }
        public DialogModule Dialog { get; set; }

        public ClipboardModule ClipBoard { get; set; }

        public MainWindowModule MainWindow { get; set; }
    }
}