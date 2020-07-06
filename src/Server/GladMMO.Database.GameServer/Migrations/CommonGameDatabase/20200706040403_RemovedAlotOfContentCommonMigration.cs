using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations.CommonGameDatabase
{
    public partial class RemovedAlotOfContentCommonMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "path_waypoints");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "path_waypoints",
                columns: table => new
                {
                    PathId = table.Column<int>(nullable: false),
                    PointId = table.Column<int>(nullable: false),
                    Point_X = table.Column<float>(nullable: true),
                    Point_Y = table.Column<float>(nullable: true),
                    Point_Z = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_path_waypoints", x => new { x.PathId, x.PointId });
                });
        }
    }
}
