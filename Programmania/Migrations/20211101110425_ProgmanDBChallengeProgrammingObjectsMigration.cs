using Microsoft.EntityFrameworkCore.Migrations;

namespace Programmania.Migrations
{
    public partial class ProgmanDBChallengeProgrammingObjectsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnswersCount",
                table: "Challenges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(@"CREATE PROCEDURE dbo.SetChallengeForUsersPROC @creatorId int, @opponentId int, @challengeId int, @creatorAnswersCount int
                                AS
                                    INSERT INTO UsersChallenges(UserId, ChallengeId, IsFinished, AnswersCount) VALUES(@creatorId, @challengeId, 1, @creatorAnswersCount);
                                    INSERT INTO UsersChallenges(UserId, ChallengeId, IsFinished) VALUES(@opponentId, @challengeId, 0);
                                GO");

            migrationBuilder.Sql(@"CREATE PROCEDURE dbo.GenerateChallengePROC @courseId int,
										   @challengeId int output
                                   AS
                                   DECLARE @testsOfOneCourse table (
                                   	test_id int
                                   )
                                   
                                   INSERT INTO Challenges(Created, CourseId) VALUES(GETDATE(), @courseId);
                                   SET @challengeId = SCOPE_IDENTITY();
                                   
                                   INSERT INTO @testsOfOneCourse SELECT Tests.Id FROM Tests 
                                   							  INNER JOIN Courses ON Courses.Id = Tests.CourseId
                                   							  WHERE Courses.Id = @courseId;
                                   
                                   INSERT INTO ChallengeTest(ChallengesId, TestsId)  SELECT @challengeId, t.test_id FROM @testsOfOneCourse t
                                   												  GROUP BY t.test_id
                                   												  ORDER BY NEWID();
                                   GO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswersCount",
                table: "Challenges");
        }
    }
}
