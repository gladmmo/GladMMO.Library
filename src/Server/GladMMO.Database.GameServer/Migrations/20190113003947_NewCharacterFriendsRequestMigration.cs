using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations
{
    public partial class NewCharacterFriendsRequestMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "character_friendrequests",
                columns: table => new
                {
                    FriendshipRequestId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RequestingCharacterId = table.Column<int>(nullable: false),
                    TargetRequestCharacterId = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TIMESTAMP(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DirectionalUniqueness = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_friendrequests", x => x.FriendshipRequestId);
                    table.UniqueConstraint("AK_character_friendrequests_RequestingCharacterId_TargetRequest~", x => new { x.RequestingCharacterId, x.TargetRequestCharacterId });
                    table.ForeignKey(
                        name: "FK_character_friendrequests_characters_RequestingCharacterId",
                        column: x => x.RequestingCharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_character_friendrequests_characters_TargetRequestCharacterId",
                        column: x => x.TargetRequestCharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_character_friendrequests_DirectionalUniqueness",
                table: "character_friendrequests",
                column: "DirectionalUniqueness",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_character_friendrequests_TargetRequestCharacterId",
                table: "character_friendrequests",
                column: "TargetRequestCharacterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "character_friendrequests");
        }
    }
}
