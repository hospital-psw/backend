using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegrationLibrary.Migrations
{
    public partial class AlterIsDummyPasswordColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsDummyPassword",
                table: "BloodBanks",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "BloodBanks",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDummyPassword",
                value: false);

            migrationBuilder.UpdateData(
                table: "BloodBanks",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDummyPassword",
                value: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IsDummyPassword",
                table: "BloodBanks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "BloodBanks",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDummyPassword",
                value: null);

            migrationBuilder.UpdateData(
                table: "BloodBanks",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDummyPassword",
                value: null);
        }
    }
}
