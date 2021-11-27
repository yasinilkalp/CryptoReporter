using Hangfire.Logging;
using Hangfire.Logging.LogProviders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace CryptoReporter
{
    class Program
    {
        static void Main(string[] args)
        {
            LogProvider.SetCurrentLogProvider(new ColouredConsoleLogProvider());
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
