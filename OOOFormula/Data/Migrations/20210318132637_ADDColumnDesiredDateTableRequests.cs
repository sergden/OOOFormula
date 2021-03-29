using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace OOOFormula.Data.Migrations
{
    public partial class ADDColumnDesiredDateTableRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DesiredDate",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DesiredDate",
                table: "Requests");
        }
    }
}
