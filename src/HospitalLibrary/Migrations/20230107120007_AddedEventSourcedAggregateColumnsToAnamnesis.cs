using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class AddedEventSourcedAggregateColumnsToAnamnesis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnamnesisId",
                table: "DomainEvent",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Anamneses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DomainEvent_AnamnesisId",
                table: "DomainEvent",
                column: "AnamnesisId");

            migrationBuilder.AddForeignKey(
                name: "FK_DomainEvent_Anamneses_AnamnesisId",
                table: "DomainEvent",
                column: "AnamnesisId",
                principalTable: "Anamneses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DomainEvent_Anamneses_AnamnesisId",
                table: "DomainEvent");

            migrationBuilder.DropIndex(
                name: "IX_DomainEvent_AnamnesisId",
                table: "DomainEvent");

            migrationBuilder.DropColumn(
                name: "AnamnesisId",
                table: "DomainEvent");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Anamneses");
        }
    }
}
