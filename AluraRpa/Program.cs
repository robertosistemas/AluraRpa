using AluraRpa.Application.Services;
using AluraRpa.Infra.Configurations;
using AluraRpa.Infra.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;

ConfigureLog.InitializeLog();

var configuration = ConfigureServices.LoadConfiguration();

using var services = ConfigureServices.CreateServices(configuration);

using var aluraDbContext = services.GetRequiredService<AluraDbContext>();

aluraDbContext.Database.Migrate();

ManagerConsole.BringConsoleToFront();

Console.WriteLine("Digite algo e pressione ENTER para pesquisar ou somente ENTER para consultar 'RPA'");

var textoParaProcurar = Console.ReadLine();

if (string.IsNullOrWhiteSpace(textoParaProcurar))
    textoParaProcurar = "RPA";

var aluraService = services.GetRequiredService<IAluraService>();

aluraService.Consulta(textoParaProcurar.Trim());

var driver = services.GetRequiredService<IWebDriver>();

driver.Quit();

ManagerConsole.BringConsoleToFront();

Console.WriteLine();
Console.WriteLine($"As informações de consulta do texto: {textoParaProcurar} informado foram salvas no banco de dados SqlServer :Alura-6d509556.");
Console.WriteLine();
Console.WriteLine("Pressione qualuer tecla para finalizar");

Console.ReadKey();
