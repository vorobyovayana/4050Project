using Microsoft.EntityFrameworkCore.Migrations;

namespace FinaiProejct_200OK.Migrations
{
    public partial class add_Password_To_Database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GenreName",
                table: "Genre",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DirectorName",
                table: "Director",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "User");

            migrationBuilder.DropColumn(
                name: "GenreName",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "DirectorName",
                table: "Director");
        }
    }
}
