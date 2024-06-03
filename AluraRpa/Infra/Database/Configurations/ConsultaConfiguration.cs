using AluraRpa.Infra.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AluraRpa.Infra.Database.Configurations
{
    public class ConsultaConfiguration : IEntityTypeConfiguration<ConsultaModel>
    {
        public void Configure(EntityTypeBuilder<ConsultaModel> builder)
        {
            builder.ToTable("Consulta");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).UseIdentityColumn();

            builder.Property(p => p.Titulo).HasMaxLength(255).IsRequired();
            builder.Property(p => p.Professores).HasMaxLength(4096).IsRequired();
            builder.Property(p => p.CargaHoraria).HasMaxLength(255).IsRequired();
            builder.Property(p => p.Descricao).HasMaxLength(4096).IsRequired();
        }
    }
}
