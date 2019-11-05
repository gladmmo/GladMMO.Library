using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations.WorldDatabase
{
    public partial class AddedInitialSpellEntryModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "spell_entry",
                columns: table => new
                {
                    SpellId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SpellName = table.Column<string>(nullable: false),
                    SpellType = table.Column<int>(nullable: false),
                    isDefault = table.Column<bool>(nullable: false),
                    CastTime = table.Column<int>(nullable: false),
                    SpellEffectIdOne = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spell_entry", x => x.SpellId);
                    table.ForeignKey(
                        name: "FK_spell_entry_spell_effect_SpellEffectIdOne",
                        column: x => x.SpellEffectIdOne,
                        principalTable: "spell_effect",
                        principalColumn: "SpellEffectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_spell_entry_SpellEffectIdOne",
                table: "spell_entry",
                column: "SpellEffectIdOne");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "spell_entry");
        }
    }
}
