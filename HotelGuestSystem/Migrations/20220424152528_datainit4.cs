using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelGuestSystem.Migrations
{
    public partial class datainit4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "requestmade",
                table: "request",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9af34-a133-43e2-8dd2-aef04ddb2b8c",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEEJx9TtqBYLzDJAf2QWDkiMpkNHptSsTB/EAs8BQ7R8w/5ulY0p7z3NRVs1f5wI78g==", "ac61435a-d995-410c-9d72-a33e8cf9dd8e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27b9df34-a133-43e2-8dd2-aef04ddb2b8c",
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEAnfn+ZDZ7u4HAeN/1GRHLz5Zt5dlYAbnk8viYwWQh75iuj15T7R8svqSDgTBz6pOg==", "d4ffaad0-aedd-49bb-8ebc-0f7495b2efc6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "requestmade",
                table: "request");

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
    }
}
