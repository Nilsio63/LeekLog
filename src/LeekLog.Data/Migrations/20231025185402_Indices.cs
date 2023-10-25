using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeekLog.Data.Migrations
{
    /// <inheritdoc />
    public partial class Indices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Exercises",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSetEntity_SessionExerciseId_Order",
                table: "WorkoutSetEntity",
                columns: new[] { "SessionExerciseId", "Order" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SessionExercises_SessionId_Order",
                table: "SessionExercises",
                columns: new[] { "SessionId", "Order" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GymSessions_UserId_Date",
                table: "GymSessions",
                columns: new[] { "UserId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_Title",
                table: "Exercises",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkoutSetEntity_SessionExerciseId_Order",
                table: "WorkoutSetEntity");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserName",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_SessionExercises_SessionId_Order",
                table: "SessionExercises");

            migrationBuilder.DropIndex(
                name: "IX_GymSessions_UserId_Date",
                table: "GymSessions");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_Title",
                table: "Exercises");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Exercises",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
