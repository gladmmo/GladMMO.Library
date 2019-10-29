using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations
{
	public partial class DisabledZoneIdAutoGeneration : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("SET foreign_key_checks = 0;");
			migrationBuilder.AlterColumn<int>(
				name: "ZoneId",
				table: "zone_endpoints",
				nullable: false,
				oldClrType: typeof(int))
				.OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
			migrationBuilder.Sql("SET foreign_key_checks = 1;");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("SET foreign_key_checks = 0;");
			migrationBuilder.AlterColumn<int>(
				name: "ZoneId",
				table: "zone_endpoints",
				nullable: false,
				oldClrType: typeof(int))
				.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
			migrationBuilder.Sql("SET foreign_key_checks = 1;");
		}
	}
}
