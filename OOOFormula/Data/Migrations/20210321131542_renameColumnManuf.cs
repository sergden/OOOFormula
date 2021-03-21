using Microsoft.EntityFrameworkCore.Migrations;

namespace OOOFormula.Data.Migrations
{
    public partial class renameColumnManuf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Materials_MaterialsId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "MaterialsId",
                table: "Products",
                newName: "FacadeMaterialsId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_MaterialsId",
                table: "Products",
                newName: "IX_Products_FacadeMaterialsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Materials_FacadeMaterialsId",
                table: "Products",
                column: "FacadeMaterialsId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Materials_FacadeMaterialsId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "FacadeMaterialsId",
                table: "Products",
                newName: "MaterialsId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_FacadeMaterialsId",
                table: "Products",
                newName: "IX_Products_MaterialsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Materials_MaterialsId",
                table: "Products",
                column: "MaterialsId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
