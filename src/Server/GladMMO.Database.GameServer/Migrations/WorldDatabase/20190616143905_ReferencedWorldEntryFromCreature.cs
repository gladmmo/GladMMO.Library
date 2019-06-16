using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations.WorldDatabase
{
    public partial class ReferencedWorldEntryFromCreature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MapId",
                table: "creature");

            migrationBuilder.AddColumn<long>(
                name: "WorldId",
                table: "creature",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_creature_WorldId",
                table: "creature",
                column: "WorldId");

            migrationBuilder.AddForeignKey(
                name: "FK_creature_world_entry_WorldId",
                table: "creature",
                column: "WorldId",
                principalTable: "world_entry",
                principalColumn: "WorldId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_creature_world_entry_WorldId",
                table: "creature");

            migrationBuilder.DropIndex(
                name: "IX_creature_WorldId",
                table: "creature");

            migrationBuilder.DropColumn(
                name: "WorldId",
                table: "creature");

            migrationBuilder.AddColumn<int>(
                name: "MapId",
                table: "creature",
                nullable: false,
                defaultValue: 0);
        }
    }
}
