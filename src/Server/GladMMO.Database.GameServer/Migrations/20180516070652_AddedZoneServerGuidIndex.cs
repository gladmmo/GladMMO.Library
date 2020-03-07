using Microsoft.EntityFrameworkCore.Migrations;
using System; using FreecraftCore;
using System.Collections.Generic;

namespace GladMMO.Database.GameServer.Migrations
{
    public partial class AddedZoneServerGuidIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ZoneGuid",
                table: "zone_endpoints",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_zone_endpoints_ZoneGuid",
                table: "zone_endpoints",
                column: "ZoneGuid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_zone_endpoints_ZoneGuid",
                table: "zone_endpoints");

            migrationBuilder.DropColumn(
                name: "ZoneGuid",
                table: "zone_endpoints");
        }
    }
}
