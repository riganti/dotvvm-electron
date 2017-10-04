using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DotVVM.Electron.Services
{
    public class ElectronMessageHandler : WebSocketHandler
    {
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly ConcurrentDictionary<Guid, Func<Task>> _eventHandlers
            = new ConcurrentDictionary<Guid, Func<Task>>();
        public event Func<ElectronResponse, Task> ResponseReceived;

        public ElectronMessageHandler()
        {
            _serializerSettings = new JsonSerializerSettings();
            _serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            _serializerSettings.NullValueHandling = NullValueHandling.Ignore;
            _serializerSettings.CheckAdditionalContent = true;
            _serializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;

            ResponseReceived +=  async (response) => {
                if(_eventHandlers.TryGetValue(response.ActionId, out var handler))
                {
                    await handler();
                }
            };
        }

        public async Task<ElectronResponse> SendActionAsync<TArgument>(ElectronAction<TArgument> action)
        {
            var serializedObject = JsonConvert.SerializeObject(action, _serializerSettings);

            await SendMessageAsync(serializedObject);

            return await ReceiveResponseForActionAsync(action.Id);
        }

        public async Task SubscribeToEventAsync(ElectronAction<ElectronEventArguments> action, Func<Task> eventHandler)
        {
            var serializedObject = JsonConvert.SerializeObject(action, _serializerSettings);

            _eventHandlers.TryAdd(action.Id, eventHandler);

            await SendMessageAsync(serializedObject);
        }

        public async Task UnSubscribeToEventAsync(Guid actionId)
        {
            var action = new ElectronAction
            {
                Id = actionId,
                Type = ElectronRequestType.UnSubscribeEvent
            };
            
            var serializedObject = JsonConvert.SerializeObject(action, _serializerSettings);

            _eventHandlers.TryRemove(actionId, out _);

            await SendMessageAsync(serializedObject);
        }

        public override async Task ReceiveAsync(WebSocketReceiveResult result, byte[] buffer)
        {
            var response = JsonConvert.DeserializeObject<ElectronResponse>(
                System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count)
            );

            await ResponseReceived?.Invoke(response);
        }

        private Task<ElectronResponse> ReceiveResponseForActionAsync(Guid id)
        {
            var tcs = new TaskCompletionSource<ElectronResponse>();

            Func<ElectronResponse, Task> handler = null;
            handler = (response) =>
            {
                if (id == response.ActionId)
                {
                    ResponseReceived -= handler;
                    tcs.SetResult(response);
                }
                return Task.CompletedTask;
            };

            // will get raised, when the work is done
            ResponseReceived += handler;

            return tcs.Task;
        }
    }
}