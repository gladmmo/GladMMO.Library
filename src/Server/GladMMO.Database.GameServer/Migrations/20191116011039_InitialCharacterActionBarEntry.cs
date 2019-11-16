using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations
{
	public partial class InitialCharacterActionBarEntry : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "character_actionbar",
				columns: table => new
				{
					CharacterId = table.Column<int>(nullable: false),
					BarIndex = table.Column<int>(nullable: false),
					LinkedSpellId = table.Column<int>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_character_actionbar", x => new { x.CharacterId, x.BarIndex });
					table.ForeignKey(
						name: "FK_character_actionbar_characters_CharacterId",
						column: x => x.CharacterId,
						principalTable: "characters",
						principalColumn: "CharacterId",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_character_actionbar_spell_entry_LinkedSpellId",
						column: x => x.LinkedSpellId,
						principalTable: "spell_entry",
						principalColumn: "SpellId",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "IX_character_actionbar_CharacterId",
				table: "character_actionbar",
				column: "CharacterId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "character_actionbar");
		}
	}
}
