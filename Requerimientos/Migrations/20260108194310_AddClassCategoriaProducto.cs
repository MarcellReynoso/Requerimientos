using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Requerimientos.Migrations
{
    /// <inheritdoc />
    public partial class AddClassCategoriaProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Prefijo = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Nombre = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Unidad = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    StockMinimo = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Proviene = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Condicion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    NroRequerimiento = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    AreaRequerimiento = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    PerteneceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Producto_Categoria",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Producto_Pertenece",
                        column: x => x.PerteneceId,
                        principalTable: "Pertenece",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_Prefijo",
                table: "Categoria",
                column: "Prefijo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Producto_CategoriaId",
                table: "Producto",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_Codigo",
                table: "Producto",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Producto_PerteneceId",
                table: "Producto",
                column: "PerteneceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
