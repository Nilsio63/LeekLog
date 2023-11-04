using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeekLog.Data.Migrations
{
    /// <inheritdoc />
    public partial class RepColumnRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfRepetitions",
                table: "WorkoutSetEntity",
                newName: "CleanRepetitions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CleanRepetitions",
                table: "WorkoutSetEntity",
                newName: "NumberOfRepetitions");
        }
    }
}
