using Microsoft.EntityFrameworkCore.Migrations;

namespace GameApi.Migrations
{
    public partial class AddPlatformGameRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GamePlatform",
                columns: table => new
                {
                    GamesID = table.Column<int>(type: "int", nullable: false),
                    PlatformsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlatform", x => new { x.GamesID, x.PlatformsID });
                    table.ForeignKey(
                        name: "FK_GamePlatform_Games_GamesID",
                        column: x => x.GamesID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GamePlatform_Platforms_PlatformsID",
                        column: x => x.PlatformsID,
                        principalTable: "Platforms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatform_PlatformsID",
                table: "GamePlatform",
                column: "PlatformsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePlatform");
        }
    }
}
