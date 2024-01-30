using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TerraMours_Gpt.Migrations
{
    /// <inheritdoc />
    public partial class updateknowledge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameSpace",
                table: "KnowledgeItem",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameSpace",
                table: "KnowledgeItem");
        }
    }
}
