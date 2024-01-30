using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TerraMours_Gpt.Migrations
{
    /// <inheritdoc />
    public partial class knowledge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmbedRecord",
                columns: table => new
                {
                    EmbedRecordId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmbedType = table.Column<int>(type: "integer", nullable: true),
                    ApiKey = table.Column<string>(type: "text", nullable: true),
                    Result = table.Column<string>(type: "jsonb", nullable: true),
                    Input = table.Column<string>(type: "jsonb", nullable: true),
                    PromptTokens = table.Column<int>(type: "integer", nullable: true),
                    CompletionTokens = table.Column<int>(type: "integer", nullable: true),
                    TotalTokens = table.Column<int>(type: "integer", nullable: true),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    Enable = table.Column<bool>(type: "boolean", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateID = table.Column<long>(type: "bigint", nullable: true),
                    CreatorName = table.Column<string>(type: "text", nullable: true),
                    ModifyID = table.Column<long>(type: "bigint", nullable: true),
                    ModifierName = table.Column<string>(type: "text", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Remark = table.Column<string>(type: "text", nullable: true),
                    OrderNo = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmbedRecord", x => x.EmbedRecordId);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeItem",
                columns: table => new
                {
                    KnowledgeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KnowledgeName = table.Column<string>(type: "text", nullable: true),
                    IsCommon = table.Column<bool>(type: "boolean", nullable: true),
                    KnowledgeType = table.Column<int>(type: "integer", nullable: true),
                    ApiKey = table.Column<string>(type: "text", nullable: true),
                    IndexName = table.Column<string>(type: "text", nullable: true),
                    NamespaceName = table.Column<string>(type: "text", nullable: true),
                    BaseUrl = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    Enable = table.Column<bool>(type: "boolean", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateID = table.Column<long>(type: "bigint", nullable: true),
                    CreatorName = table.Column<string>(type: "text", nullable: true),
                    ModifyID = table.Column<long>(type: "bigint", nullable: true),
                    ModifierName = table.Column<string>(type: "text", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Remark = table.Column<string>(type: "text", nullable: true),
                    OrderNo = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeItem", x => x.KnowledgeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmbedRecord_ApiKey",
                table: "EmbedRecord",
                column: "ApiKey");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeItem_ApiKey",
                table: "KnowledgeItem",
                column: "ApiKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmbedRecord");

            migrationBuilder.DropTable(
                name: "KnowledgeItem");
        }
    }
}
