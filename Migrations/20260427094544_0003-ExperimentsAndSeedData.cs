using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AarhusSpaceProgram.Api.Migrations
{
    /// <inheritdoc />
    public partial class _0003ExperimentsAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CelestialBody",
                keyColumn: "body_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Mission_Astronaut",
                keyColumns: new[] { "AstronautEmployeeId", "MissionId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "Mission_Astronaut",
                keyColumns: new[] { "AstronautEmployeeId", "MissionId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "Mission_Astronaut",
                keyColumns: new[] { "AstronautEmployeeId", "MissionId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "Mission_Astronaut",
                keyColumns: new[] { "AstronautEmployeeId", "MissionId" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "Mission_Astronaut",
                keyColumns: new[] { "AstronautEmployeeId", "MissionId" },
                keyValues: new object[] { 7, 2 });

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

            migrationBuilder.DeleteData(
                table: "Astronaut",
                keyColumn: "employee_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Astronaut",
                keyColumn: "employee_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Astronaut",
                keyColumn: "employee_id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Mission",
                keyColumn: "mission_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Mission",
                keyColumn: "mission_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Scientist",
                keyColumn: "employee_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Scientist",
                keyColumn: "employee_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CelestialBody",
                keyColumn: "body_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "employee_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "employee_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "employee_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "employee_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "employee_id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "LaunchPad",
                keyColumn: "launchpad_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LaunchPad",
                keyColumn: "launchpad_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Manager",
                keyColumn: "employee_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Manager",
                keyColumn: "employee_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rocket",
                keyColumn: "rocket_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rocket",
                keyColumn: "rocket_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CelestialBody",
                keyColumn: "body_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "employee_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "employee_id",
                keyValue: 2);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Employee",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Experiments",
                columns: table => new
                {
                    experiment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    creation_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MissionId = table.Column<int>(type: "int", nullable: false),
                    ScientistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiments", x => x.experiment_id);
                    table.ForeignKey(
                        name: "FK_Experiments_Mission_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Mission",
                        principalColumn: "mission_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Experiments_Scientist_ScientistId",
                        column: x => x.ScientistId,
                        principalTable: "Scientist",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_AppUserId",
                table: "Employee",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiments_MissionId",
                table: "Experiments",
                column: "MissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiments_ScientistId",
                table: "Experiments",
                column: "ScientistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_AspNetUsers_AppUserId",
                table: "Employee",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_AspNetUsers_AppUserId",
                table: "Employee");

            migrationBuilder.DropTable(
                name: "Experiments");

            migrationBuilder.DropIndex(
                name: "IX_Employee_AppUserId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Employee");

            migrationBuilder.InsertData(
                table: "CelestialBody",
                columns: new[] { "body_id", "body_type", "Distance", "name", "parent_body_id", "subtype" },
                values: new object[,]
                {
                    { 1, "Planet", 0, "Earth", null, "RockyPlanet" },
                    { 3, "Planet", 225000000, "Mars", null, "RockyPlanet" }
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "employee_id", "hire_date", "name" },
                values: new object[,]
                {
                    { 1, new DateOnly(2001, 1, 1), "Dumbledore" },
                    { 2, new DateOnly(2002, 1, 1), "Voldemord" },
                    { 3, new DateOnly(2004, 2, 5), "Hermoine Granger" },
                    { 4, new DateOnly(2004, 1, 1), "Harry Potter" },
                    { 5, new DateOnly(2002, 1, 1), "Ron Weasley" },
                    { 6, new DateOnly(2003, 1, 1), "Neville Longbottom" },
                    { 7, new DateOnly(2004, 1, 1), "Draco Malfoy" }
                });

            migrationBuilder.InsertData(
                table: "LaunchPad",
                columns: new[] { "launchpad_id", "location", "max_supported_weight", "status" },
                values: new object[,]
                {
                    { 1, "Hogwartz", 3000000.0, "Active" },
                    { 2, "The forbidden forest", 4000000.0, "Active" }
                });

            migrationBuilder.InsertData(
                table: "Rocket",
                columns: new[] { "rocket_id", "crew_cap", "fuel_cap", "model_name", "no_of_stages", "payload_cap", "weight" },
                values: new object[,]
                {
                    { 1, 3, 200000, "Superfly", 3, 14000, 296000.0 },
                    { 2, 2, 150000, "BC Rocket", 2, 13000, 230000.0 }
                });

            migrationBuilder.InsertData(
                table: "Astronaut",
                columns: new[] { "employee_id", "hours_in_sim", "hours_in_space", "paygrade", "rank" },
                values: new object[,]
                {
                    { 5, 600, 200, "0-8", "Commander" },
                    { 6, 500, 150, "0-7", "Pilot" },
                    { 7, 550, 100, "0-7", "Pilot" }
                });

            migrationBuilder.InsertData(
                table: "CelestialBody",
                columns: new[] { "body_id", "body_type", "Distance", "name", "parent_body_id", "subtype" },
                values: new object[] { 2, "Moon", 384400, "Moon", 1, "None" });

            migrationBuilder.InsertData(
                table: "Manager",
                columns: new[] { "employee_id", "department" },
                values: new object[,]
                {
                    { 1, "Flight control" },
                    { 2, "Engineering" }
                });

            migrationBuilder.InsertData(
                table: "Scientist",
                columns: new[] { "employee_id", "speciality", "title" },
                values: new object[,]
                {
                    { 3, "Mars", "Astronomer" },
                    { 4, "Alien life", "Alien investigator" }
                });

            migrationBuilder.InsertData(
                table: "Mission",
                columns: new[] { "mission_id", "launchpad_id", "manager_id", "name", "planned_duration", "planned_launch_date", "rocket_id", "status", "target_body_id", "type" },
                values: new object[,]
                {
                    { 1, 1, 1, "Apollo 11", 8, new DateOnly(2010, 7, 16), 1, "Completed", 1, "Landing" },
                    { 2, 2, 2, "Artemis I", 25, new DateOnly(2025, 11, 16), 2, "Active", 2, "Orbiting" }
                });

            migrationBuilder.InsertData(
                table: "Mission_Astronaut",
                columns: new[] { "AstronautEmployeeId", "MissionId" },
                values: new object[,]
                {
                    { 5, 1 },
                    { 6, 1 },
                    { 6, 2 },
                    { 7, 1 },
                    { 7, 2 }
                });

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
    }
}
