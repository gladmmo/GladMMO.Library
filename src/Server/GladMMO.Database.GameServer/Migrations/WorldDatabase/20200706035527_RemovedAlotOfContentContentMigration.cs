using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations.WorldDatabase
{
    public partial class RemovedAlotOfContentContentMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "creature");

            migrationBuilder.DropTable(
                name: "gameobject_avatarpedestal");

            migrationBuilder.DropTable(
                name: "gameobject_worldteleporter");

            migrationBuilder.DropTable(
                name: "spell_entry");

            migrationBuilder.DropTable(
                name: "spell_levellearned");

            migrationBuilder.DropTable(
                name: "creature_template");

            migrationBuilder.DropTable(
                name: "avatar_entry");

            migrationBuilder.DropTable(
                name: "player_spawnpoint");

            migrationBuilder.DropTable(
                name: "gameobject");

            migrationBuilder.DropTable(
                name: "clientcontent_icon");

            migrationBuilder.DropTable(
                name: "spell_effect");

            migrationBuilder.DropTable(
                name: "creature_model");

            migrationBuilder.DropTable(
                name: "gameobject_template");

            migrationBuilder.DropTable(
                name: "gameobject_model");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "avatar_entry",
                columns: table => new
                {
                    AvatarId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: false),
                    CreationIp = table.Column<string>(maxLength: 15, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    IsValidated = table.Column<bool>(nullable: false),
                    StorageGuid = table.Column<Guid>(nullable: false),
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_avatar_entry", x => x.AvatarId);
                });

            migrationBuilder.CreateTable(
                name: "clientcontent_icon",
                columns: table => new
                {
                    IconId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IconPathName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientcontent_icon", x => x.IconId);
                });

            migrationBuilder.CreateTable(
                name: "creature_model",
                columns: table => new
                {
                    CreatureId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: false),
                    CreationIp = table.Column<string>(maxLength: 15, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    IsValidated = table.Column<bool>(nullable: false),
                    StorageGuid = table.Column<Guid>(nullable: false),
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_creature_model", x => x.CreatureId);
                });

            migrationBuilder.CreateTable(
                name: "gameobject_model",
                columns: table => new
                {
                    GameObjectModelId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: false),
                    CreationIp = table.Column<string>(maxLength: 15, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    IsValidated = table.Column<bool>(nullable: false),
                    StorageGuid = table.Column<Guid>(nullable: false),
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gameobject_model", x => x.GameObjectModelId);
                });

            migrationBuilder.CreateTable(
                name: "player_spawnpoint",
                columns: table => new
                {
                    PlayerSpawnId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InitialOrientation = table.Column<float>(nullable: false),
                    WorldId = table.Column<long>(nullable: false),
                    isReserved = table.Column<bool>(nullable: false),
                    SpawnPosition_X = table.Column<float>(nullable: true),
                    SpawnPosition_Y = table.Column<float>(nullable: true),
                    SpawnPosition_Z = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player_spawnpoint", x => x.PlayerSpawnId);
                    table.ForeignKey(
                        name: "FK_player_spawnpoint_world_entry_WorldId",
                        column: x => x.WorldId,
                        principalTable: "world_entry",
                        principalColumn: "WorldId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "spell_effect",
                columns: table => new
                {
                    SpellEffectId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AdditionalEffectTargetingType = table.Column<int>(nullable: false),
                    BasePointsAdditiveLevelModifier = table.Column<float>(nullable: false),
                    EffectBasePoints = table.Column<int>(nullable: false),
                    EffectPointsDiceRange = table.Column<int>(nullable: false),
                    EffectTargetingType = table.Column<int>(nullable: false),
                    EffectType = table.Column<int>(nullable: false),
                    isDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spell_effect", x => x.SpellEffectId);
                });

            migrationBuilder.CreateTable(
                name: "creature_template",
                columns: table => new
                {
                    CreatureTemplateId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: false),
                    CreatureName = table.Column<string>(nullable: false),
                    MaximumLevel = table.Column<int>(nullable: false),
                    MinimumLevel = table.Column<int>(nullable: false),
                    ModelId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_creature_template", x => x.CreatureTemplateId);
                    table.ForeignKey(
                        name: "FK_creature_template_creature_model_ModelId",
                        column: x => x.ModelId,
                        principalTable: "creature_model",
                        principalColumn: "CreatureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gameobject_template",
                columns: table => new
                {
                    GameObjectTemplateId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: false),
                    GameObjectName = table.Column<string>(nullable: false),
                    ModelId = table.Column<long>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gameobject_template", x => x.GameObjectTemplateId);
                    table.ForeignKey(
                        name: "FK_gameobject_template_gameobject_model_ModelId",
                        column: x => x.ModelId,
                        principalTable: "gameobject_model",
                        principalColumn: "GameObjectModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "spell_entry",
                columns: table => new
                {
                    SpellId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CastTime = table.Column<int>(nullable: false),
                    SpellEffectIdOne = table.Column<int>(nullable: false),
                    SpellIconId = table.Column<int>(nullable: false),
                    SpellName = table.Column<string>(nullable: false),
                    SpellType = table.Column<int>(nullable: false),
                    isDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spell_entry", x => x.SpellId);
                    table.ForeignKey(
                        name: "FK_spell_entry_spell_effect_SpellEffectIdOne",
                        column: x => x.SpellEffectIdOne,
                        principalTable: "spell_effect",
                        principalColumn: "SpellEffectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_spell_entry_clientcontent_icon_SpellIconId",
                        column: x => x.SpellIconId,
                        principalTable: "clientcontent_icon",
                        principalColumn: "IconId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "spell_levellearned",
                columns: table => new
                {
                    SpellLearnId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CharacterClassType = table.Column<int>(nullable: false),
                    LevelLearned = table.Column<int>(nullable: false),
                    SpellId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spell_levellearned", x => x.SpellLearnId);
                    table.ForeignKey(
                        name: "FK_spell_levellearned_spell_effect_SpellId",
                        column: x => x.SpellId,
                        principalTable: "spell_effect",
                        principalColumn: "SpellEffectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "creature",
                columns: table => new
                {
                    CreatureEntryId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatureTemplateId = table.Column<int>(nullable: false),
                    InitialOrientation = table.Column<float>(nullable: false),
                    WorldId = table.Column<long>(nullable: false),
                    SpawnPosition_X = table.Column<float>(nullable: true),
                    SpawnPosition_Y = table.Column<float>(nullable: true),
                    SpawnPosition_Z = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_creature", x => x.CreatureEntryId);
                    table.ForeignKey(
                        name: "FK_creature_creature_template_CreatureTemplateId",
                        column: x => x.CreatureTemplateId,
                        principalTable: "creature_template",
                        principalColumn: "CreatureTemplateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_creature_world_entry_WorldId",
                        column: x => x.WorldId,
                        principalTable: "world_entry",
                        principalColumn: "WorldId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gameobject",
                columns: table => new
                {
                    GameObjectEntryId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GameObjectTemplateId = table.Column<int>(nullable: false),
                    InitialOrientation = table.Column<float>(nullable: false),
                    WorldId = table.Column<long>(nullable: false),
                    SpawnPosition_X = table.Column<float>(nullable: true),
                    SpawnPosition_Y = table.Column<float>(nullable: true),
                    SpawnPosition_Z = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gameobject", x => x.GameObjectEntryId);
                    table.ForeignKey(
                        name: "FK_gameobject_gameobject_template_GameObjectTemplateId",
                        column: x => x.GameObjectTemplateId,
                        principalTable: "gameobject_template",
                        principalColumn: "GameObjectTemplateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gameobject_world_entry_WorldId",
                        column: x => x.WorldId,
                        principalTable: "world_entry",
                        principalColumn: "WorldId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gameobject_avatarpedestal",
                columns: table => new
                {
                    TargetGameObjectId = table.Column<int>(nullable: false),
                    AvatarModelId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gameobject_avatarpedestal", x => x.TargetGameObjectId);
                    table.ForeignKey(
                        name: "FK_gameobject_avatarpedestal_avatar_entry_AvatarModelId",
                        column: x => x.AvatarModelId,
                        principalTable: "avatar_entry",
                        principalColumn: "AvatarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gameobject_avatarpedestal_gameobject_TargetGameObjectId",
                        column: x => x.TargetGameObjectId,
                        principalTable: "gameobject",
                        principalColumn: "GameObjectEntryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gameobject_worldteleporter",
                columns: table => new
                {
                    TargetGameObjectId = table.Column<int>(nullable: false),
                    LocalSpawnPointId = table.Column<int>(nullable: false),
                    RemoteSpawnPointId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gameobject_worldteleporter", x => x.TargetGameObjectId);
                    table.ForeignKey(
                        name: "FK_gameobject_worldteleporter_player_spawnpoint_LocalSpawnPoint~",
                        column: x => x.LocalSpawnPointId,
                        principalTable: "player_spawnpoint",
                        principalColumn: "PlayerSpawnId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gameobject_worldteleporter_player_spawnpoint_RemoteSpawnPoin~",
                        column: x => x.RemoteSpawnPointId,
                        principalTable: "player_spawnpoint",
                        principalColumn: "PlayerSpawnId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gameobject_worldteleporter_gameobject_TargetGameObjectId",
                        column: x => x.TargetGameObjectId,
                        principalTable: "gameobject",
                        principalColumn: "GameObjectEntryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_creature_CreatureTemplateId",
                table: "creature",
                column: "CreatureTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_creature_WorldId",
                table: "creature",
                column: "WorldId");

            migrationBuilder.CreateIndex(
                name: "IX_creature_template_ModelId",
                table: "creature_template",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_gameobject_GameObjectTemplateId",
                table: "gameobject",
                column: "GameObjectTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_gameobject_WorldId",
                table: "gameobject",
                column: "WorldId");

            migrationBuilder.CreateIndex(
                name: "IX_gameobject_avatarpedestal_AvatarModelId",
                table: "gameobject_avatarpedestal",
                column: "AvatarModelId");

            migrationBuilder.CreateIndex(
                name: "IX_gameobject_template_ModelId",
                table: "gameobject_template",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_gameobject_worldteleporter_LocalSpawnPointId",
                table: "gameobject_worldteleporter",
                column: "LocalSpawnPointId");

            migrationBuilder.CreateIndex(
                name: "IX_gameobject_worldteleporter_RemoteSpawnPointId",
                table: "gameobject_worldteleporter",
                column: "RemoteSpawnPointId");

            migrationBuilder.CreateIndex(
                name: "IX_player_spawnpoint_WorldId",
                table: "player_spawnpoint",
                column: "WorldId");

            migrationBuilder.CreateIndex(
                name: "IX_spell_entry_SpellEffectIdOne",
                table: "spell_entry",
                column: "SpellEffectIdOne");

            migrationBuilder.CreateIndex(
                name: "IX_spell_entry_SpellIconId",
                table: "spell_entry",
                column: "SpellIconId");

            migrationBuilder.CreateIndex(
                name: "IX_spell_levellearned_SpellId",
                table: "spell_levellearned",
                column: "SpellId");
        }
    }
}
