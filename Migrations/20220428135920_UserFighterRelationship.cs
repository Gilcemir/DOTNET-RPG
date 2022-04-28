using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DOTNET_RPG.Migrations
{
    public partial class UserFighterRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Fighters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fighters_UserId",
                table: "Fighters",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fighters_Users_UserId",
                table: "Fighters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fighters_Users_UserId",
                table: "Fighters");

            migrationBuilder.DropIndex(
                name: "IX_Fighters_UserId",
                table: "Fighters");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Fighters");
        }
    }
}
