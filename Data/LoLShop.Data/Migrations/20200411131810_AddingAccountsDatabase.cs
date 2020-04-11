using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoLShop.Data.Migrations
{
    public partial class AddingAccountsDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ChampionsCount = table.Column<int>(nullable: false),
                    SkinsCount = table.Column<int>(nullable: false),
                    BlueEssence = table.Column<int>(nullable: false),
                    RiotPoints = table.Column<int>(nullable: false),
                    SellerId = table.Column<int>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
