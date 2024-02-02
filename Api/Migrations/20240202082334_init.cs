using System;
using Essensoft.Paylink.Alipay;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json.Linq;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TerraMours.Framework.Infrastructure.Contracts.Commons;
using TerraMours_Gpt.Framework.Infrastructure.Contracts.Commons;

#nullable disable

namespace TerraMours_Gpt_Api.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, computedColumnSql: "xmin"),
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
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatConversation",
                columns: table => new
                {
                    ConversationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConversationName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    KnowledgeId = table.Column<int>(type: "integer", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, computedColumnSql: "xmin"),
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
                    table.PrimaryKey("PK_ChatConversation", x => x.ConversationId);
                });

            migrationBuilder.CreateTable(
                name: "ChatRecord",
                columns: table => new
                {
                    ChatRecordId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModelType = table.Column<int>(type: "integer", nullable: true),
                    Model = table.Column<string>(type: "text", nullable: true),
                    ConversationId = table.Column<long>(type: "bigint", nullable: true),
                    IP = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Enable = table.Column<bool>(type: "boolean", nullable: false),
                    PromptTokens = table.Column<int>(type: "integer", nullable: true),
                    CompletionTokens = table.Column<int>(type: "integer", nullable: true),
                    TotalTokens = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRecord", x => x.ChatRecordId);
                });

            migrationBuilder.CreateTable(
                name: "CollectRecord",
                columns: table => new
                {
                    CollectRecordId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    RecordId = table.Column<long>(type: "bigint", nullable: true),
                    RecordType = table.Column<int>(type: "integer", nullable: true),
                    IP = table.Column<string>(type: "text", nullable: true),
                    Forward = table.Column<bool>(type: "boolean", nullable: true),
                    Collect = table.Column<bool>(type: "boolean", nullable: true),
                    Like = table.Column<bool>(type: "boolean", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, computedColumnSql: "xmin"),
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
                    table.PrimaryKey("PK_CollectRecord", x => x.CollectRecordId);
                });

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
                name: "GptOptions",
                columns: table => new
                {
                    GptOptionsId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OpenAIOptions = table.Column<OpenAIOptions>(type: "jsonb", nullable: true),
                    ImagOptions = table.Column<ImagOptions>(type: "jsonb", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, computedColumnSql: "xmin"),
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
                    table.PrimaryKey("PK_GptOptions", x => x.GptOptionsId);
                });

            migrationBuilder.CreateTable(
                name: "ImageRecord",
                columns: table => new
                {
                    ImageRecordId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModelType = table.Column<int>(type: "integer", nullable: true),
                    Model = table.Column<string>(type: "text", nullable: true),
                    IP = table.Column<string>(type: "text", nullable: true),
                    Prompt = table.Column<string>(type: "text", nullable: true),
                    PranslatePrompt = table.Column<string>(type: "text", nullable: true),
                    Size = table.Column<int>(type: "integer", nullable: true),
                    ImagUrl = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: true),
                    ForwardCount = table.Column<int>(type: "integer", nullable: true),
                    CollectCount = table.Column<int>(type: "integer", nullable: true),
                    LikeCount = table.Column<int>(type: "integer", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, computedColumnSql: "xmin"),
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
                    table.PrimaryKey("PK_ImageRecord", x => x.ImageRecordId);
                });

            migrationBuilder.CreateTable(
                name: "KeyOptions",
                columns: table => new
                {
                    KeyId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApiKey = table.Column<string>(type: "text", nullable: true),
                    ExpirationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Used = table.Column<decimal>(type: "numeric", nullable: true),
                    UnUsed = table.Column<decimal>(type: "numeric", nullable: true),
                    Total = table.Column<decimal>(type: "numeric", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, computedColumnSql: "xmin"),
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
                    table.PrimaryKey("PK_KeyOptions", x => x.KeyId);
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
                    WorkSpace = table.Column<string>(type: "text", nullable: true),
                    NameSpace = table.Column<string>(type: "text", nullable: true),
                    BaseUrl = table.Column<string>(type: "text", nullable: true),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<string>(type: "text", nullable: false),
                    TradeNo = table.Column<string>(type: "text", nullable: true),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false),
                    Stock = table.Column<int>(type: "integer", nullable: true),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PaidTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsVIP = table.Column<bool>(type: "boolean", nullable: true),
                    VipLevel = table.Column<int>(type: "integer", nullable: true),
                    VipTime = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PromptOptions",
                columns: table => new
                {
                    PromptId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Act = table.Column<string>(type: "text", nullable: true),
                    Prompt = table.Column<string>(type: "text", nullable: true),
                    UsedCount = table.Column<int>(type: "integer", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, computedColumnSql: "xmin"),
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
                    table.PrimaryKey("PK_PromptOptions", x => x.PromptId);
                });

            migrationBuilder.CreateTable(
                name: "Sensitive",
                columns: table => new
                {
                    SensitiveId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Word = table.Column<string>(type: "text", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, computedColumnSql: "xmin"),
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
                    table.PrimaryKey("PK_Sensitive", x => x.SensitiveId);
                });

            migrationBuilder.CreateTable(
                name: "SysDictionary",
                columns: table => new
                {
                    DictionaryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Dictionary = table.Column<JObject>(type: "jsonb", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, computedColumnSql: "xmin"),
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
                    table.PrimaryKey("PK_SysDictionary", x => x.DictionaryId);
                });

            migrationBuilder.CreateTable(
                name: "SysMenus",
                columns: table => new
                {
                    MenuId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    HasChildren = table.Column<bool>(type: "boolean", nullable: false),
                    MenuName = table.Column<string>(type: "text", nullable: false),
                    MenuUrl = table.Column<string>(type: "text", nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IconUrl = table.Column<string>(type: "text", nullable: true),
                    IsHome = table.Column<bool>(type: "boolean", nullable: false),
                    ExternalUrl = table.Column<bool>(type: "boolean", nullable: false),
                    IsShow = table.Column<bool>(type: "boolean", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, computedColumnSql: "xmin"),
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
                    table.PrimaryKey("PK_SysMenus", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "SysRole",
                columns: table => new
                {
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleName = table.Column<string>(type: "text", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    HasChildren = table.Column<bool>(type: "boolean", nullable: false),
                    DeptId = table.Column<long>(type: "bigint", nullable: true),
                    DeptName = table.Column<string>(type: "text", nullable: true),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: true),
                    IsNewUser = table.Column<bool>(type: "boolean", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, computedColumnSql: "xmin"),
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
                    table.PrimaryKey("PK_SysRole", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "SysSettings",
                columns: table => new
                {
                    SysSettingsId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Initial = table.Column<Initial>(type: "jsonb", nullable: true),
                    Email = table.Column<Email>(type: "jsonb", nullable: true),
                    Alipay = table.Column<AlipayOptions>(type: "jsonb", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, computedColumnSql: "xmin"),
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
                    table.PrimaryKey("PK_SysSettings", x => x.SysSettingsId);
                });

            migrationBuilder.CreateTable(
                name: "SysUser",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    UserEmail = table.Column<string>(type: "text", nullable: true),
                    UserPhoneNum = table.Column<string>(type: "text", nullable: true),
                    ShortPhone = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    UserPassword = table.Column<string>(type: "text", nullable: false),
                    IsRegregisterPhone = table.Column<bool>(type: "boolean", nullable: false),
                    UserTrueName = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    HeadImageUrl = table.Column<string>(type: "text", nullable: true),
                    DeptId = table.Column<string>(type: "text", nullable: true),
                    DeptName = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<long>(type: "bigint", nullable: true),
                    RoleName = table.Column<string>(type: "text", nullable: true),
                    Token = table.Column<string>(type: "text", nullable: true),
                    LoginFailCount = table.Column<int>(type: "integer", nullable: true),
                    EnableLogin = table.Column<bool>(type: "boolean", nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    ExpireTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    VipLevel = table.Column<int>(type: "integer", nullable: true),
                    VipExpireTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ImageCount = table.Column<int>(type: "integer", nullable: true),
                    Balance = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, computedColumnSql: "xmin"),
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
                    table.PrimaryKey("PK_SysUser", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Verification",
                columns: table => new
                {
                    VerificationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VerificationCode = table.Column<string>(type: "text", nullable: false),
                    CodeType = table.Column<int>(type: "integer", nullable: true),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: true),
                    UsedUserIds = table.Column<string[]>(type: "text[]", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, computedColumnSql: "xmin"),
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
                    table.PrimaryKey("PK_Verification", x => x.VerificationId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false),
                    Discount = table.Column<decimal>(type: "numeric", nullable: true),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
                    Stock = table.Column<int>(type: "integer", nullable: true),
                    IsVIP = table.Column<bool>(type: "boolean", nullable: true),
                    VipLevel = table.Column<int>(type: "integer", nullable: true),
                    VipTime = table.Column<int>(type: "integer", nullable: true),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, computedColumnSql: "xmin"),
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
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysRolesToMenu",
                columns: table => new
                {
                    RolesToMenuId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    MenuId = table.Column<long>(type: "bigint", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, computedColumnSql: "xmin"),
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
                    table.PrimaryKey("PK_SysRolesToMenu", x => x.RolesToMenuId);
                    table.ForeignKey(
                        name: "FK_SysRolesToMenu_SysRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SysRole",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "SysMenuButtons",
                columns: table => new
                {
                    MenuButtonId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MenuId = table.Column<long>(type: "bigint", nullable: false),
                    ButtonShowName = table.Column<string>(type: "text", nullable: false),
                    ButtonEnName = table.Column<string>(type: "text", nullable: false),
                    RolesToMenuId = table.Column<long>(type: "bigint", nullable: false),
                    SysRolesToMenuRolesToMenuId = table.Column<long>(type: "bigint", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, computedColumnSql: "xmin"),
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
                    table.PrimaryKey("PK_SysMenuButtons", x => x.MenuButtonId);
                    table.ForeignKey(
                        name: "FK_SysMenuButtons_SysRolesToMenu_SysRolesToMenuRolesToMenuId",
                        column: x => x.SysRolesToMenuRolesToMenuId,
                        principalTable: "SysRolesToMenu",
                        principalColumn: "RolesToMenuId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmbedRecord_ApiKey",
                table: "EmbedRecord",
                column: "ApiKey");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeItem_ApiKey",
                table: "KnowledgeItem",
                column: "ApiKey");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SysMenuButtons_SysRolesToMenuRolesToMenuId",
                table: "SysMenuButtons",
                column: "SysRolesToMenuRolesToMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_SysRolesToMenu_RoleId",
                table: "SysRolesToMenu",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatConversation");

            migrationBuilder.DropTable(
                name: "ChatRecord");

            migrationBuilder.DropTable(
                name: "CollectRecord");

            migrationBuilder.DropTable(
                name: "EmbedRecord");

            migrationBuilder.DropTable(
                name: "GptOptions");

            migrationBuilder.DropTable(
                name: "ImageRecord");

            migrationBuilder.DropTable(
                name: "KeyOptions");

            migrationBuilder.DropTable(
                name: "KnowledgeItem");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "PromptOptions");

            migrationBuilder.DropTable(
                name: "Sensitive");

            migrationBuilder.DropTable(
                name: "SysDictionary");

            migrationBuilder.DropTable(
                name: "SysMenuButtons");

            migrationBuilder.DropTable(
                name: "SysMenus");

            migrationBuilder.DropTable(
                name: "SysSettings");

            migrationBuilder.DropTable(
                name: "SysUser");

            migrationBuilder.DropTable(
                name: "Verification");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "SysRolesToMenu");

            migrationBuilder.DropTable(
                name: "SysRole");
        }
    }
}
