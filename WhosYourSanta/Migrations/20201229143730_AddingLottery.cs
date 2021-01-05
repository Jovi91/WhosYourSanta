using Microsoft.EntityFrameworkCore.Migrations;

namespace WhosYourSanta.Migrations
{
    public partial class AddingLottery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Santas",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LotteryId",
                table: "Santas",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Lottery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    AdminId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lottery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lottery_AspNetUsers_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Santas_LotteryId",
                table: "Santas",
                column: "LotteryId");

            migrationBuilder.CreateIndex(
                name: "IX_Lottery_AdminId",
                table: "Lottery",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Santas_Lottery_LotteryId",
                table: "Santas",
                column: "LotteryId",
                principalTable: "Lottery",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Santas_Lottery_LotteryId",
                table: "Santas");

            migrationBuilder.DropTable(
                name: "Lottery");

            migrationBuilder.DropIndex(
                name: "IX_Santas_LotteryId",
                table: "Santas");

            migrationBuilder.DropColumn(
                name: "LotteryId",
                table: "Santas");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Santas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
