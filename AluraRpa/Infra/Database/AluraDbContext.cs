using AluraRpa.Infra.Database.Configurations;
using AluraRpa.Infra.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace AluraRpa.Infra.Database
{
    public class AluraDbContext : DbContext
    {
        public AluraDbContext(DbContextOptions<AluraDbContext> options) : base(options)
        {
        }

        public DbSet<ConsultaModel> Consulta { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration<ConsultaModel>(new ConsultaConfiguration());
        }

    }
}
