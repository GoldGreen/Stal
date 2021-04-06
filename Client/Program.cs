using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Stal.Shared.Log;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Stal.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<ILogger, Logger>(_ => new Logger("log-client.txt"));

            await builder.Build().RunAsync();
        }
    }
}
