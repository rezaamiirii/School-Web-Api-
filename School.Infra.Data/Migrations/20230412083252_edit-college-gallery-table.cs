using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Infra.Data.Migrations
{
    public partial class editcollegegallerytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollegeGalleries_Colleges_CollegeId",
                table: "CollegeGalleries");

            migrationBuilder.DropColumn(
                name: "NewsId",
                table: "CollegeGalleries");

            migrationBuilder.AlterColumn<int>(
                name: "CollegeId",
                table: "CollegeGalleries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "CollegeGalleries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2023, 4, 12, 12, 2, 52, 499, DateTimeKind.Local).AddTicks(1238));

            migrationBuilder.AddForeignKey(
                name: "FK_CollegeGalleries_Colleges_CollegeId",
                table: "CollegeGalleries",
                column: "CollegeId",
                principalTable: "Colleges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollegeGalleries_Colleges_CollegeId",
                table: "CollegeGalleries");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "CollegeGalleries");

            migrationBuilder.AlterColumn<int>(
                name: "CollegeId",
                table: "CollegeGalleries",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "NewsId",
                table: "CollegeGalleries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2023, 4, 9, 12, 22, 40, 432, DateTimeKind.Local).AddTicks(9881));

            migrationBuilder.AddForeignKey(
                name: "FK_CollegeGalleries_Colleges_CollegeId",
                table: "CollegeGalleries",
                column: "CollegeId",
                principalTable: "Colleges",
                principalColumn: "Id");
        }
    }
}
