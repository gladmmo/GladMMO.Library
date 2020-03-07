using System; using FreecraftCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations
{
    public partial class GuildMemberTableAddedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "guild_charactermember",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false),
                    GuildId = table.Column<int>(nullable: false),
                    JoinDate = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guild_charactermember", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_guild_charactermember_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_guild_charactermember_guild_entry_GuildId",
                        column: x => x.GuildId,
                        principalTable: "guild_entry",
                        principalColumn: "GuildId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_guild_charactermember_GuildId",
                table: "guild_charactermember",
                column: "GuildId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "guild_charactermember");
        }
    }
}
