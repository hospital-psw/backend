using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegrationLibrary.Migrations
{
    public partial class CreateTenderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "TenderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BloodType = table.Column<int>(type: "int", nullable: false),
                    Money = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    TenderId = table.Column<int>(type: "int", nullable: true),
                    TenderOfferId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenderOffer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferorId = table.Column<int>(type: "int", nullable: true),
                    TenderId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderOffer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenderOffer_BloodBanks_OfferorId",
                        column: x => x.OfferorId,
                        principalTable: "BloodBanks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tenders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenderWinnerId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tenders_TenderOffer_TenderWinnerId",
                        column: x => x.TenderWinnerId,
                        principalTable: "TenderOffer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TenderItem_TenderId",
                table: "TenderItem",
                column: "TenderId");

            migrationBuilder.CreateIndex(
                name: "IX_TenderItem_TenderOfferId",
                table: "TenderItem",
                column: "TenderOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_TenderOffer_OfferorId",
                table: "TenderOffer",
                column: "OfferorId");

            migrationBuilder.CreateIndex(
                name: "IX_TenderOffer_TenderId",
                table: "TenderOffer",
                column: "TenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenders_TenderWinnerId",
                table: "Tenders",
                column: "TenderWinnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TenderItem_TenderOffer_TenderOfferId",
                table: "TenderItem",
                column: "TenderOfferId",
                principalTable: "TenderOffer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TenderItem_Tenders_TenderId",
                table: "TenderItem",
                column: "TenderId",
                principalTable: "Tenders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TenderOffer_Tenders_TenderId",
                table: "TenderOffer",
                column: "TenderId",
                principalTable: "Tenders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenders_TenderOffer_TenderWinnerId",
                table: "Tenders");

            migrationBuilder.DropTable(
                name: "TenderItem");

            migrationBuilder.DropTable(
                name: "TenderOffer");

            migrationBuilder.DropTable(
                name: "Tenders");
        }
    }
}
