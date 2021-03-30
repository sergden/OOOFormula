using Microsoft.EntityFrameworkCore.Migrations;

namespace OOOFormula.Data.Migrations
{
    public partial class ProfileCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Category_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Profile",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Profile_CategoryId",
            //    table: "Profile",
            //    column: "CategoryId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Profile_Category_CategoryId",
            //    table: "Profile",
            //    column: "CategoryId",
            //    principalTable: "Category",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profile_Category_CategoryId",
                table: "Profile");

            migrationBuilder.DropIndex(
                name: "IX_Profile_CategoryId",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Profile");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Products_CategoryId",
            //    table: "Products",
            //    column: "CategoryId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Products_Category_CategoryId",
            //    table: "Products",
            //    column: "CategoryId",
            //    principalTable: "Category",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
