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
    [Migration("20190615135939_AddedInitialCreatureTemplate")]
    partial class AddedInitialCreatureTemplate
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

                    b.ToTable("creature_model_entry");
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
