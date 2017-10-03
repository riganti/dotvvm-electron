using System.Collections.Generic;

namespace DotVVM.Electron.Modules.Options
{
    public class FileFilter
    {
        public string Name { get; set; }
        public IEnumerable<string> Extensions { get; set; }
    }
}