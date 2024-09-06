﻿// <auto-generated />
using System;
using EmployeeManagement.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmployeeManagement.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240905074608_ChangeName")]
    partial class ChangeName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EmployeeManagement.Models.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CityId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CityId");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            CityId = 1,
                            Name = "Hanoi"
                        },
                        new
                        {
                            CityId = 2,
                            Name = "Ho Chi Minh"
                        });
                });

            modelBuilder.Entity("EmployeeManagement.Models.District", b =>
                {
                    b.Property<int>("DistrictId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DistrictId"));

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DistrictId");

                    b.HasIndex("CityId");

                    b.ToTable("Districts");

                    b.HasData(
                        new
                        {
                            DistrictId = 1,
                            CityId = 1,
                            Name = "Dong Da"
                        },
                        new
                        {
                            DistrictId = 2,
                            CityId = 1,
                            Name = "Cau Giay"
                        },
                        new
                        {
                            DistrictId = 3,
                            CityId = 2,
                            Name = "Tan Binh"
                        },
                        new
                        {
                            DistrictId = 4,
                            CityId = 2,
                            Name = "Binh Thanh"
                        });
                });

            modelBuilder.Entity("EmployeeManagement.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<int?>("EthicId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("IdentityId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("JobId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WardId")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.HasIndex("CityId");

                    b.HasIndex("DistrictId");

                    b.HasIndex("EthicId");

                    b.HasIndex("JobId");

                    b.HasIndex("WardId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EmployeeManagement.Models.Ethic", b =>
                {
                    b.Property<int>("EthicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EthicId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EthicId");

                    b.ToTable("Ethics");

                    b.HasData(
                        new
                        {
                            EthicId = 1,
                            Name = "Kinh"
                        },
                        new
                        {
                            EthicId = 2,
                            Name = "Muong"
                        },
                        new
                        {
                            EthicId = 3,
                            Name = "Thai"
                        });
                });

            modelBuilder.Entity("EmployeeManagement.Models.Job", b =>
                {
                    b.Property<int>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("JobId");

                    b.ToTable("Jobs");

                    b.HasData(
                        new
                        {
                            JobId = 1,
                            Description = "Develop software",
                            Title = "Developer"
                        },
                        new
                        {
                            JobId = 2,
                            Description = "Test software",
                            Title = "Tester"
                        },
                        new
                        {
                            JobId = 3,
                            Description = "Analyze requirement",
                            Title = "BA"
                        });
                });

            modelBuilder.Entity("EmployeeManagement.Models.Ward", b =>
                {
                    b.Property<int>("WardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WardId"));

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WardId");

                    b.HasIndex("DistrictId");

                    b.ToTable("Wards");

                    b.HasData(
                        new
                        {
                            WardId = 1,
                            DistrictId = 1,
                            Name = "Quan Thanh"
                        },
                        new
                        {
                            WardId = 2,
                            DistrictId = 1,
                            Name = "Cat Linh"
                        },
                        new
                        {
                            WardId = 3,
                            DistrictId = 2,
                            Name = "Nghia Do"
                        },
                        new
                        {
                            WardId = 4,
                            DistrictId = 2,
                            Name = "Nghia Tan"
                        },
                        new
                        {
                            WardId = 5,
                            DistrictId = 3,
                            Name = "Tan Dinh"
                        },
                        new
                        {
                            WardId = 6,
                            DistrictId = 3,
                            Name = "Tan Thanh"
                        },
                        new
                        {
                            WardId = 7,
                            DistrictId = 4,
                            Name = "Binh Thanh"
                        },
                        new
                        {
                            WardId = 8,
                            DistrictId = 4,
                            Name = "Binh Loi"
                        });
                });

            modelBuilder.Entity("EmployeeManagement.Models.District", b =>
                {
                    b.HasOne("EmployeeManagement.Models.City", "City")
                        .WithMany("Districts")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("EmployeeManagement.Models.Employee", b =>
                {
                    b.HasOne("EmployeeManagement.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EmployeeManagement.Models.District", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EmployeeManagement.Models.Ethic", "Ethic")
                        .WithMany()
                        .HasForeignKey("EthicId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("EmployeeManagement.Models.Job", "Job")
                        .WithMany()
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("EmployeeManagement.Models.Ward", "Ward")
                        .WithMany()
                        .HasForeignKey("WardId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("District");

                    b.Navigation("Ethic");

                    b.Navigation("Job");

                    b.Navigation("Ward");
                });

            modelBuilder.Entity("EmployeeManagement.Models.Ward", b =>
                {
                    b.HasOne("EmployeeManagement.Models.District", "District")
                        .WithMany("Wards")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("District");
                });

            modelBuilder.Entity("EmployeeManagement.Models.City", b =>
                {
                    b.Navigation("Districts");
                });

            modelBuilder.Entity("EmployeeManagement.Models.District", b =>
                {
                    b.Navigation("Wards");
                });
#pragma warning restore 612, 618
        }
    }
}
