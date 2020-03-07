using System; using FreecraftCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations
{
    public partial class TransitioningToProjectVindictiveWorldSystemCharacter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_zone_endpoints_ZoneGuid",
                table: "zone_endpoints");

            migrationBuilder.DropColumn(
                name: "ZoneGuid",
                table: "zone_endpoints");

            migrationBuilder.DropColumn(
                name: "ZoneType",
                table: "zone_endpoints");

            migrationBuilder.DropColumn(
                name: "ZoneType",
                table: "character_locations");

            migrationBuilder.AddColumn<long>(
                name: "WorldId",
                table: "zone_endpoints",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "WorldId",
                table: "character_locations",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "world_entry",
                columns: table => new
                {
                    WorldId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    CreationIp = table.Column<string>(maxLength: 15, nullable: false),
                    StorageGuid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_world_entry", x => x.WorldId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_zone_endpoints_WorldId",
                table: "zone_endpoints",
                column: "WorldId");

            migrationBuilder.CreateIndex(
                name: "IX_character_locations_WorldId",
                table: "character_locations",
                column: "WorldId");

            migrationBuilder.AddForeignKey(
                name: "FK_character_locations_world_entry_WorldId",
                table: "character_locations",
                column: "WorldId",
                principalTable: "world_entry",
                principalColumn: "WorldId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_zone_endpoints_world_entry_WorldId",
                table: "zone_endpoints",
                column: "WorldId",
                principalTable: "world_entry",
                principalColumn: "WorldId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_character_locations_world_entry_WorldId",
                table: "character_locations");

            migrationBuilder.DropForeignKey(
                name: "FK_zone_endpoints_world_entry_WorldId",
                table: "zone_endpoints");

            migrationBuilder.DropTable(
                name: "world_entry");

            migrationBuilder.DropIndex(
                name: "IX_zone_endpoints_WorldId",
                table: "zone_endpoints");

            migrationBuilder.DropIndex(
                name: "IX_character_locations_WorldId",
                table: "character_locations");

            migrationBuilder.DropColumn(
                name: "WorldId",
                table: "zone_endpoints");

            migrationBuilder.DropColumn(
                name: "WorldId",
                table: "character_locations");

            migrationBuilder.AddColumn<Guid>(
                name: "ZoneGuid",
                table: "zone_endpoints",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "ZoneType",
                table: "zone_endpoints",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ZoneType",
                table: "character_locations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_zone_endpoints_ZoneGuid",
                table: "zone_endpoints",
                column: "ZoneGuid",
                unique: true);
        }
    }
}
