using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.Global.Migrations
{
    public partial class AddedServiceEndpointMissingEndpoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Endpoint_Address",
                table: "service_endpoints",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Endpoint_Port",
                table: "service_endpoints",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Endpoint_Address",
                table: "service_endpoints");

            migrationBuilder.DropColumn(
                name: "Endpoint_Port",
                table: "service_endpoints");
        }
    }
}
