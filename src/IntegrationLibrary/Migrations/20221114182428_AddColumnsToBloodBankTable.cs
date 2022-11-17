using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace IntegrationLibrary.Migrations
{
    public partial class AddColumnsToBloodBankTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Frequently",
                table: "BloodBanks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReportFrom",
                table: "BloodBanks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReportTo",
                table: "BloodBanks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Frequently",
                table: "BloodBanks");

            migrationBuilder.DropColumn(
                name: "ReportFrom",
                table: "BloodBanks");

            migrationBuilder.DropColumn(
                name: "ReportTo",
                table: "BloodBanks");
        }
    }
}
