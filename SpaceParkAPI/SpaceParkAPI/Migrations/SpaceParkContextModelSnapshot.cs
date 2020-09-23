﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpaceParkAPI.Db_Context;

namespace SpaceParkAPI.Migrations
{
    [DbContext(typeof(SpaceParkContext))]
    partial class SpaceParkContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0-rc.1.20451.13");

            modelBuilder.Entity("SpaceParkAPI.Models.ParkingLotModel", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("TotalAmount")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.ToTable("ParkingLots");

                    b.HasData(
                        new
                        {
                            ID = 1L,
                            TotalAmount = 15L
                        },
                        new
                        {
                            ID = 2L,
                            TotalAmount = 14L
                        });
                });

            modelBuilder.Entity("SpaceParkAPI.Models.ParkingSpaceModel", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long?>("ParkingLotID")
                        .HasColumnType("bigint");

                    b.Property<long?>("SpaceShipID")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("ParkingLotID");

                    b.HasIndex("SpaceShipID");

                    b.ToTable("ParkingSpaces");

                    b.HasData(
                        new
                        {
                            ID = 1L,
                            ParkingLotID = 1L,
                            SpaceShipID = 2L
                        },
                        new
                        {
                            ID = 2L,
                            ParkingLotID = 2L,
                            SpaceShipID = 1L
                        });
                });

            modelBuilder.Entity("SpaceParkAPI.Models.PersonModel", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Persons");

                    b.HasData(
                        new
                        {
                            ID = 1L,
                            Name = "sebastian"
                        },
                        new
                        {
                            ID = 2L,
                            Name = "Eric"
                        });
                });

            modelBuilder.Entity("SpaceParkAPI.Models.SpaceshipModel", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long?>("PersonID")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("PersonID");

                    b.ToTable("Spaceships");

                    b.HasData(
                        new
                        {
                            ID = 1L,
                            PersonID = 2L
                        },
                        new
                        {
                            ID = 2L,
                            PersonID = 2L
                        });
                });

            modelBuilder.Entity("SpaceParkAPI.Models.ParkingSpaceModel", b =>
                {
                    b.HasOne("SpaceParkAPI.Models.ParkingLotModel", "ParkingLot")
                        .WithMany("ParkingSpaces")
                        .HasForeignKey("ParkingLotID");

                    b.HasOne("SpaceParkAPI.Models.SpaceshipModel", "SpaceShip")
                        .WithMany()
                        .HasForeignKey("SpaceShipID");

                    b.Navigation("ParkingLot");

                    b.Navigation("SpaceShip");
                });

            modelBuilder.Entity("SpaceParkAPI.Models.SpaceshipModel", b =>
                {
                    b.HasOne("SpaceParkAPI.Models.PersonModel", "Person")
                        .WithMany()
                        .HasForeignKey("PersonID");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("SpaceParkAPI.Models.ParkingLotModel", b =>
                {
                    b.Navigation("ParkingSpaces");
                });
#pragma warning restore 612, 618
        }
    }
}
