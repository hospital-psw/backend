using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegrationLibrary.Migrations
{
    public partial class AddColumnForMonthlyTransferToBloodBank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MonthlyTransfer_ABMinus",
                table: "BloodBanks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MonthlyTransfer_ABPlus",
                table: "BloodBanks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MonthlyTransfer_AMinus",
                table: "BloodBanks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MonthlyTransfer_APlus",
                table: "BloodBanks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MonthlyTransfer_BMinus",
                table: "BloodBanks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MonthlyTransfer_BPlus",
                table: "BloodBanks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MonthlyTransfer_DateTime",
                table: "BloodBanks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MonthlyTransfer_OMinus",
                table: "BloodBanks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MonthlyTransfer_OPlus",
                table: "BloodBanks",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthlyTransfer_ABMinus",
                table: "BloodBanks");

            migrationBuilder.DropColumn(
                name: "MonthlyTransfer_ABPlus",
                table: "BloodBanks");

            migrationBuilder.DropColumn(
                name: "MonthlyTransfer_AMinus",
                table: "BloodBanks");

            migrationBuilder.DropColumn(
                name: "MonthlyTransfer_APlus",
                table: "BloodBanks");

            migrationBuilder.DropColumn(
                name: "MonthlyTransfer_BMinus",
                table: "BloodBanks");

            migrationBuilder.DropColumn(
                name: "MonthlyTransfer_BPlus",
                table: "BloodBanks");

            migrationBuilder.DropColumn(
                name: "MonthlyTransfer_DateTime",
                table: "BloodBanks");

            migrationBuilder.DropColumn(
                name: "MonthlyTransfer_OMinus",
                table: "BloodBanks");

            migrationBuilder.DropColumn(
                name: "MonthlyTransfer_OPlus",
                table: "BloodBanks");
        }
    }
}
