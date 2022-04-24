using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelGuestSystem.Migrations
{
    public partial class datainit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerID = table.Column<string>(type: "TEXT", nullable: false),
                    ItemID = table.Column<int>(type: "INTEGER", nullable: false),
                    WantsASAP = table.Column<bool>(type: "INTEGER", nullable: false),
                    Delivered = table.Column<bool>(type: "INTEGER", nullable: false),
                    WantBy = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8c",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEKm6YkvCq1c+w2tVfVx/Kw+h0GSqzXfPLyU5bRmivMltU9soJaLXFzYmqG0Na+LPIQ==", "5467dc82-cf43-4865-bdab-d097d8880dd9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9df34-a133-43e2-8dd2-aef04ddb2b8c",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEGOUjWkG26NA7jWSPYPConAczl+aWLuDdx21u6kKO+zwuU0Iw52+kK5OLsQoHHpWhw==", "169ccdb1-714e-442b-b0b8-3834d0f5ef74" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8c",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEHm/NgZxfXN5RUndiZfI1nWgG1RzgS/5H1d05GDVAWrDwe3N7QuUs5C6rujrpso1+w==", "0aff56fe-02d5-4fa1-95b7-9b1a3007d8e7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9df34-a133-43e2-8dd2-aef04ddb2b8c",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEKo7eXZorM1J2jbzt2PjSyeMhD8zOAuniSpYnSKKTOVcjVReAtix63gGhGxLjfy8rA==", "3480109c-a7ae-4121-9def-aa9ada9ef56c" });
        }
    }
}
