using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Expertec.Lcc.Services.Notify.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "notifica");

            migrationBuilder.CreateTable(
                name: "TipoAlerta",
                schema: "notifica",
                columns: table => new
                {
                    TipoAlertaId = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(type: "varchar(20)", nullable: false),
                    IconoCls = table.Column<string>(type: "varchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAlerta", x => x.TipoAlertaId);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                schema: "notifica",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(maxLength: 128, nullable: false),
                    NombreUsuario = table.Column<string>(maxLength: 256, nullable: false),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Alerta",
                schema: "notifica",
                columns: table => new
                {
                    AlertaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoAlertaId = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerta", x => x.AlertaId);
                    table.ForeignKey(
                        name: "FK_Alerta_TipoAlerta_TipoAlertaId",
                        column: x => x.TipoAlertaId,
                        principalSchema: "notifica",
                        principalTable: "TipoAlerta",
                        principalColumn: "TipoAlertaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlertaUsuario",
                schema: "notifica",
                columns: table => new
                {
                    AlertaId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertaUsuario", x => new { x.AlertaId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_AlertaUsuario_Alerta_AlertaId",
                        column: x => x.AlertaId,
                        principalSchema: "notifica",
                        principalTable: "Alerta",
                        principalColumn: "AlertaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlertaUsuario_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "notifica",
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notificacion",
                schema: "notifica",
                columns: table => new
                {
                    NotificacionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlertaId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<string>(maxLength: 128, nullable: false),
                    EmisorId = table.Column<string>(maxLength: 128, nullable: false),
                    Contenido = table.Column<string>(type: "varchar(256)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    Notificado = table.Column<bool>(nullable: false),
                    Enlace = table.Column<string>(type: "varchar(256)", nullable: true),
                    EnlaceExterno = table.Column<bool>(nullable: false),
                    FechaNotificado = table.Column<DateTime>(type: "datetime", nullable: true),
                    Anulado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacion", x => x.NotificacionId);
                    table.ForeignKey(
                        name: "FK_Notificacion_Alerta_AlertaId",
                        column: x => x.AlertaId,
                        principalSchema: "notifica",
                        principalTable: "Alerta",
                        principalColumn: "AlertaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notificacion_Usuario_EmisorId",
                        column: x => x.EmisorId,
                        principalSchema: "notifica",
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notificacion_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "notifica",
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "notifica",
                table: "TipoAlerta",
                columns: new[] { "TipoAlertaId", "IconoCls", "Nombre" },
                values: new object[,]
                {
                    { 1, "fas fa-exclamation-triangle", "warning" },
                    { 2, "fas fa-exclamation-circle", "danger" },
                    { 3, "fas fa-check-circle", "success" },
                    { 4, "fas fa-info-circle", "info" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alerta_TipoAlertaId",
                schema: "notifica",
                table: "Alerta",
                column: "TipoAlertaId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertaUsuario_UsuarioId",
                schema: "notifica",
                table: "AlertaUsuario",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacion_AlertaId",
                schema: "notifica",
                table: "Notificacion",
                column: "AlertaId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacion_EmisorId",
                schema: "notifica",
                table: "Notificacion",
                column: "EmisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacion_UsuarioId",
                schema: "notifica",
                table: "Notificacion",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlertaUsuario",
                schema: "notifica");

            migrationBuilder.DropTable(
                name: "Notificacion",
                schema: "notifica");

            migrationBuilder.DropTable(
                name: "Alerta",
                schema: "notifica");

            migrationBuilder.DropTable(
                name: "Usuario",
                schema: "notifica");

            migrationBuilder.DropTable(
                name: "TipoAlerta",
                schema: "notifica");
        }
    }
}
