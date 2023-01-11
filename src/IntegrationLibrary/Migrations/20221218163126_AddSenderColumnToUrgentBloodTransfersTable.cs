using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegrationLibrary.Migrations
{
    public partial class AddSenderColumnToUrgentBloodTransfersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_urgentBloodTransfers",
                table: "urgentBloodTransfers");

            migrationBuilder.RenameTable(
                name: "urgentBloodTransfers",
                newName: "UrgentBloodTransfers");

            migrationBuilder.AddColumn<int>(
                name: "SenderId",
                table: "UrgentBloodTransfers",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UrgentBloodTransfers",
                table: "UrgentBloodTransfers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UrgentBloodTransfers_SenderId",
                table: "UrgentBloodTransfers",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_UrgentBloodTransfers_BloodBanks_SenderId",
                table: "UrgentBloodTransfers",
                column: "SenderId",
                principalTable: "BloodBanks",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UrgentBloodTransfers_BloodBanks_SenderId",
                table: "UrgentBloodTransfers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UrgentBloodTransfers",
                table: "UrgentBloodTransfers");

            migrationBuilder.DropIndex(
                name: "IX_UrgentBloodTransfers_SenderId",
                table: "UrgentBloodTransfers");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "UrgentBloodTransfers");

            migrationBuilder.RenameTable(
                name: "UrgentBloodTransfers",
                newName: "urgentBloodTransfers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_urgentBloodTransfers",
                table: "urgentBloodTransfers",
                column: "Id");
        }
    }
}
