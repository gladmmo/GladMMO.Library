using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations
{
    public partial class FriendRequestsToRelationshipMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "character_friendrequests",
                newName: "character_friendrelationship");

            migrationBuilder.RenameColumn(
                name: "FriendshipRequestId",
                table: "character_friendrelationship",
                newName: "FriendshipRelationshipId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "character_friendrelationship",
                newName: "character_friendrequests");

            migrationBuilder.RenameColumn(
                name: "FriendshipRelationshipId",
                table: "character_friendrequests",
                newName: "FriendshipRequestId");
        }
    }
}
