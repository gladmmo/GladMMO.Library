using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations.WorldDatabase
{
    public partial class AddedInitialSpellEffectEntryModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "spell_effect",
                columns: table => new
                {
                    SpellEffectId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    isDefault = table.Column<bool>(nullable: false),
                    EffectType = table.Column<int>(nullable: false),
                    BasePointsAdditiveLevelModifier = table.Column<float>(nullable: false),
                    EffectBasePoints = table.Column<int>(nullable: false),
                    EffectPointsDiceRange = table.Column<int>(nullable: false),
                    EffectTargetingType = table.Column<int>(nullable: false),
                    AdditionalEffectTargetingType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spell_effect", x => x.SpellEffectId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "spell_effect");
        }
    }
}
