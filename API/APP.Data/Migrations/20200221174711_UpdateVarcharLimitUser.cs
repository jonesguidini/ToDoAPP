using Microsoft.EntityFrameworkCore.Migrations;

namespace APP.Data.Migrations
{
    public partial class UpdateVarcharLimitUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordSalt",
                table: "Users",
                type: "varchar(1000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "varchar(1000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordSalt",
                table: "Users",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1000)");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1000)");
        }
    }
}
