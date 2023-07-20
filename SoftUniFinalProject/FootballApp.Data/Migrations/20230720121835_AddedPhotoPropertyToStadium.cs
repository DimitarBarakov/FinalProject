using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballApp.Data.Migrations
{
    public partial class AddedPhotoPropertyToStadium : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Stadiums",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Stadiums");
        }
    }
}
