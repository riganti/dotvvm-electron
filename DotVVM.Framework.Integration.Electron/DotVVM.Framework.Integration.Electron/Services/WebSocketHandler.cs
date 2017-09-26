using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotVVM.Framework.Integration.Electron.Services
{

    public abstract class WebSocketHandler
    {
        protected WebSocket CurrentConnection { get; set; }

        public WebSocketHandler()
        {
            CurrentConnection = null;
        }

        public virtual Task OnConnected(WebSocket socket)
        {
            CurrentConnection = socket;
            return Task.CompletedTask;
        }

        public virtual Task OnDisconnected(WebSocket socket)
        {
            CurrentConnection = null;
            return Task.CompletedTask;
        }

        public async Task SendMessageAsync(WebSocket socket, string message)
        {
            if(socket.State != WebSocketState.Open)
                return;

            await socket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.UTF8.GetBytes(message),
                                                                  offset: 0, 
                                                                  count: message.Length),
                                   messageType: WebSocketMessageType.Text,
                                   endOfMessage: true,
                                   cancellationToken: CancellationToken.None);          
        }

        public async Task SendMessageAsync(string message)
        {
            if(CurrentConnection == null)
            {
                throw new Exception("Current Web Socket is closed");
            }

            await SendMessageAsync(CurrentConnection, message);
        }

        public abstract Task ReceiveAsync(WebSocketReceiveResult result, byte[] buffer);
    }
}