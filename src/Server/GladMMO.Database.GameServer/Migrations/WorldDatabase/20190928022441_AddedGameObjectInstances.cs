using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations.WorldDatabase
{
    public partial class AddedGameObjectInstances : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gameobject",
                columns: table => new
                {
                    GameObjectEntryId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GameObjectTemplateId = table.Column<int>(nullable: false),
                    SpawnPosition_X = table.Column<float>(nullable: false),
                    SpawnPosition_Y = table.Column<float>(nullable: false),
                    SpawnPosition_Z = table.Column<float>(nullable: false),
                    InitialOrientation = table.Column<float>(nullable: false),
                    WorldId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gameobject", x => x.GameObjectEntryId);
                    table.ForeignKey(
                        name: "FK_gameobject_creature_template_GameObjectTemplateId",
                        column: x => x.GameObjectTemplateId,
                        principalTable: "creature_template",
                        principalColumn: "CreatureTemplateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gameobject_world_entry_WorldId",
                        column: x => x.WorldId,
                        principalTable: "world_entry",
                        principalColumn: "WorldId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_gameobject_GameObjectTemplateId",
                table: "gameobject",
                column: "GameObjectTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_gameobject_WorldId",
                table: "gameobject",
                column: "WorldId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gameobject");
        }
    }
}
