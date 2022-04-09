using Microsoft.EntityFrameworkCore.Migrations;

namespace FinaiProejct_200OK.Migrations
{
    public partial class addCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MovieDescription",
                table: "Movie",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "posterPath",
                table: "IMDBData",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieDescription",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "posterPath",
                table: "IMDBData");
        }
    }
}
