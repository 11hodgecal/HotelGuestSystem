using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelGuestSystem.Migrations
{
    public partial class datainit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GBPConversionRates",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GDPToUSDRate = table.Column<double>(type: "REAL", nullable: false),
                    GDPToEuroRate = table.Column<double>(type: "REAL", nullable: false),
                    GDPToYenRate = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GBPConversionRates", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GBPConversionRates");
        }
    }
}
