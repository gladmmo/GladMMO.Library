using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GladMMO.Database.GameServer.Migrations
{
    public partial class RemovedMultipleMappedForeignKeyOnClaimedSessions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_claimed_sessions_characters_CharacterId",
                table: "claimed_sessions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_claimed_sessions_characters_CharacterId",
                table: "claimed_sessions",
                column: "CharacterId",
                principalTable: "characters",
                principalColumn: "CharacterId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
