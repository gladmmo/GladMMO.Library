using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations
{
    public partial class CharacterFriendsTableMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "character_friends",
                columns: table => new
                {
                    FriendshipId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CharacterOneId = table.Column<int>(nullable: false),
                    CharacterTwoId = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_friends", x => x.FriendshipId);
                    table.ForeignKey(
                        name: "FK_character_friends_characters_CharacterOneId",
                        column: x => x.CharacterOneId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_character_friends_characters_CharacterTwoId",
                        column: x => x.CharacterTwoId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_character_friends_CharacterOneId",
                table: "character_friends",
                column: "CharacterOneId");

            migrationBuilder.CreateIndex(
                name: "IX_character_friends_CharacterTwoId",
                table: "character_friends",
                column: "CharacterTwoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "character_friends");
        }
    }
}
