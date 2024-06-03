using AluraRpa.Infra.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AluraRpa.Infra.Database
{
    public class AluraDbContextFactory : IDesignTimeDbContextFactory<AluraDbContext>
    {
        public AluraDbContext CreateDbContext(string[] args)
        {
            var configuration = ConfigureServices.LoadConfiguration();

            var optionsBuilder = new DbContextOptionsBuilder<AluraDbContext>();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("AluraDbContext")!);

            return new AluraDbContext(optionsBuilder.Options);
        }
    }
}
