using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TerraMours_Gpt.Migrations
{
    /// <inheritdoc />
    public partial class knowledgeaddimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "KnowledgeItem",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "KnowledgeItem");
        }
    }
}
