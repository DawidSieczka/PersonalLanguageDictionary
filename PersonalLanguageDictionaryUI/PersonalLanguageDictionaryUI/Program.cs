using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PersonalLanguageDictionaryUI.Application.Interfaces;
using PersonalLanguageDictionaryUI.Application.Services;

namespace PersonalLanguageDictionaryUI
{
    public class Program
    {
        private static WebAssemblyHostBuilder _builder;
        public static async Task Main(string[] args)
        {
            _builder = WebAssemblyHostBuilder.CreateDefault(args);
            _builder.RootComponents.Add<App>("#app");
            AddServices(_builder.Services);
            
            await _builder.Build().RunAsync();

        }

        public static void AddServices(IServiceCollection services)
        {

            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(_builder.HostEnvironment.BaseAddress) });
            services.AddTransient<IPersonalLanguageDictionaryService, PersonalLanguageDictionaryService>();
            services.AddHttpClient();
        }
    }
}
