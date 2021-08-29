using Microsoft.EntityFrameworkCore.Migrations;

namespace Programmania.Migrations
{
    public partial class ProgmanDBUserDisciplineMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonUser");

            migrationBuilder.AddColumn<int>(
                name: "DisciplineId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LessonCount",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UsersDisciplines",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DisciplineId = table.Column<int>(type: "int", nullable: false),
                    LessonOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersDisciplines", x => new { x.UserId, x.DisciplineId });
                    table.ForeignKey(
                        name: "FK_UsersDisciplines_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersDisciplines_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersDisciplines_DisciplineId",
                table: "UsersDisciplines",
                column: "DisciplineId");

            //Triggers for update lesson count of course
            
            migrationBuilder.Sql(@"CREATE TRIGGER CourseLessonIncrementation
                                   ON Lessons
                                   AFTER INSERT
                                   AS
                                   UPDATE Courses SET LessonCount += 1
			                       WHERE Courses.Id = (SELECT Disciplines.CourseId FROM inserted 
			                    					   INNER JOIN Disciplines on inserted.DisciplineId = Disciplines.Id)");

            migrationBuilder.Sql(@"CREATE TRIGGER CourseLessonDecrementation
                                   ON Lessons
                                   AFTER DELETE
                                   AS
                                   UPDATE Courses SET LessonCount -= 1
                                   			   WHERE Courses.Id = (SELECT Disciplines.CourseId FROM deleted 
                                   								   INNER JOIN Disciplines on deleted.DisciplineId = Disciplines.Id)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersDisciplines");

            migrationBuilder.DropColumn(
                name: "DisciplineId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LessonCount",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "LessonUser",
                columns: table => new
                {
                    LessonsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonUser", x => new { x.LessonsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_LessonUser_Lessons_LessonsId",
                        column: x => x.LessonsId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LessonUser_UsersId",
                table: "LessonUser",
                column: "UsersId");
        }
    }
}
