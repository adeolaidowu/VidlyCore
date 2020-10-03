using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VidlyCore.Migrations
{
    public partial class AddBirthDateFieldAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Customers",
                nullable: true,
                 defaultValue: new DateTime(1, 1, 1));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Customers");
        }
    }
}
