using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations.WorldDatabase
{
    public partial class AddedIconToSpellEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpellIconId",
                table: "spell_entry",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_spell_entry_SpellIconId",
                table: "spell_entry",
                column: "SpellIconId");

            migrationBuilder.AddForeignKey(
                name: "FK_spell_entry_clientcontent_icon_SpellIconId",
                table: "spell_entry",
                column: "SpellIconId",
                principalTable: "clientcontent_icon",
                principalColumn: "IconId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_spell_entry_clientcontent_icon_SpellIconId",
                table: "spell_entry");

            migrationBuilder.DropIndex(
                name: "IX_spell_entry_SpellIconId",
                table: "spell_entry");

            migrationBuilder.DropColumn(
                name: "SpellIconId",
                table: "spell_entry");
        }
    }
}
