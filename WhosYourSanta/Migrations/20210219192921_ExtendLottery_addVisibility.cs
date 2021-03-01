using Microsoft.EntityFrameworkCore.Migrations;

namespace WhosYourSanta.Migrations
{
    public partial class ExtendLottery_addVisibility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Visibility",
                table: "Lottery",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "Lottery");
        }
    }
}
