﻿// <auto-generated />
using System;
using GladMMO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GladMMO.Database.GameServer.Migrations
{
    [DbContext(typeof(CharacterDatabaseContext))]
    partial class CharacterDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GladMMO.CharacterDataModel", b =>
                {
                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("ExperiencePoints")
                        .HasColumnType("int");

                    b.HasKey("CharacterId");

                    b.ToTable("character_data");
                });

            modelBuilder.Entity("GladMMO.CharacterEntryModel", b =>
                {
                    b.Property<int>("CharacterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("CharacterName")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP(6)");

                    b.Property<string>("PlayFabCharacterId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("PlayFabId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("CharacterId");

                    b.HasAlternateKey("PlayFabId", "PlayFabCharacterId");

                    b.HasIndex("AccountId");

                    b.HasIndex("CharacterName")
                        .IsUnique();

                    b.HasIndex("PlayFabId");

                    b.ToTable("characters");
                });

            modelBuilder.Entity("GladMMO.CharacterLocationModel", b =>
                {
                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("TIMESTAMP(6)");

                    b.Property<long>("WorldId")
                        .HasColumnType("bigint");

                    b.Property<float>("XPosition")
                        .HasColumnType("float");

                    b.Property<float>("YPosition")
                        .HasColumnType("float");

                    b.Property<float>("ZPosition")
                        .HasColumnType("float");

                    b.HasKey("CharacterId");

                    b.HasIndex("WorldId");

                    b.ToTable("character_locations");
                });

            modelBuilder.Entity("GladMMO.CharacterSessionModel", b =>
                {
                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SessionCreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP(6)");

                    b.Property<DateTime>("SessionLastUpdateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("TIMESTAMP(6)");

                    b.Property<int>("ZoneId")
                        .HasColumnType("int");

                    b.HasKey("CharacterId");

                    b.HasIndex("CharacterId")
                        .IsUnique();

                    b.HasIndex("ZoneId");

                    b.ToTable("character_sessions");
                });

            modelBuilder.Entity("GladMMO.ClaimedSessionsModel", b =>
                {
                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SessionCreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP(6)");

                    b.HasKey("CharacterId");

                    b.ToTable("claimed_sessions");
                });

            modelBuilder.Entity("GladMMO.WorldEntryModel", b =>
                {
                    b.Property<long>("WorldId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("CreationIp")
                        .IsRequired()
                        .HasColumnType("varchar(15) CHARACTER SET utf8mb4")
                        .HasMaxLength(15);

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsValidated")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("StorageGuid")
                        .HasColumnType("char(36)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("WorldId");

                    b.ToTable("world_entry");
                });

            modelBuilder.Entity("GladMMO.ZoneInstanceEntryModel", b =>
                {
                    b.Property<int>("ZoneId")
                        .HasColumnType("int");

                    b.Property<long>("LastCheckinTime")
                        .HasColumnType("bigint");

                    b.Property<long>("RegistrationTime")
                        .HasColumnType("bigint");

                    b.Property<long>("WorldId")
                        .HasColumnType("bigint");

                    b.Property<string>("ZoneServerAddress")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<short>("ZoneServerPort")
                        .HasColumnType("smallint");

                    b.HasKey("ZoneId");

                    b.HasAlternateKey("ZoneServerAddress", "ZoneServerPort");

                    b.HasIndex("WorldId");

                    b.ToTable("zone_endpoints");
                });

            modelBuilder.Entity("GladMMO.CharacterDataModel", b =>
                {
                    b.HasOne("GladMMO.CharacterEntryModel", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GladMMO.CharacterLocationModel", b =>
                {
                    b.HasOne("GladMMO.CharacterEntryModel", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GladMMO.WorldEntryModel", "WorldEntry")
                        .WithMany()
                        .HasForeignKey("WorldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GladMMO.CharacterSessionModel", b =>
                {
                    b.HasOne("GladMMO.CharacterEntryModel", "CharacterEntry")
                        .WithOne()
                        .HasForeignKey("GladMMO.CharacterSessionModel", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GladMMO.ZoneInstanceEntryModel", "ZoneEntry")
                        .WithMany()
                        .HasForeignKey("ZoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GladMMO.ClaimedSessionsModel", b =>
                {
                    b.HasOne("GladMMO.CharacterSessionModel", "Session")
                        .WithOne()
                        .HasForeignKey("GladMMO.ClaimedSessionsModel", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GladMMO.ZoneInstanceEntryModel", b =>
                {
                    b.HasOne("GladMMO.WorldEntryModel", "WorldEntry")
                        .WithMany()
                        .HasForeignKey("WorldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
