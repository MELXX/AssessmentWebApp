using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssessmentWebApp.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "User",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "User",
                newName: "LoginEmail");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "User",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "User",
                newName: "FirstName");

            migrationBuilder.AddColumn<int>(
                name: "Identifier",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "Values",
                table: "User",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Identifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Values",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "User",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "LoginEmail",
                table: "User",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "User",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "User",
                newName: "Email");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "User",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");
        }
    }
}
