using System;
using Expertec.Lcc.Services.Notify.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expertec.Lcc.Services.Notify.API.Infrastructure.EntityTypeConfiguration
{
    class NotificacionEntityTypeConfiguration : IEntityTypeConfiguration<Notificacion>
    {
        public void Configure(EntityTypeBuilder<Notificacion> builder)
        {
            builder.ToTable(nameof(Notificacion), NotifyContext.DEFAULT_SCHEMA);

            builder.HasKey(b => b.NotificacionId);

            builder.Property(b => b.NotificacionId)
                .ValueGeneratedOnAdd();

            builder.Property<int>(b => b.AlertaId)
                .IsRequired();

            builder.Property<string>(b => b.UsuarioId)
                .HasMaxLength(128)
                .IsRequired();
            
            builder.Property<string>(b => b.EmisorId)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property<string>(b => b.Contenido)
                .HasColumnType("varchar(256)")
                .IsRequired();

            builder.Property<DateTime>(b => b.Fecha)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property<bool>(b => b.Notificado)
                .IsRequired();

            builder.Property<string>(b => b.Enlace)
                .HasColumnType("varchar(256)")
                .IsRequired(false);

            builder.Property<bool>(b => b.EnlaceExterno)
                .IsRequired();

            builder.Property<DateTime?>(b => b.FechaNotificado)
                .HasColumnType("datetime")
                .IsRequired(false);

            builder.Property<bool>(b => b.Anulado)
                .IsRequired();

            builder.HasOne(b => b.Alerta)
                .WithMany()
                .HasForeignKey(r => r.AlertaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Usuario)
                .WithMany()
                .HasForeignKey(r => r.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Emisor)
                .WithMany()
                .HasForeignKey(r => r.EmisorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}