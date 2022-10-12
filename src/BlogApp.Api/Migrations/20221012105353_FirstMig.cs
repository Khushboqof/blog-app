using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApp.Api.Migrations
{
    public partial class FirstMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "BlogPosts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "BlogPosts");
        }
    }
}
