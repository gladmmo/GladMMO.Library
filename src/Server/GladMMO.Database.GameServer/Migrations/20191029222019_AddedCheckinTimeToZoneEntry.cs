using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations
{
    public partial class AddedCheckinTimeToZoneEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LastCheckinTime",
                table: "zone_endpoints",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "RegistrationTime",
                table: "zone_endpoints",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastCheckinTime",
                table: "zone_endpoints");

            migrationBuilder.DropColumn(
                name: "RegistrationTime",
                table: "zone_endpoints");
        }
    }
}
