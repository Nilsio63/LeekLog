using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeekLog.Data.Migrations
{
    /// <inheritdoc />
    public partial class AssistedReps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssistedRepetitions",
                table: "WorkoutSetEntity",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssistedRepetitions",
                table: "WorkoutSetEntity");
        }
    }
}
