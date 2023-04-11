﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Infra.Data.Migrations
{
    public partial class addeverything : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Academies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Academies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colleges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colleges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileActiveCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false),
                    IsStudent = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    AverageOfNineLevel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MarkOfMath = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MarkOfScience = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MarkOfWorkAndTechnology = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NameOfExSchool = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstMajorPriority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondMajorPriority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThirdMajorPriority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollegeGalleries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewsId = table.Column<int>(type: "int", nullable: false),
                    CollegeId = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollegeGalleries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollegeGalleries_Colleges_CollegeId",
                        column: x => x.CollegeId,
                        principalTable: "Colleges",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MainStudentRegisters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FathersName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Serial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowOfBirthCertificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    National = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Religion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubReligion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsLeftHand = table.Column<bool>(type: "bit", nullable: false),
                    IsHelthy = table.Column<bool>(type: "bit", nullable: false),
                    LiveWithParents = table.Column<bool>(type: "bit", nullable: false),
                    Major = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildOfShahid = table.Column<bool>(type: "bit", nullable: false),
                    SchoolLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityOfCertificcate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    FathersMajor = table.Column<int>(type: "int", nullable: false),
                    FatherNationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherJob = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherMajor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherJob = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherPhoneNumberIsHisOwn = table.Column<bool>(type: "bit", nullable: false),
                    MotherPhoneNumberIsHerOwn = table.Column<bool>(type: "bit", nullable: false),
                    PlaceState = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HousePhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelativesPhoneNuumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameOfFormFillOuter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainStudentRegisters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MainStudentRegisters_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "CreateDate", "FirstName", "IsAdmin", "IsDelete", "LastName", "Password", "PhoneNumber" },
                values: new object[] { 5, new DateTime(2023, 4, 9, 12, 22, 40, 432, DateTimeKind.Local).AddTicks(9881), "Arash", true, false, "Ghanavati", "A66106518@", "09163008552" });

            migrationBuilder.CreateIndex(
                name: "IX_CollegeGalleries_CollegeId",
                table: "CollegeGalleries",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_MainStudentRegisters_StudentId",
                table: "MainStudentRegisters",
                column: "StudentId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Academies");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "CollegeGalleries");

            migrationBuilder.DropTable(
                name: "MainStudentRegisters");

            migrationBuilder.DropTable(
                name: "Colleges");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
