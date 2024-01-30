using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TerraMours.Framework.Infrastructure.Contracts.SystemModels;
using TerraMours_Gpt.Framework.Infrastructure.Contracts.GptModels;
using TerraMours_Gpt.Framework.Infrastructure.Contracts.PaymentModels;
using TerraMours_Gpt.Framework.Infrastructure.Contracts.ProductModels;
using TerraMours_Gpt.Framework.Infrastructure.Contracts.SystemModels;
using TerraMours_Gpt_Api.Framework.Infrastructure.Contracts.GptModels;

namespace TerraMours.Framework.Infrastructure.EFCore
{
    public class FrameworkDbContext : DbContext
    {
        //数据库表
        public DbSet<SysUser> SysUsers { get; set; }
        public DbSet<SysRole> SysRoles { get; set; }
        public DbSet<SysMenus> SysMenus { get; set; }
        public DbSet<SysMenuButtons> SysMenuButtons { get; set; }
        public DbSet<SysRolesToMenu> SysRolesToMenus { get; set; }
        public DbSet<SysDictionary> SysDictionaries { get; set; }
        public DbSet<SysSettingsEntity> SysSettings { get; set; }
        #region 业务
        public DbSet<ChatConversation> ChatConversations { get; set; }
        public DbSet<ChatRecord> ChatRecords { get; set; }
        public DbSet<CollectRecord> CollectRecords { get; set; }
        public DbSet<GptOptionsEntity> GptOptions { get; set; }
        public DbSet<ImageRecord> ImageRecords { get; set; }
        public DbSet<KeyOptions> KeyOptions { get; set; }
        public DbSet<PromptOptions> PromptOptions { get; set; }
        public DbSet<Sensitive> Sensitives { get; set; }
        public DbSet<Verification> Verifications { get; set; }
        /// <summary>
        /// 订单表
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        public DbSet<Category> Categorys { get; set; }
        public DbSet<Product> Products { get; set; }
        #region 知识库
        public DbSet<KnowledgeItem> knowledgeItems { get; set; }
        #endregion

        #endregion


        public FrameworkDbContext(DbContextOptions<FrameworkDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
