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

                    b.Property<int>("Version");

                    b.HasKey("AvatarId");

                    b.ToTable("avatar_entry");
                });

            modelBuilder.Entity("GladMMO.CharacterAppearanceModel", b =>
                {
                    b.Property<int>("CharacterId");

                    b.Property<long>("AvatarModelId");

                    b.HasKey("CharacterId");

                    b.HasIndex("AvatarModelId");

                    b.ToTable("character_appearance");
                });

            modelBuilder.Entity("GladMMO.CharacterDataModel", b =>
                {
                    b.Property<int>("CharacterId");

                    b.Property<int>("ExperiencePoints");

                    b.HasKey("CharacterId");

                    b.ToTable("character_data");
                });

            modelBuilder.Entity("GladMMO.CharacterEntryModel", b =>
                {
                    b.Property<int>("CharacterId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountId");

                    b.Property<string>("CharacterName")
                        .IsRequired();

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP(6)");

                    b.Property<string>("PlayFabCharacterId")
                        .IsRequired();

                    b.Property<string>("PlayFabId")
                        .IsRequired();

                    b.HasKey("CharacterId");

                    b.HasAlternateKey("PlayFabId", "PlayFabCharacterId");

                    b.HasIndex("AccountId");

                    b.HasIndex("CharacterName")
                        .IsUnique();

                    b.HasIndex("PlayFabId");

                    b.ToTable("characters");
                });

            modelBuilder.Entity("GladMMO.CharacterFriendRelationshipModel", b =>
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

            modelBuilder.Entity("GladMMO.CharacterGroupEntryModel", b =>
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

            modelBuilder.Entity("GladMMO.CharacterGroupInviteEntryModel", b =>
                {
                    b.Property<int>("CharacterId");

                    b.Property<int>("GroupId");

                    b.Property<DateTime>("InviteExpirationTime")
                        .HasColumnType("TIMESTAMP(6)");

                    b.HasKey("CharacterId");

                    b.HasIndex("GroupId");

                    b.ToTable("group_invites");
                });

            modelBuilder.Entity("GladMMO.CharacterGroupMembershipModel", b =>
                {
                    b.Property<int>("CharacterId");

                    b.Property<int>("GroupId");

                    b.HasKey("CharacterId");

                    b.HasIndex("GroupId");

                    b.ToTable("group_members");
                });

            modelBuilder.Entity("GladMMO.CharacterGuildMemberRelationshipModel", b =>
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

            modelBuilder.Entity("GladMMO.CharacterLocationModel", b =>
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

            modelBuilder.Entity("GladMMO.CharacterSessionModel", b =>
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

            modelBuilder.Entity("GladMMO.ClaimedSessionsModel", b =>
                {
                    b.Property<int>("CharacterId");

                    b.Property<DateTime>("SessionCreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP(6)");

                    b.HasKey("CharacterId");

                    b.ToTable("claimed_sessions");
                });

            modelBuilder.Entity("GladMMO.GuildEntryModel", b =>
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

                    b.Property<int>("Version");

                    b.HasKey("WorldId");

                    b.ToTable("world_entry");
                });

            modelBuilder.Entity("GladMMO.ZoneInstanceEntryModel", b =>
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

            modelBuilder.Entity("GladMMO.CharacterAppearanceModel", b =>
                {
                    b.HasOne("GladMMO.AvatarEntryModel", "AvatarModel")
                        .WithMany()
                        .HasForeignKey("AvatarModelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GladMMO.CharacterEntryModel", "Character")
                        .WithOne()
                        .HasForeignKey("GladMMO.CharacterAppearanceModel", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GladMMO.CharacterDataModel", b =>
                {
                    b.HasOne("GladMMO.CharacterEntryModel", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GladMMO.CharacterFriendRelationshipModel", b =>
                {
                    b.HasOne("GladMMO.CharacterEntryModel", "RequestingCharacter")
                        .WithMany()
                        .HasForeignKey("RequestingCharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GladMMO.CharacterEntryModel", "TargetRequestCharacter")
                        .WithMany()
                        .HasForeignKey("TargetRequestCharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GladMMO.CharacterGroupEntryModel", b =>
                {
                    b.HasOne("GladMMO.CharacterEntryModel", "LeaderCharacter")
                        .WithMany()
                        .HasForeignKey("LeaderCharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GladMMO.CharacterGroupInviteEntryModel", b =>
                {
                    b.HasOne("GladMMO.CharacterEntryModel", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GladMMO.GuildEntryModel", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GladMMO.CharacterGroupMembershipModel", b =>
                {
                    b.HasOne("GladMMO.CharacterEntryModel", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GladMMO.GuildEntryModel", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GladMMO.CharacterGuildMemberRelationshipModel", b =>
                {
                    b.HasOne("GladMMO.CharacterEntryModel", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GladMMO.GuildEntryModel", "Guild")
                        .WithMany()
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GladMMO.CharacterLocationModel", b =>
                {
                    b.HasOne("GladMMO.CharacterEntryModel", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GladMMO.WorldEntryModel", "WorldEntry")
                        .WithMany()
                        .HasForeignKey("WorldId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GladMMO.CharacterSessionModel", b =>
                {
                    b.HasOne("GladMMO.CharacterEntryModel", "CharacterEntry")
                        .WithOne()
                        .HasForeignKey("GladMMO.CharacterSessionModel", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GladMMO.ZoneInstanceEntryModel", "ZoneEntry")
                        .WithMany()
                        .HasForeignKey("ZoneId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GladMMO.ClaimedSessionsModel", b =>
                {
                    b.HasOne("GladMMO.CharacterSessionModel", "Session")
                        .WithOne()
                        .HasForeignKey("GladMMO.ClaimedSessionsModel", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GladMMO.GuildEntryModel", b =>
                {
                    b.HasOne("GladMMO.CharacterEntryModel", "GuildMaster")
                        .WithMany()
                        .HasForeignKey("GuildMasterCharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GladMMO.ZoneInstanceEntryModel", b =>
                {
                    b.HasOne("GladMMO.WorldEntryModel", "WorldEntry")
                        .WithMany()
                        .HasForeignKey("WorldId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
