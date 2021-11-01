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

            //migrationBuilder.Sql()
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswersCount",
                table: "Challenges");
        }
    }
}
