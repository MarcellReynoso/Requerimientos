using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Requerimientos.Migrations
{
    /// <inheritdoc />
    public partial class AddClassTrabajador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trabajador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    DNI = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: true),
                    Clase = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Cargo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    NroBBVA = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CCIBBVA = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    NroBCP = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CCIBCP = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    NroInterbank = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CCIInterbank = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabajador", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trabajador");
        }
    }
}
