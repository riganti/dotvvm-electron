using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using DotVVM.Electron.Services;
using DotVVM.Electron.Helpers;

namespace DotVVM.Electron.Modules
{
    public abstract class ElectronModule
    {
        private ElectronMessageHandler _handler;

        public ElectronModule(ElectronMessageHandler handler)
        {
            _handler = handler;
        }

        protected async Task<JToken> SendActionAsync([CallerMemberName] string methodName = null, params object[] arguments)
        {
            ThrowExceptionWhenMethodNameIsNull(methodName);

            var normalizedModuleName = Regex.Replace(this.GetType().Name, @"Module$", string.Empty).FirstCharacterToLower();
            var normalizedMethodName = Regex.Replace(methodName, @"Async$", string.Empty).FirstCharacterToLower();

            var action = new ElectronAction
            {
                Module = normalizedModuleName,
                Method = normalizedMethodName,
                Type = ElectronRequestType.Method,
                Arguments = arguments
            };

            var response = await _handler.SendActionAsync(action);
            return response.Result;
        }

        protected async Task SendEventAsync([CallerMemberName] string methodName = null, bool usePreventDefault = false)
        {
            ThrowExceptionWhenMethodNameIsNull(methodName);

            var normalizedModuleName = Regex.Replace(this.GetType().Name, @"Module$", string.Empty).FirstCharacterToLower();
            var normalizedEventName = Regex.Replace(methodName, @"Async$", string.Empty).InsertDashBeforeUpperCharacters().ToLower();

            var action = new ElectronAction
            {
                Module = normalizedModuleName,
                Method = normalizedEventName,
                Type = ElectronRequestType.Event,
                UsePreventDefault = usePreventDefault
            };
            await _handler.SendActionAsync(action);
        }

        private static void ThrowExceptionWhenMethodNameIsNull(string methodName)
        {
            if (methodName == null)
            {
                throw new ArgumentException(nameof(methodName));
            }
        }
    }
}