using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DotVVM.Electron.Services
{
    public class ElectronAction<TArguments> : ElectronAction
    {
        public TArguments Arguments { get; set; }
    }

    public class ElectronAction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Method { get; set; }
        public string Module { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ElectronRequestType Type {get;set;} 
    }
}
