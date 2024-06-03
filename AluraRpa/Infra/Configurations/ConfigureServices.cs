using AluraRpa.Application.Services;
using AluraRpa.Domain.Models;
using AluraRpa.Domain.Services;
using AluraRpa.Domain.Validators;
using AluraRpa.Infra.Database;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace AluraRpa.Infra.Configurations
{
    public static class ConfigureServices
    {
        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfiguration configuration = builder.Build();
            return configuration;
        }

        public static ServiceProvider CreateServices(IConfiguration configuration)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton(configuration)
                .AddSingleton<IAppSettings>((provider) =>
                {
                    var config = provider.GetRequiredService<IConfiguration>();
                    var appSettings = new AppSettings
                    {
                        Alura = config.GetSection("Alura").Get<Alura>()!
                    };
                    return appSettings!;
                })
                .AddSingleton<IWebDriver>((provider) =>
                {
                    var appSettings = provider.GetRequiredService<IAppSettings>();
                    if (appSettings.Alura.DriverType == "Chrome")
                    {
                        return new ChromeDriver();
                    }
                    return new EdgeDriver();
                })
                .AddDbContext<AluraDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("AluraDbContext")!))
                .AddScoped<IValidator<Consulta>, ConsultaValidator>()
                .AddScoped<IConsultaService, ConsultaService>()
                .AddScoped<IAluraService, AluraService>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
