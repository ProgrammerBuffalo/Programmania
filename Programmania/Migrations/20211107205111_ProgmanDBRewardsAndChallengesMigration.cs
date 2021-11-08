using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Programmania.Migrations
{
    public partial class ProgmanDBRewardsAndChallengesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hstr_stream_id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "AnswersCount",
                table: "UsersChallenges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ChallengeStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    Loses = table.Column<int>(type: "int", nullable: false),
                    Draws = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChallengeStatistics_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rewards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rewards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rewards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeStatistics_UserId",
                table: "ChallengeStatistics",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rewards_UserId",
                table: "Rewards",
                column: "UserId");


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

            migrationBuilder.Sql(@"CREATE TRIGGER UpdateUserAfterDisciplineCompletion
                                    ON UsersDisciplines
                                    AFTER UPDATE
                                    AS
                                    begin
                                    	DECLARE @DisciplineId int;
                                    	DECLARE @LessonOrder int;
                                    	DECLARE @LastLessonOrder int;
                                    	DECLARE @UserId int;
                                    	DECLARE @DisciplineExp int;
                                    	DECLARE @DisciplineName nvarchar(max);
                                    
                                    	SELECT @DisciplineId = inserted.DisciplineId, @LessonOrder = inserted.LessonOrder, @UserId = inserted.UserId FROM inserted;
                                    	SELECT TOP(1) @LastLessonOrder = Lessons.[Order], @DisciplineExp = Disciplines.Points, @DisciplineName = Disciplines.Name
                                    							  FROM Lessons
                                    							  INNER JOIN Disciplines ON Lessons.DisciplineId = Disciplines.Id
                                    							  WHERE Lessons.DisciplineId = @DisciplineId
                                    							  ORDER BY Lessons.[Order] DESC
                                    	
                                    	IF(@LastLessonOrder <= @LessonOrder)
                                    	begin
                                    		UPDATE Users SET Users.Experience += @DisciplineExp WHERE Users.Id = @UserId;
                                    		INSERT INTO Rewards(Description, Experience, CreatedAt, UserId) 
                                    			   VALUES(CONCAT('Discipline', @DisciplineName, 'Completed'), @DisciplineExp, GETDATE(), @UserId);
                                    	end;
                                    end;");

            migrationBuilder.Sql(@"CREATE TRIGGER UpdateUserAfterChallengeWin
                                ON UsersChallenges
                                AFTER UPDATE
                                AS
                                begin
                                	DECLARE @User1Id int;
                                	DECLARE @User2Id int;
                                	DECLARE @User1Answers int;
                                	DECLARE @User2Answers int;
                                	DECLARE @ChallengeId int;
                                	DECLARE @User1StatisticsId int;
                                	DECLARE @User2StatisticsId int;
                                
                                	SELECT @User1Id = inserted.UserId, @ChallengeId = inserted.ChallengeId, @User1Answers = inserted.AnswersCount FROM inserted;
                                	
                                	SELECT @User2Id = Users.Id, @User2Answers = UsersChallenges.AnswersCount FROM UsersChallenges 
                                												INNER JOIN Challenges ON UsersChallenges.ChallengeId = Challenges.Id
                                												INNER JOIN Users ON UsersChallenges.UserId = Users.Id
                                												WHERE Challenges.Id = @ChallengeId AND UserId != @User1Id AND IsFinished = 1;
                                	
                                	SELECT @User1StatisticsId = Id FROM ChallengeStatistics WHERE UserId = @User1Id;
                                	SELECT @User2StatisticsId = Id FROM ChallengeStatistics WHERE UserId = @User2Id;
                                
                                	IF(@User1Answers > @User2Answers)
                                	begin
                                		UPDATE ChallengeStatistics SET Wins += 1 WHERE Id = @User1StatisticsId;
                                		UPDATE ChallengeStatistics SET Loses += 1 WHERE Id = @User2StatisticsId;
                                		UPDATE Users SET Users.Experience += 150 WHERE Users.Id = @User1Id;
                                		INSERT INTO Rewards(Description, Experience, CreatedAt, UserId) 
                                			   VALUES('Challenge won', 150, GETDATE(), @User1Id);
                                	end;
                                	ELSE IF(@User1Answers < @User2Answers)
                                	begin
                                		UPDATE ChallengeStatistics SET Loses += 1 WHERE Id = @User1StatisticsId;
                                		UPDATE ChallengeStatistics SET Wins += 1 WHERE Id = @User2StatisticsId;
                                		UPDATE Users SET Users.Experience += 150 WHERE Users.Id = @User2Id;
                                		INSERT INTO Rewards(Description, Experience, CreatedAt, UserId) 
                                			   VALUES('Challenge won', 150, GETDATE(), @User2Id);
                                	end;
                                	ELSE
                                	begin
                                		UPDATE ChallengeStatistics SET Draws += 1 WHERE Id = @User1StatisticsId;
                                		UPDATE ChallengeStatistics SET Draws += 1 WHERE Id = @User2StatisticsId;
                                		UPDATE Users SET Users.Experience += 75 WHERE Users.Id = @User1Id;
                                		UPDATE Users SET Users.Experience += 75 WHERE Users.Id = @User2Id;
                                		INSERT INTO Rewards(Description, Experience, CreatedAt, UserId) 
                                			   VALUES('Challenge draw', 75, GETDATE(), @User1Id);
                                		INSERT INTO Rewards(Description, Experience, CreatedAt, UserId) 
                                			   VALUES('Challenge draw', 75, GETDATE(), @User2Id);
                                	end;
                                end;");

            migrationBuilder.Sql(@"CREATE TRIGGER CreateStatisticsForUser
                                   ON Users
                                   AFTER INSERT
                                   AS
                                   begin
                                   	DECLARE @UserId int;

                                   	SELECT @UserId = inserted.Id FROM inserted;
                                   	INSERT INTO ChallengeStatistics(UserId) VALUES(@UserId);
                                   end;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChallengeStatistics");

            migrationBuilder.DropTable(
                name: "Rewards");

            migrationBuilder.DropColumn(
                name: "AnswersCount",
                table: "UsersChallenges");

            migrationBuilder.AddColumn<Guid>(
                name: "hstr_stream_id",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
