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

        public async Task UnSubscribeEventAsync(Guid actionId)
        {
            if(actionId == Guid.Empty)
            {
                throw new ArgumentException(nameof(actionId));
            }

            await _handler.UnSubscribeToEventAsync(actionId);
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

        protected async Task<Guid> SubscribeEventAsync(Func<Task> handler, bool usePreventDefault = false, [CallerMemberName] string methodName = null)
        {
            ThrowExceptionWhenMethodNameIsNull(methodName);

            var normalizedModuleName = Regex.Replace(this.GetType().Name, @"Module$", string.Empty).FirstCharacterToLower();
            var normalizedEventName = Regex.Replace(Regex.Replace(methodName, @"Async$", string.Empty), @"^Subscribe", string.Empty) 
                .InsertDashBeforeUpperCharacters()
                .ToLower();

            var action = new ElectronAction
            {
                Module = normalizedModuleName,
                Method = normalizedEventName,
                Type = ElectronRequestType.SubscribeEvent,
                UsePreventDefault = usePreventDefault
            };
            await _handler.SubscribeToEventAsync(action, handler);

            return action.Id;
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