using Microsoft.EntityFrameworkCore.Migrations;

namespace LoLShop.Data.Migrations
{
    public partial class UsersDbAvatarNameUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarImageId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AvatarImageUrl",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarImageUrl",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AvatarImageId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
