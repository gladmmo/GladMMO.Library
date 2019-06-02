using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GladMMO.Database.GameServer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "characters",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: false),
                    CharacterName = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_characters", x => x.CharacterId);
                });

            migrationBuilder.CreateTable(
                name: "zone_endpoints",
                columns: table => new
                {
                    ZoneId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ZoneServerAddress = table.Column<string>(nullable: false),
                    ZoneServerPort = table.Column<short>(nullable: false),
                    ZoneType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_zone_endpoints", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "character_locations",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    XPosition = table.Column<float>(nullable: false),
                    YPosition = table.Column<float>(nullable: false),
                    ZPosition = table.Column<float>(nullable: false),
                    ZoneType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_locations", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_character_locations_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "character_sessions",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false),
                    SessionCreationDate = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SessionLastUpdateDate = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    ZoneId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_sessions", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_character_sessions_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_character_sessions_zone_endpoints_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "zone_endpoints",
                        principalColumn: "ZoneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "claimed_sessions",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false),
                    SessionCreationDate = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_claimed_sessions", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_claimed_sessions_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_claimed_sessions_character_sessions_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "character_sessions",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_character_sessions_CharacterId",
                table: "character_sessions",
                column: "CharacterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_character_sessions_ZoneId",
                table: "character_sessions",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_characters_AccountId",
                table: "characters",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_characters_CharacterName",
                table: "characters",
                column: "CharacterName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "character_locations");

            migrationBuilder.DropTable(
                name: "claimed_sessions");

            migrationBuilder.DropTable(
                name: "character_sessions");

            migrationBuilder.DropTable(
                name: "characters");

            migrationBuilder.DropTable(
                name: "zone_endpoints");
        }
    }
}
