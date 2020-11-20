﻿// <auto-generated />
using System;
using Expertec.Lcc.Services.Notify.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Expertec.Lcc.Services.Notify.API.Migrations
{
    [DbContext(typeof(NotifyContext))]
    partial class NotifyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Expertec.Lcc.Services.Notify.API.Models.Alerta", b =>
                {
                    b.Property<int>("AlertaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("ImagenUrl")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("Permanente")
                        .HasColumnType("bit");

                    b.Property<bool>("Popup")
                        .HasColumnType("bit");

                    b.Property<int>("TipoAlertaId")
                        .HasColumnType("int");

                    b.HasKey("AlertaId");

                    b.HasIndex("TipoAlertaId");

                    b.ToTable("Alerta","notifica");
                });

            modelBuilder.Entity("Expertec.Lcc.Services.Notify.API.Models.AlertaUsuario", b =>
                {
                    b.Property<int>("AlertaId")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.HasKey("AlertaId", "UsuarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("AlertaUsuario","notifica");
                });

            modelBuilder.Entity("Expertec.Lcc.Services.Notify.API.Models.Notificacion", b =>
                {
                    b.Property<int>("NotificacionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AlertaId")
                        .HasColumnType("int");

                    b.Property<bool>("Anulado")
                        .HasColumnType("bit");

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("EmisorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Enlace")
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EnlaceExterno")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaNotificado")
                        .HasColumnType("datetime");

                    b.Property<bool>("Notificado")
                        .HasColumnType("bit");

                    b.Property<string>("UsuarioId")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.HasKey("NotificacionId");

                    b.HasIndex("AlertaId");

                    b.HasIndex("EmisorId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Notificacion","notifica");
                });

            modelBuilder.Entity("Expertec.Lcc.Services.Notify.API.Models.TipoAlerta", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("TipoAlertaId")
                        .HasColumnType("int");

                    b.Property<string>("IconoCls")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Nombre")
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("TipoAlerta","notifica");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IconoCls = "fas fa-exclamation-triangle",
                            Name = "warning"
                        },
                        new
                        {
                            Id = 2,
                            IconoCls = "fas fa-exclamation-circle",
                            Name = "danger"
                        },
                        new
                        {
                            Id = 3,
                            IconoCls = "fas fa-check-circle",
                            Name = "success"
                        },
                        new
                        {
                            Id = 4,
                            IconoCls = "fas fa-info-circle",
                            Name = "info"
                        });
                });

            modelBuilder.Entity("Expertec.Lcc.Services.Notify.API.Models.Usuario", b =>
                {
                    b.Property<string>("UsuarioId")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuario","notifica");
                });

            modelBuilder.Entity("Expertec.Lcc.Services.Notify.API.Models.Alerta", b =>
                {
                    b.HasOne("Expertec.Lcc.Services.Notify.API.Models.TipoAlerta", "TipoAlerta")
                        .WithMany()
                        .HasForeignKey("TipoAlertaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Expertec.Lcc.Services.Notify.API.Models.AlertaUsuario", b =>
                {
                    b.HasOne("Expertec.Lcc.Services.Notify.API.Models.Alerta", "Alerta")
                        .WithMany()
                        .HasForeignKey("AlertaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Expertec.Lcc.Services.Notify.API.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Expertec.Lcc.Services.Notify.API.Models.Notificacion", b =>
                {
                    b.HasOne("Expertec.Lcc.Services.Notify.API.Models.Alerta", "Alerta")
                        .WithMany()
                        .HasForeignKey("AlertaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Expertec.Lcc.Services.Notify.API.Models.Usuario", "Emisor")
                        .WithMany()
                        .HasForeignKey("EmisorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Expertec.Lcc.Services.Notify.API.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
