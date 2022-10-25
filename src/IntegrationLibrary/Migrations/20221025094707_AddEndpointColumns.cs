using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegrationLibrary.Migrations
{
    public partial class AddEndpointColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GetBloodTypeAndAmountAvailability",
                table: "BloodBanks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GetBloodTypeAvailability",
                table: "BloodBanks",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GetBloodTypeAndAmountAvailability",
                table: "BloodBanks");

            migrationBuilder.DropColumn(
                name: "GetBloodTypeAvailability",
                table: "BloodBanks");
        }
    }
}