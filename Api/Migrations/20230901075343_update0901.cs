using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TerraMours_Gpt.Migrations
{
    /// <inheritdoc />
    public partial class update0901 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SysMenuButtons_SysMenus_MenuId",
                table: "SysMenuButtons");

            migrationBuilder.DropForeignKey(
                name: "FK_SysMenuButtons_SysRolesToMenu_MenuId",
                table: "SysMenuButtons");

            migrationBuilder.DropIndex(
                name: "IX_SysMenuButtons_MenuId",
                table: "SysMenuButtons");

            migrationBuilder.AddColumn<long>(
                name: "SysRolesToMenuRolesToMenuId",
                table: "SysMenuButtons",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SysMenuButtons_SysRolesToMenuRolesToMenuId",
                table: "SysMenuButtons",
                column: "SysRolesToMenuRolesToMenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_SysMenuButtons_SysRolesToMenu_SysRolesToMenuRolesToMenuId",
                table: "SysMenuButtons",
                column: "SysRolesToMenuRolesToMenuId",
                principalTable: "SysRolesToMenu",
                principalColumn: "RolesToMenuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SysMenuButtons_SysRolesToMenu_SysRolesToMenuRolesToMenuId",
                table: "SysMenuButtons");

            migrationBuilder.DropIndex(
                name: "IX_SysMenuButtons_SysRolesToMenuRolesToMenuId",
                table: "SysMenuButtons");

            migrationBuilder.DropColumn(
                name: "SysRolesToMenuRolesToMenuId",
                table: "SysMenuButtons");

            migrationBuilder.CreateIndex(
                name: "IX_SysMenuButtons_MenuId",
                table: "SysMenuButtons",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_SysMenuButtons_SysMenus_MenuId",
                table: "SysMenuButtons",
                column: "MenuId",
                principalTable: "SysMenus",
                principalColumn: "MenuId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SysMenuButtons_SysRolesToMenu_MenuId",
                table: "SysMenuButtons",
                column: "MenuId",
                principalTable: "SysRolesToMenu",
                principalColumn: "RolesToMenuId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
