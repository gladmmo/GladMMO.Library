using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations.WorldDatabase
{
    public partial class AddedAvatarPedestalModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gameobject_avatarpedestal",
                columns: table => new
                {
                    TargetGameObjectId = table.Column<int>(nullable: false),
                    AvatarModelId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gameobject_avatarpedestal", x => x.TargetGameObjectId);
                    table.ForeignKey(
                        name: "FK_gameobject_avatarpedestal_avatar_entry_AvatarModelId",
                        column: x => x.AvatarModelId,
                        principalTable: "avatar_entry",
                        principalColumn: "AvatarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gameobject_avatarpedestal_gameobject_TargetGameObjectId",
                        column: x => x.TargetGameObjectId,
                        principalTable: "gameobject",
                        principalColumn: "GameObjectEntryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_gameobject_avatarpedestal_AvatarModelId",
                table: "gameobject_avatarpedestal",
                column: "AvatarModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gameobject_avatarpedestal");
        }
    }
}
