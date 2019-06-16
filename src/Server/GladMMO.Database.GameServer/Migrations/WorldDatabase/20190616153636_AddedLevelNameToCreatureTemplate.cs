using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations.WorldDatabase
{
    public partial class AddedLevelNameToCreatureTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatureName",
                table: "creature_template",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "MaximumLevel",
                table: "creature_template",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinimumLevel",
                table: "creature_template",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatureName",
                table: "creature_template");

            migrationBuilder.DropColumn(
                name: "MaximumLevel",
                table: "creature_template");

            migrationBuilder.DropColumn(
                name: "MinimumLevel",
                table: "creature_template");
        }
    }
}
