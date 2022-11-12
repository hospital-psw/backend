using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace IntegrationLibrary.Migrations
{
    public partial class AddNewsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BloodBanks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BloodBanks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.InsertData(
                table: "BloodBanks",
                columns: new[] { "Id", "AdminPassword", "ApiKey", "ApiUrl", "DateCreated", "DateUpdated", "Deleted", "Email", "GetBloodTypeAndAmountAvailability", "GetBloodTypeAvailability", "IsDummyPassword", "Name" },
                values: new object[] { 1, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, false, "Blood bank 1" });

            migrationBuilder.InsertData(
                table: "BloodBanks",
                columns: new[] { "Id", "AdminPassword", "ApiKey", "ApiUrl", "DateCreated", "DateUpdated", "Deleted", "Email", "GetBloodTypeAndAmountAvailability", "GetBloodTypeAvailability", "IsDummyPassword", "Name" },
                values: new object[] { 2, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, false, "Crveni krst " });
        }
    }
}
