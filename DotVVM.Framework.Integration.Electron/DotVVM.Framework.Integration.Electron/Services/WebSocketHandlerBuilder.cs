using System.Net.WebSockets;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DotVVM.Electron.Services
{
    public static class WebSocketHandlerBuilder
    {
        public static void UseWebSocketHandler(this IApplicationBuilder app, string path)
        {
            app.Map(path, (builder) =>
            {
                builder.UseMiddleware<WebSocketManagerMiddleware>();
            });
        }
    }
}