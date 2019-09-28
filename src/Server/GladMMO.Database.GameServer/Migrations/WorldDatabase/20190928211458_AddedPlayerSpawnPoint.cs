using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations.WorldDatabase
{
    public partial class AddedPlayerSpawnPoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "player_spawnpoint",
                columns: table => new
                {
                    PlayerSpawnId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SpawnPosition_X = table.Column<float>(nullable: false),
                    SpawnPosition_Y = table.Column<float>(nullable: false),
                    SpawnPosition_Z = table.Column<float>(nullable: false),
                    InitialOrientation = table.Column<float>(nullable: false),
                    WorldId = table.Column<long>(nullable: false),
                    isReserved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player_spawnpoint", x => x.PlayerSpawnId);
                    table.ForeignKey(
                        name: "FK_player_spawnpoint_world_entry_WorldId",
                        column: x => x.WorldId,
                        principalTable: "world_entry",
                        principalColumn: "WorldId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_player_spawnpoint_WorldId",
                table: "player_spawnpoint",
                column: "WorldId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "player_spawnpoint");
        }
    }
}
