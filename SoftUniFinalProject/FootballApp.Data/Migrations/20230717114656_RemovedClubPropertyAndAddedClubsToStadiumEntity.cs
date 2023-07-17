using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballApp.Data.Migrations
{
    public partial class RemovedClubPropertyAndAddedClubsToStadiumEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stadiums_Clubs_ClubId",
                table: "Stadiums");

            migrationBuilder.DropIndex(
                name: "IX_Stadiums_ClubId",
                table: "Stadiums");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Stadiums");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClubId",
                table: "Stadiums",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stadiums_ClubId",
                table: "Stadiums",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stadiums_Clubs_ClubId",
                table: "Stadiums",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id");
        }
    }
}
