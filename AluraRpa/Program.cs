using AluraRpa.Application.Services;
using AluraRpa.Infra.Configurations;
using AluraRpa.Infra.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

ConfigureLog.InitializeLog();

var configuration = ConfigureServices.LoadConfiguration();

using var services = ConfigureServices.CreateServices(configuration);

using var aluraDbContext = services.GetRequiredService<AluraDbContext>();

aluraDbContext.Database.Migrate();

var aluraService = services.GetRequiredService<IAluraService>();

ManagerConsole.BringConsoleToFront();

Console.WriteLine("Digite algo para pesquisar ou somente ENTER para consultar RPA");

var textoAlternativo = Console.ReadLine();

if (string.IsNullOrWhiteSpace(textoAlternativo))
    aluraService.Consulta("RPA");
else
    aluraService.Consulta(textoAlternativo);

Console.WriteLine($"As informações de consulta do texto: {} informado foram salvas no banco de dados SqlServer :Alura-6d509556.");
Console.WriteLine("Pressione qualuer tecla para finalizar");

Console.ReadKey();