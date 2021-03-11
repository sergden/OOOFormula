using Microsoft.EntityFrameworkCore.Migrations;

namespace OOOFormula.Data.Migrations
{
    public partial class deletecol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdProduct",
                table: "ProductImages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdProduct",
                table: "ProductImages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
