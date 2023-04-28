using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Infra.Data.Migrations
{
    public partial class editadmintable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsGallery_News_NewsId",
                table: "NewsGallery");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsGallery",
                table: "NewsGallery");

            migrationBuilder.RenameTable(
                name: "NewsGallery",
                newName: "NewsGalleries");

            migrationBuilder.RenameIndex(
                name: "IX_NewsGallery_NewsId",
                table: "NewsGalleries",
                newName: "IX_NewsGalleries_NewsId");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsGalleries",
                table: "NewsGalleries",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreateDate", "Role" },
                values: new object[] { new DateTime(2023, 4, 28, 15, 6, 18, 615, DateTimeKind.Local).AddTicks(7314), "Admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_NewsGalleries_News_NewsId",
                table: "NewsGalleries",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsGalleries_News_NewsId",
                table: "NewsGalleries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsGalleries",
                table: "NewsGalleries");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Admins");

            migrationBuilder.RenameTable(
                name: "NewsGalleries",
                newName: "NewsGallery");

            migrationBuilder.RenameIndex(
                name: "IX_NewsGalleries_NewsId",
                table: "NewsGallery",
                newName: "IX_NewsGallery_NewsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsGallery",
                table: "NewsGallery",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2023, 4, 14, 13, 17, 31, 912, DateTimeKind.Local).AddTicks(9043));

            migrationBuilder.AddForeignKey(
                name: "FK_NewsGallery_News_NewsId",
                table: "NewsGallery",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
