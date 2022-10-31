using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegrationLibrary.Migrations
{
    public partial class AddPasswordAndDummyFlagColumnsToBloodBankTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminPassword",
                table: "BloodBanks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsDummyPassword",
                table: "BloodBanks",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminPassword",
                table: "BloodBanks");

            migrationBuilder.DropColumn(
                name: "IsDummyPassword",
                table: "BloodBanks");
        }
    }
}
