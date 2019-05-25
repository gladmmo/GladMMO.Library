﻿// <auto-generated />
using System;
using PSOBB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace PSOBB.Database.GameServer.Migrations
{
    [DbContext(typeof(CharacterDatabaseContext))]
    partial class CharacterDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Guardians.CharacterEntryModel", b =>
                {
                    b.Property<int>("CharacterId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountId");

                    b.Property<string>("CharacterName")
                        .IsRequired();

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP(6)");

                    b.HasKey("CharacterId");

                    b.HasIndex("AccountId");

                    b.HasIndex("CharacterName")
                        .IsUnique();

                    b.ToTable("characters");
                });

            modelBuilder.Entity("Guardians.CharacterFriendRelationshipModel", b =>
                {
                    b.Property<int>("FriendshipRelationshipId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP(6)");

                    b.Property<long>("DirectionalUniqueness");

                    b.Property<int>("RelationState");

                    b.Property<int>("RequestingCharacterId");

                    b.Property<int>("TargetRequestCharacterId");

                    b.HasKey("FriendshipRelationshipId");

                    b.HasAlternateKey("RequestingCharacterId", "TargetRequestCharacterId");

                    b.HasIndex("DirectionalUniqueness")
                        .IsUnique();

                    b.HasIndex("TargetRequestCharacterId");

                    b.ToTable("character_friendrelationship");
                });

            modelBuilder.Entity("Guardians.CharacterGroupEntryModel", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("JoinDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP(6)");

                    b.Property<int>("LeaderCharacterId");

                    b.HasKey("GroupId");

                    b.HasAlternateKey("LeaderCharacterId");

                    b.ToTable("group_entry");
                });

            modelBuilder.Entity("Guardians.CharacterGroupInviteEntryModel", b =>
                {
                    b.Property<int>("CharacterId");

                    b.Property<int>("GroupId");

                    b.Property<DateTime>("InviteExpirationTime")
                        .HasColumnType("TIMESTAMP(6)");

                    b.HasKey("CharacterId");

                    b.HasIndex("GroupId");

                    b.ToTable("group_invites");
                });

            modelBuilder.Entity("Guardians.CharacterGroupMembershipModel", b =>
                {
                    b.Property<int>("CharacterId");

                    b.Property<int>("GroupId");

                    b.HasKey("CharacterId");

                    b.HasIndex("GroupId");

                    b.ToTable("group_members");
                });

            modelBuilder.Entity("Guardians.CharacterGuildMemberRelationshipModel", b =>
                {
                    b.Property<int>("CharacterId");

                    b.Property<int>("GuildId");

                    b.Property<DateTime>("JoinDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP(6)");

                    b.HasKey("CharacterId");

                    b.HasIndex("GuildId");

                    b.ToTable("guild_charactermember");
                });

            modelBuilder.Entity("Guardians.CharacterLocationModel", b =>
                {
                    b.Property<int>("CharacterId");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("TIMESTAMP(6)");

                    b.Property<long>("WorldId");

                    b.Property<float>("XPosition");

                    b.Property<float>("YPosition");

                    b.Property<float>("ZPosition");

                    b.HasKey("CharacterId");

                    b.HasIndex("WorldId");

                    b.ToTable("character_locations");
                });

            modelBuilder.Entity("Guardians.CharacterSessionModel", b =>
                {
                    b.Property<int>("CharacterId");

                    b.Property<DateTime>("SessionCreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP(6)");

                    b.Property<DateTime>("SessionLastUpdateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("TIMESTAMP(6)");

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
                        .HasColumnType("TIMESTAMP(6)");

                    b.HasKey("CharacterId");

                    b.ToTable("claimed_sessions");
                });

            modelBuilder.Entity("Guardians.GuildEntryModel", b =>
                {
                    b.Property<int>("GuildId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP(6)");

                    b.Property<int>("GuildMasterCharacterId");

                    b.Property<string>("GuildName")
                        .HasMaxLength(32);

                    b.HasKey("GuildId");

                    b.HasAlternateKey("GuildMasterCharacterId");

                    b.HasIndex("GuildName")
                        .IsUnique();

                    b.ToTable("guild_entry");
                });

            modelBuilder.Entity("Guardians.WorldEntryModel", b =>
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

            modelBuilder.Entity("Guardians.ZoneInstanceEntryModel", b =>
                {
                    b.Property<int>("ZoneId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("WorldId");

                    b.Property<string>("ZoneServerAddress")
                        .IsRequired();

                    b.Property<short>("ZoneServerPort");

                    b.HasKey("ZoneId");

                    b.HasAlternateKey("ZoneServerAddress", "ZoneServerPort");

                    b.HasIndex("WorldId");

                    b.ToTable("zone_endpoints");
                });

            modelBuilder.Entity("Guardians.CharacterFriendRelationshipModel", b =>
                {
                    b.HasOne("Guardians.CharacterEntryModel", "RequestingCharacter")
                        .WithMany()
                        .HasForeignKey("RequestingCharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Guardians.CharacterEntryModel", "TargetRequestCharacter")
                        .WithMany()
                        .HasForeignKey("TargetRequestCharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Guardians.CharacterGroupEntryModel", b =>
                {
                    b.HasOne("Guardians.CharacterEntryModel", "LeaderCharacter")
                        .WithMany()
                        .HasForeignKey("LeaderCharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Guardians.CharacterGroupInviteEntryModel", b =>
                {
                    b.HasOne("Guardians.CharacterEntryModel", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Guardians.GuildEntryModel", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Guardians.CharacterGroupMembershipModel", b =>
                {
                    b.HasOne("Guardians.CharacterEntryModel", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Guardians.GuildEntryModel", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Guardians.CharacterGuildMemberRelationshipModel", b =>
                {
                    b.HasOne("Guardians.CharacterEntryModel", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Guardians.GuildEntryModel", "Guild")
                        .WithMany()
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Guardians.CharacterLocationModel", b =>
                {
                    b.HasOne("Guardians.CharacterEntryModel", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Guardians.WorldEntryModel", "WorldEntry")
                        .WithMany()
                        .HasForeignKey("WorldId")
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

            modelBuilder.Entity("Guardians.GuildEntryModel", b =>
                {
                    b.HasOne("Guardians.CharacterEntryModel", "GuildMaster")
                        .WithMany()
                        .HasForeignKey("GuildMasterCharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Guardians.ZoneInstanceEntryModel", b =>
                {
                    b.HasOne("Guardians.WorldEntryModel", "WorldEntry")
                        .WithMany()
                        .HasForeignKey("WorldId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
