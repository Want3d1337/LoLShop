using Microsoft.EntityFrameworkCore.Migrations;

namespace LoLShop.Data.Migrations
{
    public partial class BoosOrdersAddedBuyerIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BoosterId",
                table: "BoostOrders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoosterId",
                table: "BoostOrders");
        }
    }
}
