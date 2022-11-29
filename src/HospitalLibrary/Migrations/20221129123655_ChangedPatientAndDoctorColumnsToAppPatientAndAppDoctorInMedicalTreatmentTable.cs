using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalLibrary.Migrations
{
    public partial class ChangedPatientAndDoctorColumnsToAppPatientAndAppDoctorInMedicalTreatmentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalTreatments_Users_DoctorId",
                table: "MedicalTreatments");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalTreatments_Users_PatientId",
                table: "MedicalTreatments");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalTreatments_AspNetUsers_DoctorId",
                table: "MedicalTreatments",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalTreatments_AspNetUsers_PatientId",
                table: "MedicalTreatments",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalTreatments_AspNetUsers_DoctorId",
                table: "MedicalTreatments");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalTreatments_AspNetUsers_PatientId",
                table: "MedicalTreatments");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalTreatments_Users_DoctorId",
                table: "MedicalTreatments",
                column: "DoctorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalTreatments_Users_PatientId",
                table: "MedicalTreatments",
                column: "PatientId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
