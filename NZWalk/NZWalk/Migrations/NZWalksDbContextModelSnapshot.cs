﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NZWalk.Data;

#nullable disable

namespace NZWalk.Migrations
{
    [DbContext(typeof(NZWalksDbContext))]
    partial class NZWalksDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NZWalk.Models.Difficulty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Difficulties");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a5152b2a-65d0-4ce4-aba5-a5965795f7ac"),
                            Name = "Easy"
                        },
                        new
                        {
                            Id = new Guid("a2d9cff7-c4ff-4078-9a32-23c4d2091b80"),
                            Name = "Medium"
                        },
                        new
                        {
                            Id = new Guid("405662c8-fb7a-4ea2-82af-9dbb5b50c05b"),
                            Name = "Hard"
                        });
                });

            modelBuilder.Entity("NZWalk.Models.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("FileSizeInByte")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("NZWalk.Models.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("62ce941a-f207-4222-a6cd-b7ce288c4caa"),
                            Code = "Al",
                            Name = "Ackland",
                            RegionImageUrl = "Null"
                        },
                        new
                        {
                            Id = new Guid("9d53067e-b2e6-4ee1-b3a8-7c8188d49ac8"),
                            Code = "STL",
                            Name = "Shuttherland",
                            RegionImageUrl = "Null"
                        },
                        new
                        {
                            Id = new Guid("2547fc32-2817-489b-8907-ae45ee2b2913"),
                            Code = "Cal",
                            Name = "Californea",
                            RegionImageUrl = "Null"
                        },
                        new
                        {
                            Id = new Guid("64a5db8a-dab6-464e-bea7-edb822730619"),
                            Code = "JAP",
                            Name = "Japan",
                            RegionImageUrl = "Null"
                        },
                        new
                        {
                            Id = new Guid("07a6055c-9414-4ea1-aaa3-6f2b49eb085f"),
                            Code = "THI",
                            Name = "Thiland",
                            RegionImageUrl = "Null"
                        },
                        new
                        {
                            Id = new Guid("00a5f071-1269-4287-9705-dbf8d6b2a4d1"),
                            Code = "BK",
                            Name = "Bankok",
                            RegionImageUrl = "Null"
                        });
                });

            modelBuilder.Entity("NZWalk.Models.Walk", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DifficultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("LengthInKm")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WalkImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("RegionId");

                    b.ToTable("Walks");
                });

            modelBuilder.Entity("NZWalk.Models.Walk", b =>
                {
                    b.HasOne("NZWalk.Models.Difficulty", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NZWalk.Models.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Difficulty");

                    b.Navigation("Region");
                });
#pragma warning restore 612, 618
        }
    }
}
