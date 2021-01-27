using Microsoft.EntityFrameworkCore.Migrations;

namespace WhosYourSanta.Migrations
{
    public partial class sp_FindLotteriesByUserEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"Create procedure [dbo].[sp_FindLotteriesByUserEmail]
                      @email varchar(50)
                as 
                begin 
                    SET NOCOUNT ON;
                    Select LotteryId
                    from Santas
                    where Email=@email;
                end";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
