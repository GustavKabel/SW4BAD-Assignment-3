using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AarhusSpaceProgram.Api.Migrations
{
    /// <inheritdoc />
    public partial class _0003SmallFixesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mission_Manager_manager_id",
                table: "Mission");

            migrationBuilder.AlterColumn<int>(
                name: "manager_id",
                table: "Mission",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Mission_Manager_manager_id",
                table: "Mission",
                column: "manager_id",
                principalTable: "Manager",
                principalColumn: "employee_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mission_Manager_manager_id",
                table: "Mission");

            migrationBuilder.AlterColumn<int>(
                name: "manager_id",
                table: "Mission",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Mission_Manager_manager_id",
                table: "Mission",
                column: "manager_id",
                principalTable: "Manager",
                principalColumn: "employee_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
