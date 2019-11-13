using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations.WorldDatabase
{
    public partial class InitialLevelLearnedSpellsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "spell_levellearned",
                columns: table => new
                {
                    SpellLearnId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CharacterClassType = table.Column<int>(nullable: false),
                    LevelLearned = table.Column<int>(nullable: false),
                    SpellId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spell_levellearned", x => x.SpellLearnId);
                    table.ForeignKey(
                        name: "FK_spell_levellearned_spell_effect_SpellId",
                        column: x => x.SpellId,
                        principalTable: "spell_effect",
                        principalColumn: "SpellEffectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_spell_levellearned_SpellId",
                table: "spell_levellearned",
                column: "SpellId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "spell_levellearned");
        }
    }
}
