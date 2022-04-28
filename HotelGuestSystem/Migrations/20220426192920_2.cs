using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelGuestSystem.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8c",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEANDnQtFhz+N9a8p5BO3ucH5Pdmq7UqNcpE8DEGBOmm0bSC2kYa+GpXzCVcGIkUgCQ==", "5f7db12b-d3c1-4fcf-8b5e-fdbdedca7118" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9df34-a133-43e2-8dd2-aef04ddb2b8c",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEIW5pWOwaD+8W8rfZBeZlWP0CfpGcyxcEYSP6jNJNvCCFO9CxhN6chompT/iPbb8EA==", "94e91645-f080-4047-83b3-2f4c15531b56" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8c",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEDb1NTsmn7seQAuWREN09fQXD5JiKRBnWIAqBB9sugXuL1FYBPEKRN+s/vU63M53ug==", "1b0b8d7b-471c-4aab-93f5-c2aace30bd6f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9df34-a133-43e2-8dd2-aef04ddb2b8c",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEOpbcJaZ1PKoOmlQA3j0xtToXGqpQS7TRQchX96hREZ9Xildmiv3CBiqiuSfRVMduw==", "96f04254-6ceb-4e29-bec2-07cb4ec34ded" });
        }
    }
}
