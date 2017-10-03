using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DotVVM.Electron.Services
{
    public class ElectronAction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Method { get; set; }
        public string Module { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ElectronRequestType Type {get;set;} 
        public bool UsePreventDefault {get;set;}
        public object[] Arguments { get; set; } = Array.Empty<object>();
    }
}
