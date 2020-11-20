using Expertec.Lcc.Services.Notify.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expertec.Lcc.Services.Notify.API.Infrastructure.EntityTypeConfiguration
{
    class AlertaUsuarioEntityTypeConfiguration : IEntityTypeConfiguration<AlertaUsuario>
    {
        public void Configure(EntityTypeBuilder<AlertaUsuario> builder)
        {
            builder.ToTable(nameof(AlertaUsuario), NotifyContext.DEFAULT_SCHEMA);

            builder.HasKey(b => new { b.AlertaId, b.UsuarioId });

            builder.Property<string>(b => b.UsuarioId)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasOne(b => b.Alerta)
                .WithMany()
                .HasForeignKey(r => r.AlertaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Usuario)
                .WithMany()
                .HasForeignKey(r => r.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}