using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class CreatedExaminationEventsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DescriptionCreatedEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescriptionCreatedEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DescriptionCreatedEvents_DomainEvent_Id",
                        column: x => x.Id,
                        principalTable: "DomainEvent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExaminationFinishedEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FinishedAnamnesisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationFinishedEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExaminationFinishedEvents_DomainEvent_Id",
                        column: x => x.Id,
                        principalTable: "DomainEvent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExaminationStartedEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationStartedEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExaminationStartedEvents_DomainEvent_Id",
                        column: x => x.Id,
                        principalTable: "DomainEvent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionCreatedEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MedicamentId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionCreatedEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrescriptionCreatedEvents_DomainEvent_Id",
                        column: x => x.Id,
                        principalTable: "DomainEvent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionRemovedEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    PrescriptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionRemovedEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrescriptionRemovedEvents_DomainEvent_Id",
                        column: x => x.Id,
                        principalTable: "DomainEvent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SymptomsChangedEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SymptomId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymptomsChangedEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SymptomsChangedEvents_DomainEvent_Id",
                        column: x => x.Id,
                        principalTable: "DomainEvent",
                        principalColumn: "Id");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DescriptionCreatedEvents");

            migrationBuilder.DropTable(
                name: "ExaminationFinishedEvents");

            migrationBuilder.DropTable(
                name: "ExaminationStartedEvents");

            migrationBuilder.DropTable(
                name: "PrescriptionCreatedEvents");

            migrationBuilder.DropTable(
                name: "PrescriptionRemovedEvents");

            migrationBuilder.DropTable(
                name: "SymptomsChangedEvents");
        }
    }
}
