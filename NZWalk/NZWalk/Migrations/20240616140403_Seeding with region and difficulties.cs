using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalk.Migrations
{
    /// <inheritdoc />
    public partial class Seedingwithregionanddifficulties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("405662c8-fb7a-4ea2-82af-9dbb5b50c05b"), "Hard" },
                    { new Guid("a2d9cff7-c4ff-4078-9a32-23c4d2091b80"), "Medium" },
                    { new Guid("a5152b2a-65d0-4ce4-aba5-a5965795f7ac"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("00a5f071-1269-4287-9705-dbf8d6b2a4d1"), "BK", "Bankok", "Null" },
                    { new Guid("07a6055c-9414-4ea1-aaa3-6f2b49eb085f"), "THI", "Thiland", "Null" },
                    { new Guid("2547fc32-2817-489b-8907-ae45ee2b2913"), "Cal", "Californea", "Null" },
                    { new Guid("62ce941a-f207-4222-a6cd-b7ce288c4caa"), "Al", "Ackland", "Null" },
                    { new Guid("64a5db8a-dab6-464e-bea7-edb822730619"), "JAP", "Japan", "Null" },
                    { new Guid("9d53067e-b2e6-4ee1-b3a8-7c8188d49ac8"), "STL", "Shuttherland", "Null" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("405662c8-fb7a-4ea2-82af-9dbb5b50c05b"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("a2d9cff7-c4ff-4078-9a32-23c4d2091b80"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("a5152b2a-65d0-4ce4-aba5-a5965795f7ac"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("00a5f071-1269-4287-9705-dbf8d6b2a4d1"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("07a6055c-9414-4ea1-aaa3-6f2b49eb085f"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("2547fc32-2817-489b-8907-ae45ee2b2913"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("62ce941a-f207-4222-a6cd-b7ce288c4caa"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("64a5db8a-dab6-464e-bea7-edb822730619"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("9d53067e-b2e6-4ee1-b3a8-7c8188d49ac8"));
        }
    }
}
