using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations.WorldDatabase
{
    public partial class AddedCreatureEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_creature_template_creature_model_entry_ModelId",
                table: "creature_template");

            migrationBuilder.DropPrimaryKey(
                name: "PK_creature_model_entry",
                table: "creature_model_entry");

            migrationBuilder.RenameTable(
                name: "creature_model_entry",
                newName: "creature_model");

            migrationBuilder.AddPrimaryKey(
                name: "PK_creature_model",
                table: "creature_model",
                column: "CreatureId");

            migrationBuilder.CreateTable(
                name: "creature",
                columns: table => new
                {
                    CreatureEntryId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatureTemplateId = table.Column<int>(nullable: false),
                    SpawnPosition_X = table.Column<float>(nullable: false),
                    SpawnPosition_Y = table.Column<float>(nullable: false),
                    SpawnPosition_Z = table.Column<float>(nullable: false),
                    InitialOrientation = table.Column<float>(nullable: false),
                    MapId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_creature", x => x.CreatureEntryId);
                    table.ForeignKey(
                        name: "FK_creature_creature_template_CreatureTemplateId",
                        column: x => x.CreatureTemplateId,
                        principalTable: "creature_template",
                        principalColumn: "CreatureTemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_creature_CreatureTemplateId",
                table: "creature",
                column: "CreatureTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_creature_template_creature_model_ModelId",
                table: "creature_template",
                column: "ModelId",
                principalTable: "creature_model",
                principalColumn: "CreatureId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_creature_template_creature_model_ModelId",
                table: "creature_template");

            migrationBuilder.DropTable(
                name: "creature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_creature_model",
                table: "creature_model");

            migrationBuilder.RenameTable(
                name: "creature_model",
                newName: "creature_model_entry");

            migrationBuilder.AddPrimaryKey(
                name: "PK_creature_model_entry",
                table: "creature_model_entry",
                column: "CreatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_creature_template_creature_model_entry_ModelId",
                table: "creature_template",
                column: "ModelId",
                principalTable: "creature_model_entry",
                principalColumn: "CreatureId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
