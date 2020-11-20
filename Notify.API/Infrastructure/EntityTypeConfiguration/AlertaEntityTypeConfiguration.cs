using Expertec.Lcc.Services.Notify.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expertec.Lcc.Services.Notify.API.Infrastructure.EntityTypeConfiguration
{
    class AlertaEntityTypeConfiguration : IEntityTypeConfiguration<Alerta>
    {
        public void Configure(EntityTypeBuilder<Alerta> builder)
        {
            builder.ToTable(nameof(Alerta), NotifyContext.DEFAULT_SCHEMA);

            builder.HasKey(b => b.AlertaId);

            builder.Property(b => b.AlertaId)
                .ValueGeneratedOnAdd();

            builder.Property<int>(b => b.TipoAlertaId)
                .IsRequired();

            builder.Property<string>(b => b.Nombre)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property<string>(b => b.ImagenUrl)
                .HasMaxLength(256)
                .IsRequired(false);
            
            builder.Property<bool>(b => b.Popup)
                .IsRequired();

            builder.Property<bool>(b => b.Permanente)
                .IsRequired();

            builder.Property<bool>(b => b.Activo)
                .IsRequired();

            builder.HasOne(b => b.TipoAlerta)
                .WithMany()
                .HasForeignKey(r => r.TipoAlertaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}