using System.Collections.Generic;

namespace DotVVM.Framework.Integration.Electron.Modules.Options
{
    public class ShowMessageBoxOptions
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public string CheckBoxLabel { get; set; }
        public bool CheckBoxChecked { get; set; }
        public int CancelId { get; set; }
        public bool NoLink { get; set; }
        public bool NormalizeAccessKeys { get; set; }
        public IEnumerable<string> Buttons { get; set; }
        public int DefaultId { get; set; }

    }
}