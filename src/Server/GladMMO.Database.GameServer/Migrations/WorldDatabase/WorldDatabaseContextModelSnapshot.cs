﻿// <auto-generated />
using System;
using GladMMO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GladMMO.Database.GameServer.Migrations.WorldDatabase
{
    [DbContext(typeof(ContentDatabaseContext))]
    partial class WorldDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

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
#pragma warning restore 612, 618
        }
    }
}
