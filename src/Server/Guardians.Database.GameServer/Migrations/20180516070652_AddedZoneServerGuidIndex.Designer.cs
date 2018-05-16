﻿// <auto-generated />
using Guardians;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Guardians.Database.GameServer.Migrations
{
    [DbContext(typeof(CharacterDatabaseContext))]
    [Migration("20180516070652_AddedZoneServerGuidIndex")]
    partial class AddedZoneServerGuidIndex
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Guardians.CharacterEntryModel", b =>
                {
                    b.Property<int>("CharacterId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId");

                    b.Property<string>("CharacterName")
                        .IsRequired();

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP(6)")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.HasKey("CharacterId");

                    b.HasIndex("AccountId");

                    b.HasIndex("CharacterName")
                        .IsUnique();

                    b.ToTable("characters");
                });

            modelBuilder.Entity("Guardians.CharacterLocationModel", b =>
                {
                    b.Property<int>("CharacterId");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("TIMESTAMP(6)")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

                    b.Property<float>("XPosition");

                    b.Property<float>("YPosition");

                    b.Property<float>("ZPosition");

                    b.Property<int>("ZoneType");

                    b.HasKey("CharacterId");

                    b.ToTable("character_locations");
                });

            modelBuilder.Entity("Guardians.CharacterSessionModel", b =>
                {
                    b.Property<int>("CharacterId");

                    b.Property<DateTime>("SessionCreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP(6)")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("SessionLastUpdateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("TIMESTAMP(6)")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

                    b.Property<int>("ZoneId");

                    b.HasKey("CharacterId");

                    b.HasIndex("CharacterId")
                        .IsUnique();

                    b.HasIndex("ZoneId");

                    b.ToTable("character_sessions");
                });

            modelBuilder.Entity("Guardians.ClaimedSessionsModel", b =>
                {
                    b.Property<int>("CharacterId");

                    b.Property<DateTime>("SessionCreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP(6)")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.HasKey("CharacterId");

                    b.ToTable("claimed_sessions");
                });

            modelBuilder.Entity("Guardians.ZoneInstanceEntryModel", b =>
                {
                    b.Property<int>("ZoneId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ZoneGuid");

                    b.Property<string>("ZoneServerAddress")
                        .IsRequired();

                    b.Property<short>("ZoneServerPort");

                    b.Property<int>("ZoneType");

                    b.HasKey("ZoneId");

                    b.HasIndex("ZoneGuid")
                        .IsUnique();

                    b.ToTable("zone_endpoints");
                });

            modelBuilder.Entity("Guardians.CharacterLocationModel", b =>
                {
                    b.HasOne("Guardians.CharacterEntryModel", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Guardians.CharacterSessionModel", b =>
                {
                    b.HasOne("Guardians.CharacterEntryModel", "CharacterEntry")
                        .WithOne()
                        .HasForeignKey("Guardians.CharacterSessionModel", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Guardians.ZoneInstanceEntryModel", "ZoneEntry")
                        .WithMany()
                        .HasForeignKey("ZoneId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Guardians.ClaimedSessionsModel", b =>
                {
                    b.HasOne("Guardians.CharacterSessionModel", "Session")
                        .WithOne()
                        .HasForeignKey("Guardians.ClaimedSessionsModel", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
