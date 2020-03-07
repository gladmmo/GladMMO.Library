using System; using FreecraftCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations
{
    public partial class AddedGuildEntryMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "guild_entry",
                columns: table => new
                {
                    GuildId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GuildName = table.Column<string>(maxLength: 32, nullable: true),
                    GuildMasterCharacterId = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guild_entry", x => x.GuildId);
                    table.UniqueConstraint("AK_guild_entry_GuildMasterCharacterId", x => x.GuildMasterCharacterId);
                    table.ForeignKey(
                        name: "FK_guild_entry_characters_GuildMasterCharacterId",
                        column: x => x.GuildMasterCharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_guild_entry_GuildName",
                table: "guild_entry",
                column: "GuildName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "guild_entry");
        }
    }
}
