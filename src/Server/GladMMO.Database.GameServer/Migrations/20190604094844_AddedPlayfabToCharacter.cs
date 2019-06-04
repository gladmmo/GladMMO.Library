using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations
{
    public partial class AddedPlayfabToCharacter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlayFabCharacterId",
                table: "characters",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlayFabId",
                table: "characters",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_characters_PlayFabId_PlayFabCharacterId",
                table: "characters",
                columns: new[] { "PlayFabId", "PlayFabCharacterId" });

            migrationBuilder.CreateIndex(
                name: "IX_characters_PlayFabId",
                table: "characters",
                column: "PlayFabId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_characters_PlayFabId_PlayFabCharacterId",
                table: "characters");

            migrationBuilder.DropIndex(
                name: "IX_characters_PlayFabId",
                table: "characters");

            migrationBuilder.DropColumn(
                name: "PlayFabCharacterId",
                table: "characters");

            migrationBuilder.DropColumn(
                name: "PlayFabId",
                table: "characters");
        }
    }
}
