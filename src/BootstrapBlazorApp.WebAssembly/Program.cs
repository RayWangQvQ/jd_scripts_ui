using BootstrapBlazorApp.Shared;
using BootstrapBlazorApp.Shared.Data;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BootstrapBlazorApp.WebAssembly
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5000") });

            // 增加 BootstrapBlazor 组件
            builder.Services.AddBootstrapBlazor();

            builder.Services.AddSingleton<WeatherForecastService>();

            // 增加 Table 数据服务操作类
            builder.Services.AddTableDemoDataService();

            var host = builder.Build();

            await host.RunAsync();
        }
    }
}
