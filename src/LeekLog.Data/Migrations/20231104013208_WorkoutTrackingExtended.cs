using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeekLog.Data.Migrations
{
    /// <inheritdoc />
    public partial class WorkoutTrackingExtended : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FailedRepetitions",
                table: "WorkoutSetEntity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NegativeRepetitions",
                table: "WorkoutSetEntity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PartialRepetitions",
                table: "WorkoutSetEntity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UncleanRepetitions",
                table: "WorkoutSetEntity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "AverageHeartRate",
                table: "GymSessions",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "GymSessions",
                type: "time(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailedRepetitions",
                table: "WorkoutSetEntity");

            migrationBuilder.DropColumn(
                name: "NegativeRepetitions",
                table: "WorkoutSetEntity");

            migrationBuilder.DropColumn(
                name: "PartialRepetitions",
                table: "WorkoutSetEntity");

            migrationBuilder.DropColumn(
                name: "UncleanRepetitions",
                table: "WorkoutSetEntity");

            migrationBuilder.DropColumn(
                name: "AverageHeartRate",
                table: "GymSessions");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "GymSessions");
        }
    }
}
