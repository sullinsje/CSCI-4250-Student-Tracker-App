using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentTrackerApp.Migrations
{
    /// <inheritdoc />
    public partial class AddInOut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InOut",
                table: "AttendanceRecords",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InOut",
                table: "AttendanceRecords");
        }
    }
}
