using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace APP.Data.Migrations
{
    public partial class AdicionaRefDeletedUserTodoEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordSalt",
                table: "Users",
                type: "varchar(1000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(65535)");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "varchar(1000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(65535)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeleted",
                table: "Todos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "Todos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Todos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Todos_DeletedByUserId",
                table: "Todos",
                column: "DeletedByUserId");

            migrationBuilder.AddForeignKey(
                name: "ToDo.Possui.UserDeleted",
                table: "Todos",
                column: "DeletedByUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "ToDo.Possui.UserDeleted",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_DeletedByUserId",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "DateDeleted",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Todos");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordSalt",
                table: "Users",
                type: "varchar(65535)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1000)");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "varchar(65535)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1000)");
        }
    }
}
