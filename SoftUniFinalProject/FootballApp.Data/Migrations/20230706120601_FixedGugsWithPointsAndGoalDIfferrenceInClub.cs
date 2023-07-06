using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballApp.Data.Migrations
{
    public partial class FixedGugsWithPointsAndGoalDIfferrenceInClub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcededGoals",
                table: "Clubs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConcededGoals",
                table: "Clubs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
