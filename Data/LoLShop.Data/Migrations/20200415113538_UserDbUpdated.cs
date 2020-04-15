using Microsoft.EntityFrameworkCore.Migrations;

namespace LoLShop.Data.Migrations
{
    public partial class UserDbUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApprovedAccounts",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AvatarImageId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Champions",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rank",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedAccounts",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AvatarImageId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Champions",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "AspNetUsers");
        }
    }
}
