using System.Collections.Generic;

namespace DotVVM.Electron.Modules.Options
{
    public class ShowOpenDialogOptions
    {
        public string Title { get; set; }
        public string DefaultPath { get; set; }
        public string ButtonLabel { get; set; }
        public IEnumerable<string> Properties { get; set; }
        public IEnumerable<FileFilter> Filters {get;set;}
    }
}