using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations
{
    public partial class AddedDefaultActionBarTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "character_defaultactionbar",
                columns: table => new
                {
                    ClassType = table.Column<int>(nullable: false),
                    BarIndex = table.Column<int>(nullable: false),
                    LinkedSpellId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_defaultactionbar", x => new { x.ClassType, x.BarIndex });
                    table.ForeignKey(
                        name: "FK_character_defaultactionbar_spell_entry_LinkedSpellId",
                        column: x => x.LinkedSpellId,
                        principalTable: "spell_entry",
                        principalColumn: "SpellId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_character_defaultactionbar_ClassType",
                table: "character_defaultactionbar",
                column: "ClassType");

            migrationBuilder.CreateIndex(
                name: "IX_character_defaultactionbar_LinkedSpellId",
                table: "character_defaultactionbar",
                column: "LinkedSpellId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "character_defaultactionbar");
        }
    }
}
