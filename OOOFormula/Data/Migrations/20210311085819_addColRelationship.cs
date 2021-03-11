using Microsoft.EntityFrameworkCore.Migrations;

namespace OOOFormula.Data.Migrations
{
    public partial class addColRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductsId",
                table: "ProductImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductsId",
                table: "ProductImages",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductsId",
                table: "ProductImages",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductsId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductsId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                table: "ProductImages");
        }
    }
}
