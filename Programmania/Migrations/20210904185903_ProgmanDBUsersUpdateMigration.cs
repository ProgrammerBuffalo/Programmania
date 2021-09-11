using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Programmania.Migrations
{
    public partial class ProgmanDBUsersUpdateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "stream_id",
                table: "Users",
                newName: "img_stream_id");

            migrationBuilder.AddColumn<Guid>(
                name: "hstr_stream_id",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hstr_stream_id",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "img_stream_id",
                table: "Users",
                newName: "stream_id");
        }
    }
}
