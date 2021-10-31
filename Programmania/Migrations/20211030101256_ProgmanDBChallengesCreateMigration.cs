using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Programmania.Migrations
{
    public partial class ProgmanDBChallengesCreateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Disciplines_DisciplineId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_TestId",
                table: "Lessons");

            migrationBuilder.AlterColumn<int>(
                name: "DisciplineId",
                table: "Lessons",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Challenge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChallengeTest",
                columns: table => new
                {
                    ChallengesId = table.Column<int>(type: "int", nullable: false),
                    TestsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeTest", x => new { x.ChallengesId, x.TestsId });
                    table.ForeignKey(
                        name: "FK_ChallengeTest_Challenge_ChallengesId",
                        column: x => x.ChallengesId,
                        principalTable: "Challenge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChallengeTest_Tests_TestsId",
                        column: x => x.TestsId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TestId",
                table: "Lessons",
                column: "TestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeTest_TestsId",
                table: "ChallengeTest",
                column: "TestsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Disciplines_DisciplineId",
                table: "Lessons",
                column: "DisciplineId",
                principalTable: "Disciplines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Disciplines_DisciplineId",
                table: "Lessons");

            migrationBuilder.DropTable(
                name: "ChallengeTest");

            migrationBuilder.DropTable(
                name: "Challenge");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_TestId",
                table: "Lessons");

            migrationBuilder.AlterColumn<int>(
                name: "DisciplineId",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TestId",
                table: "Lessons",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Disciplines_DisciplineId",
                table: "Lessons",
                column: "DisciplineId",
                principalTable: "Disciplines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
