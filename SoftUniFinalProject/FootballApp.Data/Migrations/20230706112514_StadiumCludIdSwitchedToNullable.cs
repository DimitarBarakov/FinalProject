using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballApp.Data.Migrations
{
    public partial class StadiumCludIdSwitchedToNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stadiums_Clubs_ClubId",
                table: "Stadiums");

            migrationBuilder.AlterColumn<int>(
                name: "ClubId",
                table: "Stadiums",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Stadiums_Clubs_ClubId",
                table: "Stadiums",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stadiums_Clubs_ClubId",
                table: "Stadiums");

            migrationBuilder.AlterColumn<int>(
                name: "ClubId",
                table: "Stadiums",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stadiums_Clubs_ClubId",
                table: "Stadiums",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
