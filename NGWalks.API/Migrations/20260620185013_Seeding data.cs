using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class Seedingdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("69c9925f-10ad-4f5c-8c60-1963b389fd64"), "Easy" },
                    { new Guid("80e5a420-b2fb-46b2-82d7-13e4f12bc2ea"), "Medium" },
                    { new Guid("c46296e9-6719-4508-a372-869cd42ce640"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("387f950e-36c9-4e81-b28c-ea243a6af58c"), "NL", "Northland", "image.url" },
                    { new Guid("609c5351-3f07-4d88-ba23-9b043998067c"), "AKL", "Auckland", "image1.url" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("69c9925f-10ad-4f5c-8c60-1963b389fd64"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("80e5a420-b2fb-46b2-82d7-13e4f12bc2ea"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("c46296e9-6719-4508-a372-869cd42ce640"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("387f950e-36c9-4e81-b28c-ea243a6af58c"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("609c5351-3f07-4d88-ba23-9b043998067c"));
        }
    }
}
