using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations.NpcDatabase
{
    public partial class NpcMovementDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovementData",
                table: "npc_entry",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte>(
                name: "MovementType",
                table: "npc_entry",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovementData",
                table: "npc_entry");

            migrationBuilder.DropColumn(
                name: "MovementType",
                table: "npc_entry");
        }
    }
}
