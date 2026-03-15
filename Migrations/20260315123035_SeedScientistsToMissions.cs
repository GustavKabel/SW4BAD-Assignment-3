using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AarhusSpaceProgram.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedScientistsToMissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Mission_Scientist",
                columns: new[] { "MissionId", "ScientistEmployeeId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Mission_Scientist",
                keyColumns: new[] { "MissionId", "ScientistEmployeeId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "Mission_Scientist",
                keyColumns: new[] { "MissionId", "ScientistEmployeeId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "Mission_Scientist",
                keyColumns: new[] { "MissionId", "ScientistEmployeeId" },
                keyValues: new object[] { 2, 3 });
        }
    }
}
