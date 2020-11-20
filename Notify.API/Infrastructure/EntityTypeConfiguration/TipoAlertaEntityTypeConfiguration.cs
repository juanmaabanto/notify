using Expertec.Lcc.Services.Notify.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expertec.Lcc.Services.Notify.API.Infrastructure.EntityTypeConfiguration
{
    class TipoAlertaEntityTypeConfiguration : IEntityTypeConfiguration<TipoAlerta>
    {
        public void Configure(EntityTypeBuilder<TipoAlerta> builder)
        {
            builder.ToTable("TipoAlerta", NotifyContext.DEFAULT_SCHEMA);

            builder.HasKey(b => b.Id);

            builder.Property(c => c.Id)
                .HasColumnName("TipoAlertaId")
                .ValueGeneratedNever();

            builder.Property(c => c.Name)
                .HasColumnName("Nombre")
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.Property(c => c.IconoCls)
                .HasColumnType("varchar(30)")
                .IsRequired();

            builder.HasData(
                TipoAlerta.List()
            );
        }
    }
}