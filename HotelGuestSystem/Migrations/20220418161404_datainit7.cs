using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelGuestSystem.Migrations
{
    public partial class datainit7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdate",
                table: "GBPConversionRates",
                newName: "NextUpdate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NextUpdate",
                table: "GBPConversionRates",
                newName: "LastUpdate");
        }
    }
}
