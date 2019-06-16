﻿// <auto-generated />
using System;
using GladMMO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GladMMO.Database.GameServer.Migrations.WorldDatabase
{
    [DbContext(typeof(ContentDatabaseContext))]
    [Migration("20190616143552_AddedCreatureEntry")]
    partial class AddedCreatureEntry
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GladMMO.AvatarEntryModel", b =>
                {
                    b.Property<long>("AvatarId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountId");

                    b.Property<string>("CreationIp")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<bool>("IsValidated");

                    b.Property<Guid>("StorageGuid");

                    b.HasKey("AvatarId");

                    b.ToTable("avatar_entry");
                });

            modelBuilder.Entity("GladMMO.CreatureEntryModel", b =>
                {
                    b.Property<int>("CreatureEntryId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatureTemplateId");

                    b.Property<float>("InitialOrientation");

                    b.Property<int>("MapId");

                    b.HasKey("CreatureEntryId");

                    b.HasIndex("CreatureTemplateId");

                    b.ToTable("creature");
                });

            modelBuilder.Entity("GladMMO.CreatureModelEntryModel", b =>
                {
                    b.Property<long>("CreatureId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountId");

                    b.Property<string>("CreationIp")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<bool>("IsValidated");

                    b.Property<Guid>("StorageGuid");

                    b.HasKey("CreatureId");

                    b.ToTable("creature_model");
                });

            modelBuilder.Entity("GladMMO.CreatureTemplateEntryModel", b =>
                {
                    b.Property<int>("CreatureTemplateId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountId");

                    b.Property<long>("ModelId");

                    b.HasKey("CreatureTemplateId");

                    b.HasIndex("ModelId");

                    b.ToTable("creature_template");
                });

            modelBuilder.Entity("GladMMO.WorldEntryModel", b =>
                {
                    b.Property<long>("WorldId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountId");

                    b.Property<string>("CreationIp")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<bool>("IsValidated");

                    b.Property<Guid>("StorageGuid");

                    b.HasKey("WorldId");

                    b.ToTable("world_entry");
                });

            modelBuilder.Entity("GladMMO.CreatureEntryModel", b =>
                {
                    b.HasOne("GladMMO.CreatureTemplateEntryModel", "CreatureTemplate")
                        .WithMany()
                        .HasForeignKey("CreatureTemplateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("GladMMO.Database.Vector3<float>", "SpawnPosition", b1 =>
                        {
                            b1.Property<int>("CreatureEntryModelCreatureEntryId");

                            b1.Property<float>("X");

                            b1.Property<float>("Y");

                            b1.Property<float>("Z");

                            b1.HasKey("CreatureEntryModelCreatureEntryId");

                            b1.ToTable("creature");

                            b1.HasOne("GladMMO.CreatureEntryModel")
                                .WithOne("SpawnPosition")
                                .HasForeignKey("GladMMO.Database.Vector3<float>", "CreatureEntryModelCreatureEntryId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("GladMMO.CreatureTemplateEntryModel", b =>
                {
                    b.HasOne("GladMMO.CreatureModelEntryModel", "CreatureModelEntry")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
