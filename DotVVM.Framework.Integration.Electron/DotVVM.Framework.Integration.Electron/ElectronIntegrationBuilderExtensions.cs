using System;
using DotVVM.Electron.Modules;
using DotVVM.Electron.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DotVVM.Electron
{
    public static class ElectronIntegrationBuilderExtensions
    {
        public static IServiceCollection AddElectronIntegration(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddTransient<ElectronService>();
            services.AddTransient<DialogModule>();
            services.AddTransient<AppModule>();
            services.AddTransient<ShellModule>();
            services.AddTransient<MenuModule>();
            services.AddTransient<ClipboardModule>();
            services.AddTransient<MainWindowModule>();

            services.AddSingleton<ElectronMessageHandler>();
            services.AddSingleton<WebSocketHandler, ElectronMessageHandler>(
                c => c.GetService<ElectronMessageHandler>());

            return services;
        }

        public static void UseElectronIntegration(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            app.UseWebSockets();

            app.UseWebSocketHandler("/ws-electron");
        }
    }
}