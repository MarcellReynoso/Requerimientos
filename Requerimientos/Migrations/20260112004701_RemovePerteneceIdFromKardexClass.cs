using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Requerimientos.Migrations
{
    /// <inheritdoc />
    public partial class RemovePerteneceIdFromKardexClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kardex_Pertenece",
                table: "Kardex");

            migrationBuilder.DropIndex(
                name: "IX_Kardex_PerteneceId",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "PerteneceId",
                table: "Kardex");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PerteneceId",
                table: "Kardex",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Kardex_PerteneceId",
                table: "Kardex",
                column: "PerteneceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kardex_Pertenece",
                table: "Kardex",
                column: "PerteneceId",
                principalTable: "Pertenece",
                principalColumn: "Id");
        }
    }
}
