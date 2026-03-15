using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AarhusSpaceProgram.Api.Migrations
{
    /// <inheritdoc />
    public partial class _0001InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CelestialBody",
                columns: table => new
                {
                    body_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Distance = table.Column<int>(type: "int", nullable: false),
                    body_type = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    subtype = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    parent_body_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CelestialBody", x => x.body_id);
                    table.ForeignKey(
                        name: "FK_CelestialBody_CelestialBody_parent_body_id",
                        column: x => x.parent_body_id,
                        principalTable: "CelestialBody",
                        principalColumn: "body_id");
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    employee_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    hire_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.employee_id);
                });

            migrationBuilder.CreateTable(
                name: "LaunchPad",
                columns: table => new
                {
                    launchpad_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    max_supported_weight = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaunchPad", x => x.launchpad_id);
                });

            migrationBuilder.CreateTable(
                name: "Rocket",
                columns: table => new
                {
                    rocket_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    model_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    payload_cap = table.Column<int>(type: "int", nullable: false),
                    crew_cap = table.Column<int>(type: "int", nullable: false),
                    no_of_stages = table.Column<int>(type: "int", nullable: false),
                    fuel_cap = table.Column<int>(type: "int", nullable: false),
                    weight = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rocket", x => x.rocket_id);
                    table.CheckConstraint("CK_Rocket_Weight", "weight >= 0 ");
                });

            migrationBuilder.CreateTable(
                name: "Astronaut",
                columns: table => new
                {
                    employee_id = table.Column<int>(type: "int", nullable: false),
                    paygrade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    rank = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    hours_in_space = table.Column<int>(type: "int", nullable: false),
                    hours_in_sim = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Astronaut", x => x.employee_id);
                    table.ForeignKey(
                        name: "FK_Astronaut_Employee_employee_id",
                        column: x => x.employee_id,
                        principalTable: "Employee",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    employee_id = table.Column<int>(type: "int", nullable: false),
                    department = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.employee_id);
                    table.ForeignKey(
                        name: "FK_Manager_Employee_employee_id",
                        column: x => x.employee_id,
                        principalTable: "Employee",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scientist",
                columns: table => new
                {
                    employee_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    speciality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scientist", x => x.employee_id);
                    table.ForeignKey(
                        name: "FK_Scientist_Employee_employee_id",
                        column: x => x.employee_id,
                        principalTable: "Employee",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mission",
                columns: table => new
                {
                    mission_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    planned_launch_date = table.Column<DateOnly>(type: "date", nullable: false),
                    planned_duration = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "varchar(50)", nullable: false),
                    type = table.Column<string>(type: "varchar(50)", nullable: false),
                    manager_id = table.Column<int>(type: "int", nullable: false),
                    rocket_id = table.Column<int>(type: "int", nullable: false),
                    launchpad_id = table.Column<int>(type: "int", nullable: false),
                    target_body_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mission", x => x.mission_id);
                    table.ForeignKey(
                        name: "FK_Mission_CelestialBody_target_body_id",
                        column: x => x.target_body_id,
                        principalTable: "CelestialBody",
                        principalColumn: "body_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mission_LaunchPad_launchpad_id",
                        column: x => x.launchpad_id,
                        principalTable: "LaunchPad",
                        principalColumn: "launchpad_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mission_Manager_manager_id",
                        column: x => x.manager_id,
                        principalTable: "Manager",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mission_Rocket_rocket_id",
                        column: x => x.rocket_id,
                        principalTable: "Rocket",
                        principalColumn: "rocket_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mission_Astronaut",
                columns: table => new
                {
                    AstronautEmployeeId = table.Column<int>(type: "int", nullable: false),
                    MissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mission_Astronaut", x => new { x.AstronautEmployeeId, x.MissionId });
                    table.ForeignKey(
                        name: "FK_Mission_Astronaut_Astronaut_AstronautEmployeeId",
                        column: x => x.AstronautEmployeeId,
                        principalTable: "Astronaut",
                        principalColumn: "employee_id");
                    table.ForeignKey(
                        name: "FK_Mission_Astronaut_Mission_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Mission",
                        principalColumn: "mission_id");
                });

            migrationBuilder.CreateTable(
                name: "Mission_Scientist",
                columns: table => new
                {
                    MissionId = table.Column<int>(type: "int", nullable: false),
                    ScientistEmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mission_Scientist", x => new { x.MissionId, x.ScientistEmployeeId });
                    table.ForeignKey(
                        name: "FK_Mission_Scientist_Mission_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Mission",
                        principalColumn: "mission_id");
                    table.ForeignKey(
                        name: "FK_Mission_Scientist_Scientist_ScientistEmployeeId",
                        column: x => x.ScientistEmployeeId,
                        principalTable: "Scientist",
                        principalColumn: "employee_id");
                });

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
                    { 1, 1, 1, "Apollo 11", 8, new DateOnly(2010, 7, 16), 1, "Completed", 2, "Landing" },
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

            migrationBuilder.CreateIndex(
                name: "IX_CelestialBody_parent_body_id",
                table: "CelestialBody",
                column: "parent_body_id");

            migrationBuilder.CreateIndex(
                name: "IX_Mission_launchpad_id_planned_launch_date",
                table: "Mission",
                columns: new[] { "launchpad_id", "planned_launch_date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mission_manager_id",
                table: "Mission",
                column: "manager_id");

            migrationBuilder.CreateIndex(
                name: "IX_Mission_rocket_id",
                table: "Mission",
                column: "rocket_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mission_target_body_id",
                table: "Mission",
                column: "target_body_id");

            migrationBuilder.CreateIndex(
                name: "IX_Mission_Astronaut_MissionId",
                table: "Mission_Astronaut",
                column: "MissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Mission_Scientist_ScientistEmployeeId",
                table: "Mission_Scientist",
                column: "ScientistEmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mission_Astronaut");

            migrationBuilder.DropTable(
                name: "Mission_Scientist");

            migrationBuilder.DropTable(
                name: "Astronaut");

            migrationBuilder.DropTable(
                name: "Mission");

            migrationBuilder.DropTable(
                name: "Scientist");

            migrationBuilder.DropTable(
                name: "CelestialBody");

            migrationBuilder.DropTable(
                name: "LaunchPad");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropTable(
                name: "Rocket");

            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}
