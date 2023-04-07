using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Infra.Data.Migrations
{
    public partial class addadin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "IsAdmin" },
                values: new object[] { new DateTime(2023, 4, 4, 20, 32, 21, 537, DateTimeKind.Local).AddTicks(7376), true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "IsAdmin" },
                values: new object[] { new DateTime(2023, 4, 4, 20, 28, 38, 568, DateTimeKind.Local).AddTicks(5566), false });
        }
    }
}
