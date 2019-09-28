using Microsoft.EntityFrameworkCore.Migrations;

namespace GladMMO.Database.GameServer.Migrations.WorldDatabase
{
    public partial class FixedGameObjectInstance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gameobject_creature_template_GameObjectTemplateId",
                table: "gameobject");

            migrationBuilder.AddForeignKey(
                name: "FK_gameobject_gameobject_template_GameObjectTemplateId",
                table: "gameobject",
                column: "GameObjectTemplateId",
                principalTable: "gameobject_template",
                principalColumn: "GameObjectTemplateId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gameobject_gameobject_template_GameObjectTemplateId",
                table: "gameobject");

            migrationBuilder.AddForeignKey(
                name: "FK_gameobject_creature_template_GameObjectTemplateId",
                table: "gameobject",
                column: "GameObjectTemplateId",
                principalTable: "creature_template",
                principalColumn: "CreatureTemplateId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
