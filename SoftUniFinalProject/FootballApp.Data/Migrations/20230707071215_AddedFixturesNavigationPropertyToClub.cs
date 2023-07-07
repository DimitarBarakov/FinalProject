using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballApp.Data.Migrations
{
    public partial class AddedFixturesNavigationPropertyToClub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Clubs_AwayClubId",
                table: "Fixtures");

            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Clubs_HomeClubId",
                table: "Fixtures");

            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Stadiums_StadiumId",
                table: "Fixtures");

            migrationBuilder.DropIndex(
                name: "IX_Fixtures_StadiumId",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "StadiumId",
                table: "Fixtures");

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Clubs_AwayClubId",
                table: "Fixtures",
                column: "AwayClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Clubs_HomeClubId",
                table: "Fixtures",
                column: "HomeClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Clubs_AwayClubId",
                table: "Fixtures");

            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Clubs_HomeClubId",
                table: "Fixtures");

            migrationBuilder.AddColumn<int>(
                name: "StadiumId",
                table: "Fixtures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_StadiumId",
                table: "Fixtures",
                column: "StadiumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Clubs_AwayClubId",
                table: "Fixtures",
                column: "AwayClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Clubs_HomeClubId",
                table: "Fixtures",
                column: "HomeClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Stadiums_StadiumId",
                table: "Fixtures",
                column: "StadiumId",
                principalTable: "Stadiums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
