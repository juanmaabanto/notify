using Microsoft.EntityFrameworkCore.Migrations;

namespace Expertec.Lcc.Services.Notify.API.Migrations
{
    public partial class ConfiguraPopup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagenUrl",
                schema: "notifica",
                table: "Alerta",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Permanente",
                schema: "notifica",
                table: "Alerta",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Popup",
                schema: "notifica",
                table: "Alerta",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagenUrl",
                schema: "notifica",
                table: "Alerta");

            migrationBuilder.DropColumn(
                name: "Permanente",
                schema: "notifica",
                table: "Alerta");

            migrationBuilder.DropColumn(
                name: "Popup",
                schema: "notifica",
                table: "Alerta");
        }
    }
}
