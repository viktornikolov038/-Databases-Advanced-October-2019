﻿// <auto-generated />
using System;
using EmployeesMapping.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmployeesMapping.Data.Migrations
{
    [DbContext(typeof(EmployeesMappingContext))]
    [Migration("20190322170101_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EmployeesMapping.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<DateTime?>("BirthDay");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<int?>("ManagerId");

                    b.Property<decimal>("Salary");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Georgi",
                            LastName = "Georgiev",
                            Salary = 12131.44m
                        },
                        new
                        {
                            Id = 2,
                            Address = "Neznam",
                            BirthDay = new DateTime(2019, 3, 7, 19, 1, 1, 170, DateTimeKind.Local).AddTicks(4017),
                            FirstName = "Maria",
                            LastName = "Marieva",
                            Salary = 999.10m
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Alisia",
                            LastName = "Alisieva",
                            Salary = 11111.11m
                        },
                        new
                        {
                            Id = 4,
                            Address = "Neznam2",
                            FirstName = "Pesho",
                            LastName = "Peshov",
                            Salary = 431.44m
                        },
                        new
                        {
                            Id = 6,
                            BirthDay = new DateTime(2018, 3, 22, 19, 1, 1, 172, DateTimeKind.Local).AddTicks(6247),
                            FirstName = "Miro",
                            LastName = "Mirov",
                            Salary = 2000.44m
                        },
                        new
                        {
                            Id = 7,
                            BirthDay = new DateTime(2011, 1, 3, 19, 1, 1, 172, DateTimeKind.Local).AddTicks(6264),
                            FirstName = "Blago",
                            LastName = "Petkov",
                            Salary = 2000.44m
                        },
                        new
                        {
                            Id = 8,
                            BirthDay = new DateTime(2008, 4, 8, 19, 1, 1, 172, DateTimeKind.Local).AddTicks(6269),
                            FirstName = "Emanuela",
                            LastName = "Marinova",
                            Salary = 2000.44m
                        },
                        new
                        {
                            Id = 9,
                            BirthDay = new DateTime(2013, 9, 29, 19, 1, 1, 172, DateTimeKind.Local).AddTicks(6273),
                            FirstName = "Koce",
                            LastName = "Kocev",
                            Salary = 2000.44m
                        });
                });

            modelBuilder.Entity("EmployeesMapping.Models.Employee", b =>
                {
                    b.HasOne("EmployeesMapping.Models.Employee", "Manager")
                        .WithMany("ManagedEmployees")
                        .HasForeignKey("ManagerId");
                });
#pragma warning restore 612, 618
        }
    }
}
