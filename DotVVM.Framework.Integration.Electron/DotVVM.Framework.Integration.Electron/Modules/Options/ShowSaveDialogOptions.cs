using System.Collections.Generic;

namespace DotVVM.Electron.Modules.Options
{
    public class ShowSaveDialogOptions
    {
        public string Title { get; set; }
        public string DefaultPath { get; set; }
        public string ButtonLabel { get; set; }
        public string Message { get; set; }
        public string NameFieldLabel { get; set; }
        public bool ShowTagField { get; set; }
        public IEnumerable<FileFilter> Filters { get; set; }
    }
}