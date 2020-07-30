using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.DataAccess.Migrations
{
    public partial class ImageUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoggedInDate",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastLoggedInDate",
                table: "Users");
        }
    }
}
