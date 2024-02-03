using Microsoft.EntityFrameworkCore;
using System.Xml;
using Terramours_Gpt_Vector.Entities;

namespace Terramours_Gpt_Vector.Commons
{
    public class EFCoreDbContext : DbContext
    {
        public EFCoreDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<VectorItem> vectorItems { get; set; }
        public DbSet<ApiKey> apiKeys { get; set; }
        public DbSet<IndexItem> indexItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("vector");
            modelBuilder.Entity<VectorItem>(entity =>
            {
                //设置表主键
                entity.HasKey(e => e.VectorId);
                //设置主键自增
                entity.Property(e => e.VectorId)
                       .UseIdentityColumn();
                entity.HasQueryFilter(e => e.IsDeleted == false);
                entity.HasIndex(i => i.Embedding)
                .HasMethod("ivfflat")
                .HasOperators("vector_l2_ops")
                .HasStorageParameter("lists", 100);
                entity.Property(e => e.SparseValues).IsRequired(false).HasColumnType("jsonb");
            });
            modelBuilder.Entity<IndexItem>(entity =>
            {
                //设置表主键
                entity.HasKey(e => e.IndexId);
                //设置主键自增
                entity.Property(e => e.IndexId)
                       .UseIdentityColumn();
                entity.HasQueryFilter(e => e.IsDeleted == false);
                entity.HasIndex(i => i.Key);
            });
            modelBuilder.Entity<ApiKey>(entity =>
            {
                //设置表主键
                entity.HasKey(e => e.KeyId);
                //设置主键自增
                entity.Property(e => e.KeyId)
                       .UseIdentityColumn();
                entity.HasQueryFilter(e => e.IsDeleted == false);
                entity.HasIndex(i => i.ThirdPartId);
            });
        }
    }
}
