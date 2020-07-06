using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations
{
    public partial class RemovedGuildAndGroupMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "group_entry");

            migrationBuilder.DropTable(
                name: "group_invites");

            migrationBuilder.DropTable(
                name: "group_members");

            migrationBuilder.DropTable(
                name: "guild_charactermember");

            migrationBuilder.DropTable(
                name: "guild_entry");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "group_entry",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    JoinDate = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LeaderCharacterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_entry", x => x.GroupId);
                    table.UniqueConstraint("AK_group_entry_LeaderCharacterId", x => x.LeaderCharacterId);
                    table.ForeignKey(
                        name: "FK_group_entry_characters_LeaderCharacterId",
                        column: x => x.LeaderCharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "guild_entry",
                columns: table => new
                {
                    GuildId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GuildMasterCharacterId = table.Column<int>(type: "int", nullable: false),
                    GuildName = table.Column<string>(type: "varchar(32) CHARACTER SET utf8mb4", maxLength: 32, nullable: true)
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

            migrationBuilder.CreateTable(
                name: "group_invites",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    InviteExpirationTime = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_invites", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_group_invites_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_group_invites_guild_entry_GroupId",
                        column: x => x.GroupId,
                        principalTable: "guild_entry",
                        principalColumn: "GuildId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "group_members",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_members", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_group_members_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_group_members_guild_entry_GroupId",
                        column: x => x.GroupId,
                        principalTable: "guild_entry",
                        principalColumn: "GuildId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "guild_charactermember",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    GuildId = table.Column<int>(type: "int", nullable: false),
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
                name: "IX_group_invites_GroupId",
                table: "group_invites",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_group_members_GroupId",
                table: "group_members",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_guild_charactermember_GuildId",
                table: "guild_charactermember",
                column: "GuildId");

            migrationBuilder.CreateIndex(
                name: "IX_guild_entry_GuildName",
                table: "guild_entry",
                column: "GuildName",
                unique: true);
        }
    }
}
