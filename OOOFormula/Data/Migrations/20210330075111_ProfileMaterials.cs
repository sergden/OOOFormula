using Microsoft.EntityFrameworkCore.Migrations;

namespace OOOFormula.Data.Migrations
{
    public partial class ProfileMaterials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Materials_FacadeMaterialsId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_FacadeMaterialsId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FacadeMaterialsId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "FacadeMaterialsId",
                table: "Profile",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Profile_FacadeMaterialsId",
            //    table: "Profile",
            //    column: "FacadeMaterialsId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Profile_Materials_FacadeMaterialsId",
            //    table: "Profile",
            //    column: "FacadeMaterialsId",
            //    principalTable: "Materials",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profile_Materials_FacadeMaterialsId",
                table: "Profile");

            migrationBuilder.DropIndex(
                name: "IX_Profile_FacadeMaterialsId",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "FacadeMaterialsId",
                table: "Profile");

            migrationBuilder.AddColumn<int>(
                name: "FacadeMaterialsId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Products_FacadeMaterialsId",
            //    table: "Products",
            //    column: "FacadeMaterialsId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Products_Materials_FacadeMaterialsId",
            //    table: "Products",
            //    column: "FacadeMaterialsId",
            //    principalTable: "Materials",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
