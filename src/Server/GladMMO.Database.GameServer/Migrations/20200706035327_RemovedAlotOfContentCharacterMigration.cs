using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations
{
    public partial class RemovedAlotOfContentCharacterMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "character_actionbar");

            migrationBuilder.DropTable(
                name: "character_appearance");

            migrationBuilder.DropTable(
                name: "character_defaultactionbar");

            migrationBuilder.DropTable(
                name: "avatar_entry");

            migrationBuilder.DropTable(
                name: "spell_entry");

            migrationBuilder.DropTable(
                name: "spell_effect");
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
                name: "character_appearance",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false),
                    AvatarModelId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_appearance", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_character_appearance_avatar_entry_AvatarModelId",
                        column: x => x.AvatarModelId,
                        principalTable: "avatar_entry",
                        principalColumn: "AvatarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_character_appearance_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
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
                });

            migrationBuilder.CreateTable(
                name: "character_actionbar",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false),
                    BarIndex = table.Column<int>(nullable: false),
                    LinkedSpellId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_actionbar", x => new { x.CharacterId, x.BarIndex });
                    table.ForeignKey(
                        name: "FK_character_actionbar_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_character_actionbar_spell_entry_LinkedSpellId",
                        column: x => x.LinkedSpellId,
                        principalTable: "spell_entry",
                        principalColumn: "SpellId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "character_defaultactionbar",
                columns: table => new
                {
                    ClassType = table.Column<int>(nullable: false),
                    BarIndex = table.Column<int>(nullable: false),
                    LinkedSpellId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_defaultactionbar", x => new { x.ClassType, x.BarIndex });
                    table.ForeignKey(
                        name: "FK_character_defaultactionbar_spell_entry_LinkedSpellId",
                        column: x => x.LinkedSpellId,
                        principalTable: "spell_entry",
                        principalColumn: "SpellId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_character_actionbar_CharacterId",
                table: "character_actionbar",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_character_actionbar_LinkedSpellId",
                table: "character_actionbar",
                column: "LinkedSpellId");

            migrationBuilder.CreateIndex(
                name: "IX_character_appearance_AvatarModelId",
                table: "character_appearance",
                column: "AvatarModelId");

            migrationBuilder.CreateIndex(
                name: "IX_character_defaultactionbar_ClassType",
                table: "character_defaultactionbar",
                column: "ClassType");

            migrationBuilder.CreateIndex(
                name: "IX_character_defaultactionbar_LinkedSpellId",
                table: "character_defaultactionbar",
                column: "LinkedSpellId");

            migrationBuilder.CreateIndex(
                name: "IX_spell_entry_SpellEffectIdOne",
                table: "spell_entry",
                column: "SpellEffectIdOne");
        }
    }
}
