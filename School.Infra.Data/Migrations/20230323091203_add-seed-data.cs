using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Infra.Data.Migrations
{
    public partial class addseeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "CreateDate", "FirstName", "IsDelete", "LastName", "Password", "PhoneNumber" },
                values: new object[] { 1, new DateTime(2023, 3, 23, 12, 42, 3, 205, DateTimeKind.Local).AddTicks(4627), "Arash", false, "Ghanavati", "A66106518@", "09163008552" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
