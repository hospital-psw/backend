using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class CreatedAnamnesesTableAndAnamnesesSymptomTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnamnesisId",
                table: "Prescriptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Anamneses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anamneses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Anamneses_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AnamnesisSymptom",
                columns: table => new
                {
                    AnamnesesId = table.Column<int>(type: "int", nullable: false),
                    SymptomsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnamnesisSymptom", x => new { x.AnamnesesId, x.SymptomsId });
                    table.ForeignKey(
                        name: "FK_AnamnesisSymptom_Anamneses_AnamnesesId",
                        column: x => x.AnamnesesId,
                        principalTable: "Anamneses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnamnesisSymptom_Symptoms_SymptomsId",
                        column: x => x.SymptomsId,
                        principalTable: "Symptoms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_AnamnesisId",
                table: "Prescriptions",
                column: "AnamnesisId");

            migrationBuilder.CreateIndex(
                name: "IX_Anamneses_AppointmentId",
                table: "Anamneses",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AnamnesisSymptom_SymptomsId",
                table: "AnamnesisSymptom",
                column: "SymptomsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Anamneses_AnamnesisId",
                table: "Prescriptions",
                column: "AnamnesisId",
                principalTable: "Anamneses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Anamneses_AnamnesisId",
                table: "Prescriptions");

            migrationBuilder.DropTable(
                name: "AnamnesisSymptom");

            migrationBuilder.DropTable(
                name: "Anamneses");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_AnamnesisId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "AnamnesisId",
                table: "Prescriptions");
        }
    }
}
