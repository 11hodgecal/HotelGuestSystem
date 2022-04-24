using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelGuestSystem.Migrations
{
    public partial class datainit3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Requests",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "WantBy",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "WantsASAP",
                table: "Requests");

            migrationBuilder.RenameTable(
                name: "Requests",
                newName: "request");

            migrationBuilder.AddPrimaryKey(
                name: "PK_request",
                table: "request",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8c",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAENAHxG1Nk/J5XlUbkEQaKqpcK9BfsJZv9bC/ogBYdBBSq7iXj8mHRHa48vT0LfmfRQ==", "fc81f46a-3e05-40ea-883f-d5e1c0b078ad" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9df34-a133-43e2-8dd2-aef04ddb2b8c",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAELeyK1X2DmhyYRgn8jyeNNsvaX61ZSd+l7lJunoxfKiYROYCaWz9Dn10PTjFcxYHtA==", "2c8ae795-70d2-409d-8e82-234c38851dab" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_request",
                table: "request");

            migrationBuilder.RenameTable(
                name: "request",
                newName: "Requests");

            migrationBuilder.AddColumn<DateTime>(
                name: "WantBy",
                table: "Requests",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "WantsASAP",
                table: "Requests",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Requests",
                table: "Requests",
                column: "Id");

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
    }
}
