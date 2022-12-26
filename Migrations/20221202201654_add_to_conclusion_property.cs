using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Migrations
{
    public partial class add_to_conclusion_property : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Complaints",
                table: "сonclusions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Recommendations",
                table: "сonclusions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "poolIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id1 = table.Column<int>(type: "int", nullable: true),
                    id2 = table.Column<int>(type: "int", nullable: true),
                    id3 = table.Column<int>(type: "int", nullable: true),
                    id4 = table.Column<int>(type: "int", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "poolIds");

            migrationBuilder.DropColumn(
                name: "Complaints",
                table: "сonclusions");

            migrationBuilder.DropColumn(
                name: "Recommendations",
                table: "сonclusions");
        }
    }
}
