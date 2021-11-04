using Microsoft.EntityFrameworkCore.Migrations;

namespace Programmania.Migrations
{
    public partial class ProgmanDBTestsFixMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lessons_TestId",
                table: "Lessons");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Tests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Challenges",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_CourseId",
                table: "Tests",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TestId",
                table: "Lessons",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_CourseId",
                table: "Challenges",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Challenges_Courses_CourseId",
                table: "Challenges",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Courses_CourseId",
                table: "Tests",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_Courses_CourseId",
                table: "Challenges");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Courses_CourseId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_CourseId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_TestId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Challenges_CourseId",
                table: "Challenges");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Challenges");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TestId",
                table: "Lessons",
                column: "TestId",
                unique: true);
        }
    }
}
