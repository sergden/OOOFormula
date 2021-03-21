using Microsoft.EntityFrameworkCore.Migrations;

namespace OOOFormula.Data.Migrations
{
    public partial class renameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Manufacturers_ManufacturersId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ManufacturersId",
                table: "Products",
                newName: "FurnitureManufacturersId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ManufacturersId",
                table: "Products",
                newName: "IX_Products_FurnitureManufacturersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Manufacturers_FurnitureManufacturersId",
                table: "Products",
                column: "FurnitureManufacturersId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Manufacturers_FurnitureManufacturersId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "FurnitureManufacturersId",
                table: "Products",
                newName: "ManufacturersId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_FurnitureManufacturersId",
                table: "Products",
                newName: "IX_Products_ManufacturersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Manufacturers_ManufacturersId",
                table: "Products",
                column: "ManufacturersId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
