using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations.NpcDatabase
{
    public partial class InitialNPC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "npc_template",
                columns: table => new
                {
                    TemplateId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PrefabId = table.Column<int>(nullable: false),
                    NpcName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_npc_template", x => x.TemplateId);
                });

            migrationBuilder.CreateTable(
                name: "npc_entry",
                columns: table => new
                {
                    EntryId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NpcTemplateId = table.Column<int>(nullable: false),
                    SpawnPosition_X = table.Column<float>(nullable: false),
                    SpawnPosition_Y = table.Column<float>(nullable: false),
                    SpawnPosition_Z = table.Column<float>(nullable: false),
                    InitialOrientation = table.Column<float>(nullable: false),
                    MapId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_npc_entry", x => x.EntryId);
                    table.ForeignKey(
                        name: "FK_npc_entry_npc_template_NpcTemplateId",
                        column: x => x.NpcTemplateId,
                        principalTable: "npc_template",
                        principalColumn: "TemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_npc_entry_NpcTemplateId",
                table: "npc_entry",
                column: "NpcTemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "npc_entry");

            migrationBuilder.DropTable(
                name: "npc_template");
        }
    }
}
