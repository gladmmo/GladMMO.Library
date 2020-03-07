using System; using FreecraftCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations
{
    public partial class AddedCharacterAppearance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_character_appearance_AvatarModelId",
                table: "character_appearance",
                column: "AvatarModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "character_appearance");
        }
    }
}
