using Microsoft.EntityFrameworkCore.Migrations;

namespace MedRecordManager.Migrations
{
    public partial class Updateuserproperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "Application",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "Application",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                schema: "Application",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsernameChangeLimit",
                schema: "Application",
                table: "User",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "Application",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "Application",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                schema: "Application",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UsernameChangeLimit",
                schema: "Application",
                table: "User");
        }
    }
}
