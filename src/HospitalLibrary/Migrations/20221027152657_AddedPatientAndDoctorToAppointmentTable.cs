using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class AddedPatientAndDoctorToAppointmentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_DoctorId",
                table: "Appointments",
                column: "DoctorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Users",
                principalColumn: "Id");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_DoctorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_PatientId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Appointments");

        }
    }
}
