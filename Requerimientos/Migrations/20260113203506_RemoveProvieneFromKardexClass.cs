using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Requerimientos.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProvieneFromKardexClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Proviene",
                table: "Kardex");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Proviene",
                table: "Kardex",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
