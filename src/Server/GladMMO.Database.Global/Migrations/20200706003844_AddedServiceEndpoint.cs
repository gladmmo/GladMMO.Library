using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.Global.Migrations
{
    public partial class AddedServiceEndpoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "service_endpoints",
                columns: table => new
                {
                    ServiceId = table.Column<int>(nullable: false),
                    Locale = table.Column<int>(nullable: false),
                    Mode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_service_endpoints", x => new { x.ServiceId, x.Locale, x.Mode });
                    table.ForeignKey(
                        name: "FK_service_endpoints_services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "service_endpoints");
        }
    }
}
