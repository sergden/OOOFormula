using Microsoft.EntityFrameworkCore.Migrations;

namespace OOOFormula.Data.Migrations
{
    public partial class ProfileManuf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Manufacturers_FurnitureManufacturersId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_FurnitureManufacturersId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FurnitureManufacturersId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "FurnitureManufacturersId",
                table: "Profile",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Profile_FurnitureManufacturersId",
            //    table: "Profile",
            //    column: "FurnitureManufacturersId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Profile_Manufacturers_FurnitureManufacturersId",
            //    table: "Profile",
            //    column: "FurnitureManufacturersId",
            //    principalTable: "Manufacturers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profile_Manufacturers_FurnitureManufacturersId",
                table: "Profile");

            migrationBuilder.DropIndex(
                name: "IX_Profile_FurnitureManufacturersId",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "FurnitureManufacturersId",
                table: "Profile");

            migrationBuilder.AddColumn<int>(
                name: "FurnitureManufacturersId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Products_FurnitureManufacturersId",
            //    table: "Products",
            //    column: "FurnitureManufacturersId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Products_Manufacturers_FurnitureManufacturersId",
            //    table: "Products",
            //    column: "FurnitureManufacturersId",
            //    principalTable: "Manufacturers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
