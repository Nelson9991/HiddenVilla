using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using HiddenVilla_Client.Services;
using HiddenVilla_Client.Services.IServices;

namespace HiddenVilla_Client
{
    using Blazorise;
    using Blazorise.Bootstrap;
    using Blazorise.Icons.FontAwesome;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl"))
            });

            ConfigureServices(builder.Services);

            var host = builder.Build();

            host.Services
                .UseBootstrapProviders()
                .UseFontAwesomeIcons();

            await host.RunAsync();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddBlazorise(options => { options.ChangeTextOnKeyPress = true; })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();

            services.AddBlazoredLocalStorage();

            services.AddScoped<IHotelRoomService, HotelRoomService>();
            services.AddScoped<IAmenitieService, AmenitiesService>();
        }
    }
}
