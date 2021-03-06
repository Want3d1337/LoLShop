﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoLShop.Data.Migrations
{
    public partial class DbApprovedAccountsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApprovedAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    SellerId = table.Column<string>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Region = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovedAccounts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovedAccounts");
        }
    }
}
