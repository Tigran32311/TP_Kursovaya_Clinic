using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Migrations
{
    public partial class add_table_times : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "poolIds");

            migrationBuilder.AddColumn<int>(
                name: "WorkShift",
                table: "doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "timeId",
                table: "appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "times",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    timeOnlyMorning = table.Column<DateTime>(type: "datetime2", nullable: false),
                    timeOnlyEvening = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_times", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointments_timeId",
                table: "appointments",
                column: "timeId");

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_times_timeId",
                table: "appointments",
                column: "timeId",
                principalTable: "times",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointments_times_timeId",
                table: "appointments");

            migrationBuilder.DropTable(
                name: "times");

            migrationBuilder.DropIndex(
                name: "IX_appointments_timeId",
                table: "appointments");

            migrationBuilder.DropColumn(
                name: "WorkShift",
                table: "doctors");

            migrationBuilder.DropColumn(
                name: "timeId",
                table: "appointments");

            migrationBuilder.CreateTable(
                name: "poolIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    id1 = table.Column<int>(type: "int", nullable: true),
                    id2 = table.Column<int>(type: "int", nullable: true),
                    id3 = table.Column<int>(type: "int", nullable: true),
                    id4 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_poolIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_poolIds_patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_poolIds_PatientId",
                table: "poolIds",
                column: "PatientId");
        }
    }
}
