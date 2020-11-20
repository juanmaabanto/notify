using Expertec.Lcc.Services.Notify.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expertec.Lcc.Services.Notify.API.Infrastructure.EntityTypeConfiguration
{
    class UsuarioEntityTypeConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable(nameof(Usuario), NotifyContext.DEFAULT_SCHEMA);

            builder.HasKey(b => b.UsuarioId);

            builder.Property<string>(b => b.UsuarioId)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property<string>(b => b.NombreUsuario)
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}