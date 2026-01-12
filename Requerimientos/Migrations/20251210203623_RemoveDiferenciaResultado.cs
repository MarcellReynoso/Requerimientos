using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Requerimientos.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDiferenciaResultado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Diferencia",
                table: "Requerimiento");

            migrationBuilder.DropColumn(
                name: "Resultado",
                table: "Requerimiento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Diferencia",
                table: "Requerimiento",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Resultado",
                table: "Requerimiento",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);
        }
    }
}
