using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations
{
    public partial class RemovedCharacterFriendship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "character_friendrelationship");

            migrationBuilder.DropTable(
                name: "character_friends");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "character_friendrelationship",
                columns: table => new
                {
                    FriendshipRelationshipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DirectionalUniqueness = table.Column<long>(type: "bigint", nullable: false),
                    RelationState = table.Column<int>(type: "int", nullable: false),
                    RequestingCharacterId = table.Column<int>(type: "int", nullable: false),
                    TargetRequestCharacterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_friendrelationship", x => x.FriendshipRelationshipId);
                    table.UniqueConstraint("AK_character_friendrelationship_RequestingCharacterId_TargetReq~", x => new { x.RequestingCharacterId, x.TargetRequestCharacterId });
                    table.ForeignKey(
                        name: "FK_character_friendrelationship_characters_RequestingCharacterId",
                        column: x => x.RequestingCharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_character_friendrelationship_characters_TargetRequestCharact~",
                        column: x => x.TargetRequestCharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "character_friends",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    FriendCharacterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_friends", x => new { x.CharacterId, x.FriendCharacterId });
                    table.ForeignKey(
                        name: "FK_character_friends_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_character_friends_characters_FriendCharacterId",
                        column: x => x.FriendCharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_character_friendrelationship_DirectionalUniqueness",
                table: "character_friendrelationship",
                column: "DirectionalUniqueness",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_character_friendrelationship_TargetRequestCharacterId",
                table: "character_friendrelationship",
                column: "TargetRequestCharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_character_friends_CharacterId",
                table: "character_friends",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_character_friends_FriendCharacterId",
                table: "character_friends",
                column: "FriendCharacterId");
        }
    }
}
