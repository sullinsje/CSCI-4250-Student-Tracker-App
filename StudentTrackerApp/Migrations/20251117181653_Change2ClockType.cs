using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentTrackerApp.Migrations
{
    /// <inheritdoc />
    public partial class Change2ClockType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InOut",
                table: "AttendanceRecords",
                newName: "ClockType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClockType",
                table: "AttendanceRecords",
                newName: "InOut");
        }
    }
}
