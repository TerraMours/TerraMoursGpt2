using Essensoft.Paylink.Alipay;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TerraMours_Gpt.Migrations
{
    /// <inheritdoc />
    public partial class alipay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<AlipayOptions>(
                name: "Alipay",
                table: "SysSettings",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alipay",
                table: "SysSettings");
        }
    }
}
