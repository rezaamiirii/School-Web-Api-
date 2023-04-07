using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Infra.Data.Migrations
{
    public partial class addadmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "CreateDate", "FirstName", "IsAdmin", "IsDelete", "LastName", "Password", "PhoneNumber" },
                values: new object[] { 5, new DateTime(2023, 4, 4, 20, 33, 52, 896, DateTimeKind.Local).AddTicks(9149), "Arash", true, false, "Ghanavati", "A66106518@", "09163008552" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "CreateDate", "FirstName", "IsAdmin", "IsDelete", "LastName", "Password", "PhoneNumber" },
                values: new object[] { 1, new DateTime(2023, 4, 4, 20, 32, 21, 537, DateTimeKind.Local).AddTicks(7376), "Arash", true, false, "Ghanavati", "A66106518@", "09163008552" });
        }
    }
}
