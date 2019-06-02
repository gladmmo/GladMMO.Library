using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations.CommonGameDatabase
{
    public partial class PathWaypointsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "path_waypoints",
                columns: table => new
                {
                    PathId = table.Column<int>(nullable: false),
                    PointId = table.Column<int>(nullable: false),
                    Point_X = table.Column<float>(nullable: false),
                    Point_Y = table.Column<float>(nullable: false),
                    Point_Z = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_path_waypoints", x => new { x.PathId, x.PointId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "path_waypoints");
        }
    }
}
