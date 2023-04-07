﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop.Infra.Data.Context;

#nullable disable

namespace School.Infra.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230405084807_add-studentfee-table")]
    partial class addstudentfeetable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("School.Domain.Models.Account.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Admins");

                    b.HasData(
                        new
                        {
                            Id = 5,
                            CreateDate = new DateTime(2023, 4, 5, 12, 18, 7, 131, DateTimeKind.Local).AddTicks(4514),
                            FirstName = "Arash",
                            IsAdmin = true,
                            IsDelete = false,
                            LastName = "Ghanavati",
                            Password = "A66106518@",
                            PhoneNumber = "09163008552"
                        });
                });

            modelBuilder.Entity("School.Domain.Models.Account.MainStudentRegister", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BirthCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ChildOfShahid")
                        .HasColumnType("bit");

                    b.Property<string>("CityOfCertificcate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FatherJob")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherNationalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("FatherPhoneNumberIsHisOwn")
                        .HasColumnType("bit");

                    b.Property<int>("FathersMajor")
                        .HasColumnType("int");

                    b.Property<string>("FathersName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HousePhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("IsHelthy")
                        .HasColumnType("bit");

                    b.Property<bool>("IsLeftHand")
                        .HasColumnType("bit");

                    b.Property<bool>("LiveWithParents")
                        .HasColumnType("bit");

                    b.Property<string>("Major")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherJob")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherMajor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("MotherPhoneNumberIsHerOwn")
                        .HasColumnType("bit");

                    b.Property<string>("NameOfFormFillOuter")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("National")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlaceState")
                        .HasColumnType("int");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RelativesPhoneNuumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Religion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RowOfBirthCertificate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SchoolLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Seri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Serial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("SubReligion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("MainStudentRegisters");
                });

            modelBuilder.Entity("School.Domain.Models.Account.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("AverageOfNineLevel")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstMajorPriority")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsBlock")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("IsStudent")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("MarkOfMath")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MarkOfScience")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MarkOfWorkAndTechnology")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("MobileActiveCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameOfExSchool")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondMajorPriority")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThirdMajorPriority")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("School.Domain.Models.Account.StudentFee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPay")
                        .HasColumnType("bit");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentFee");
                });

            modelBuilder.Entity("School.Domain.Models.Account.MainStudentRegister", b =>
                {
                    b.HasOne("School.Domain.Models.Account.Student", "Student")
                        .WithOne("MainStudentRegister")
                        .HasForeignKey("School.Domain.Models.Account.MainStudentRegister", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("School.Domain.Models.Account.StudentFee", b =>
                {
                    b.HasOne("School.Domain.Models.Account.Student", "Student")
                        .WithMany("StudentFees")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("School.Domain.Models.Account.Student", b =>
                {
                    b.Navigation("MainStudentRegister");

                    b.Navigation("StudentFees");
                });
#pragma warning restore 612, 618
        }
    }
}
