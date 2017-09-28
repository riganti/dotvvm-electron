using System;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DotVVM.Electron.Services
{
    public class ElectronMessageHandler : WebSocketHandler
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public event EventHandler<ElectronResponse> ResponseReceived;

        public ElectronMessageHandler()
        {
            _serializerSettings = new JsonSerializerSettings();
            _serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            _serializerSettings.NullValueHandling = NullValueHandling.Ignore;
            _serializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
            _serializerSettings.CheckAdditionalContent = true;
            _serializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
        }

        public async Task<ElectronResponse> SendActionAsync(ElectronAction action)
        {
            var serializedObject = JsonConvert.SerializeObject(action, _serializerSettings);

            await SendMessageAsync(serializedObject);

            return await ReceiveResponseForActionAsync(action.Id);
        }

        public override Task ReceiveAsync(WebSocketReceiveResult result, byte[] buffer)
        {
            var response = JsonConvert.DeserializeObject<ElectronResponse>(
                System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count)
            );

            ResponseReceived?.Invoke(this, response);
            return Task.CompletedTask;
        }

        private Task<ElectronResponse> ReceiveResponseForActionAsync(Guid id)
        {
            var tcs = new TaskCompletionSource<ElectronResponse>();

            EventHandler<ElectronResponse> handler = null;
            handler = (s, response) =>
            {
                if((response.Type == ElectronResponseType.Response || response.Type == ElectronResponseType.Event) && id == response.ActionId)
                {
                    ResponseReceived -= handler;
                    tcs.SetResult(response);
                }    
            };
           
            // will get raised, when the work is done
            ResponseReceived += handler;

            return tcs.Task;
        }
    }
}