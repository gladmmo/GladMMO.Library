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

            modelBuilder.Entity("GladMMO.CreatureEntryModel", b =>
                {
                    b.Property<int>("CreatureEntryId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatureTemplateId");

                    b.Property<float>("InitialOrientation");

                    b.Property<long>("WorldId");

                    b.HasKey("CreatureEntryId");

                    b.HasIndex("CreatureTemplateId");

                    b.HasIndex("WorldId");

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

                    b.Property<int>("Version");

                    b.HasKey("CreatureId");

                    b.ToTable("creature_model");
                });

            modelBuilder.Entity("GladMMO.CreatureTemplateEntryModel", b =>
                {
                    b.Property<int>("CreatureTemplateId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountId");

                    b.Property<string>("CreatureName")
                        .IsRequired();

                    b.Property<int>("MaximumLevel");

                    b.Property<int>("MinimumLevel");

                    b.Property<long>("ModelId");

                    b.HasKey("CreatureTemplateId");

                    b.HasIndex("ModelId");

                    b.ToTable("creature_template");
                });

            modelBuilder.Entity("GladMMO.GameObjectAvatarPedestalEntryModel", b =>
                {
                    b.Property<int>("TargetGameObjectId");

                    b.Property<long>("AvatarModelId");

                    b.HasKey("TargetGameObjectId");

                    b.HasIndex("AvatarModelId");

                    b.ToTable("gameobject_avatarpedestal");
                });

            modelBuilder.Entity("GladMMO.GameObjectEntryModel", b =>
                {
                    b.Property<int>("GameObjectEntryId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GameObjectTemplateId");

                    b.Property<float>("InitialOrientation");

                    b.Property<long>("WorldId");

                    b.HasKey("GameObjectEntryId");

                    b.HasIndex("GameObjectTemplateId");

                    b.HasIndex("WorldId");

                    b.ToTable("gameobject");
                });

            modelBuilder.Entity("GladMMO.GameObjectModelEntryModel", b =>
                {
                    b.Property<long>("GameObjectModelId")
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

                    b.HasKey("GameObjectModelId");

                    b.ToTable("gameobject_model");
                });

            modelBuilder.Entity("GladMMO.GameObjectTemplateEntryModel", b =>
                {
                    b.Property<int>("GameObjectTemplateId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountId");

                    b.Property<string>("GameObjectName")
                        .IsRequired();

                    b.Property<long>("ModelId");

                    b.Property<int>("Type");

                    b.HasKey("GameObjectTemplateId");

                    b.HasIndex("ModelId");

                    b.ToTable("gameobject_template");
                });

            modelBuilder.Entity("GladMMO.GameObjectWorldTeleporterEntryModel", b =>
                {
                    b.Property<int>("TargetGameObjectId");

                    b.Property<int>("LocalSpawnPointId");

                    b.Property<int>("RemoteSpawnPointId");

                    b.HasKey("TargetGameObjectId");

                    b.HasIndex("LocalSpawnPointId");

                    b.HasIndex("RemoteSpawnPointId");

                    b.ToTable("gameobject_worldteleporter");
                });

            modelBuilder.Entity("GladMMO.PlayerSpawnPointEntryModel", b =>
                {
                    b.Property<int>("PlayerSpawnId")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("InitialOrientation");

                    b.Property<long>("WorldId");

                    b.Property<bool>("isReserved");

                    b.HasKey("PlayerSpawnId");

                    b.HasIndex("WorldId");

                    b.ToTable("player_spawnpoint");
                });

            modelBuilder.Entity("GladMMO.SpellEffectEntryModel", b =>
                {
                    b.Property<int>("SpellEffectId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdditionalEffectTargetingType");

                    b.Property<float>("BasePointsAdditiveLevelModifier");

                    b.Property<int>("EffectBasePoints");

                    b.Property<int>("EffectPointsDiceRange");

                    b.Property<int>("EffectTargetingType");

                    b.Property<int>("EffectType");

                    b.Property<bool>("isDefault");

                    b.HasKey("SpellEffectId");

                    b.ToTable("spell_effect");
                });

            modelBuilder.Entity("GladMMO.SpellEntryModel", b =>
                {
                    b.Property<int>("SpellId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CastTime");

                    b.Property<int>("SpellEffectIdOne");

                    b.Property<string>("SpellName")
                        .IsRequired();

                    b.Property<int>("SpellType");

                    b.Property<bool>("isDefault");

                    b.HasKey("SpellId");

                    b.HasIndex("SpellEffectIdOne");

                    b.ToTable("spell_entry");
                });

            modelBuilder.Entity("GladMMO.SpellLevelLearned", b =>
                {
                    b.Property<int>("SpellLearnId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CharacterClassType");

                    b.Property<int>("LevelLearned");

                    b.Property<int>("SpellId");

                    b.HasKey("SpellLearnId");

                    b.HasIndex("SpellId");

                    b.ToTable("spell_levellearned");
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

            modelBuilder.Entity("GladMMO.CreatureEntryModel", b =>
                {
                    b.HasOne("GladMMO.CreatureTemplateEntryModel", "CreatureTemplate")
                        .WithMany()
                        .HasForeignKey("CreatureTemplateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GladMMO.WorldEntryModel", "WorldEntry")
                        .WithMany()
                        .HasForeignKey("WorldId")
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

            modelBuilder.Entity("GladMMO.GameObjectAvatarPedestalEntryModel", b =>
                {
                    b.HasOne("GladMMO.AvatarEntryModel", "AvatarModel")
                        .WithMany()
                        .HasForeignKey("AvatarModelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GladMMO.GameObjectEntryModel", "TargetGameObject")
                        .WithMany()
                        .HasForeignKey("TargetGameObjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GladMMO.GameObjectEntryModel", b =>
                {
                    b.HasOne("GladMMO.GameObjectTemplateEntryModel", "GameObjectTemplate")
                        .WithMany()
                        .HasForeignKey("GameObjectTemplateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GladMMO.WorldEntryModel", "WorldEntry")
                        .WithMany()
                        .HasForeignKey("WorldId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("GladMMO.Database.Vector3<float>", "SpawnPosition", b1 =>
                        {
                            b1.Property<int>("GameObjectEntryModelGameObjectEntryId");

                            b1.Property<float>("X");

                            b1.Property<float>("Y");

                            b1.Property<float>("Z");

                            b1.HasKey("GameObjectEntryModelGameObjectEntryId");

                            b1.ToTable("gameobject");

                            b1.HasOne("GladMMO.GameObjectEntryModel")
                                .WithOne("SpawnPosition")
                                .HasForeignKey("GladMMO.Database.Vector3<float>", "GameObjectEntryModelGameObjectEntryId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("GladMMO.GameObjectTemplateEntryModel", b =>
                {
                    b.HasOne("GladMMO.GameObjectModelEntryModel", "GameObjectModelEntry")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GladMMO.GameObjectWorldTeleporterEntryModel", b =>
                {
                    b.HasOne("GladMMO.PlayerSpawnPointEntryModel", "LocalSpawnPoint")
                        .WithMany()
                        .HasForeignKey("LocalSpawnPointId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GladMMO.PlayerSpawnPointEntryModel", "RemoteSpawnPoint")
                        .WithMany()
                        .HasForeignKey("RemoteSpawnPointId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GladMMO.GameObjectEntryModel", "TargetGameObject")
                        .WithMany()
                        .HasForeignKey("TargetGameObjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GladMMO.PlayerSpawnPointEntryModel", b =>
                {
                    b.HasOne("GladMMO.WorldEntryModel", "WorldEntry")
                        .WithMany()
                        .HasForeignKey("WorldId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("GladMMO.Database.Vector3<float>", "SpawnPosition", b1 =>
                        {
                            b1.Property<int>("PlayerSpawnPointEntryModelPlayerSpawnId");

                            b1.Property<float>("X");

                            b1.Property<float>("Y");

                            b1.Property<float>("Z");

                            b1.HasKey("PlayerSpawnPointEntryModelPlayerSpawnId");

                            b1.ToTable("player_spawnpoint");

                            b1.HasOne("GladMMO.PlayerSpawnPointEntryModel")
                                .WithOne("SpawnPosition")
                                .HasForeignKey("GladMMO.Database.Vector3<float>", "PlayerSpawnPointEntryModelPlayerSpawnId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("GladMMO.SpellEntryModel", b =>
                {
                    b.HasOne("GladMMO.SpellEffectEntryModel", "SpellEffectOne")
                        .WithMany()
                        .HasForeignKey("SpellEffectIdOne")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GladMMO.SpellLevelLearned", b =>
                {
                    b.HasOne("GladMMO.SpellEffectEntryModel", "Spell")
                        .WithMany()
                        .HasForeignKey("SpellId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
