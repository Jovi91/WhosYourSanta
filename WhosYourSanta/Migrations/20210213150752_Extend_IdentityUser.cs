using Microsoft.EntityFrameworkCore.Migrations;

namespace WhosYourSanta.Migrations
{
    public partial class Extend_IdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Santas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Santas_AppUserId",
                table: "Santas",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Santas_AspNetUsers_AppUserId",
                table: "Santas",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Santas_AspNetUsers_AppUserId",
                table: "Santas");

            migrationBuilder.DropIndex(
                name: "IX_Santas_AppUserId",
                table: "Santas");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Santas");
        }
    }
}
