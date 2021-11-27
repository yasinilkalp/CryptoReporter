using AutoMapper;
using CryptoReporter.Mappers;
using CryptoReporter.Model;
using CryptoReporter.Service.Concrete;
using CryptoReporter.Service.Contract;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;
using System.Reflection;

[assembly: OwinStartup(typeof(CryptoReporter.Startup))]

namespace CryptoReporter
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {


            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #region Configure Mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod is not null && (p.GetMethod.IsPublic || p.GetMethod.IsAssembly);
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();
            #endregion


            services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseDefaultTypeSerializer()
                    .UseMemoryStorage());

            services.AddHangfireServer();
            services.AddSingleton<IBinanceService, BinanceService>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton(typeof(ITemplateService<>), typeof(TemplateService<>));
            services.AddSingleton<ICryptoReportService, CryptoReportService>();

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.Configure<BinanceApiSettings>(Configuration.GetSection("BinanceApiSettings"));
        }

        public void Configure(
            IApplicationBuilder app,
            IRecurringJobManager recurringJobManager,
            ICryptoReportService currencyReporterService)
        {
            app.UseRouting();

            app.UseWelcomePage("/");
            app.UseHangfireServer();
            app.UseHangfireDashboard();
            recurringJobManager.AddOrUpdate("BTCUSDT Reporter", () =>
            currencyReporterService.SendReport("BTCUSDT"), Cron.Hourly);

            recurringJobManager.AddOrUpdate("BNBUSDT Reporter", () =>
            currencyReporterService.SendReport("BNBUSDT"), Cron.Minutely);

            
        }
    }
}
