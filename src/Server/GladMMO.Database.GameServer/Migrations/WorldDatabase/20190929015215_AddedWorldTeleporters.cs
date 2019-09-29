using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations.WorldDatabase
{
    public partial class AddedWorldTeleporters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gameobject_worldteleporter",
                columns: table => new
                {
                    TargetGameObjectId = table.Column<int>(nullable: false),
                    LocalSpawnPointId = table.Column<int>(nullable: false),
                    RemoteSpawnPointId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gameobject_worldteleporter", x => x.TargetGameObjectId);
                    table.ForeignKey(
                        name: "FK_gameobject_worldteleporter_player_spawnpoint_LocalSpawnPoint~",
                        column: x => x.LocalSpawnPointId,
                        principalTable: "player_spawnpoint",
                        principalColumn: "PlayerSpawnId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gameobject_worldteleporter_player_spawnpoint_RemoteSpawnPoin~",
                        column: x => x.RemoteSpawnPointId,
                        principalTable: "player_spawnpoint",
                        principalColumn: "PlayerSpawnId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gameobject_worldteleporter_gameobject_TargetGameObjectId",
                        column: x => x.TargetGameObjectId,
                        principalTable: "gameobject",
                        principalColumn: "GameObjectEntryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_gameobject_worldteleporter_LocalSpawnPointId",
                table: "gameobject_worldteleporter",
                column: "LocalSpawnPointId");

            migrationBuilder.CreateIndex(
                name: "IX_gameobject_worldteleporter_RemoteSpawnPointId",
                table: "gameobject_worldteleporter",
                column: "RemoteSpawnPointId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gameobject_worldteleporter");
        }
    }
}
