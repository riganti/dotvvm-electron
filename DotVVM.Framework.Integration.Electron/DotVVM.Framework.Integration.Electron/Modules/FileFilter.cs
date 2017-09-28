using System.Collections.Generic;

namespace DotVVM.Electron.Modules
{
    public class FileFilter
    {
        public string Name { get; set; }
        public IEnumerable<string> Extensions { get; set; }
    }
}