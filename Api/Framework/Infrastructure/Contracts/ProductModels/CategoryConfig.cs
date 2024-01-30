using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TerraMours_Gpt.Framework.Infrastructure.Contracts.ProductModels
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //在 PostgreSQL 中，表名以及列名是不区分大小写的。这意味着在数据库中创建表时，无论您是使用大写、小写或混合大小写的名称，最终都会使用相同的名称来访问和操作该表。
            //设置表
            builder.ToTable("Category");
            //设置表主键
            builder.HasKey(e => e.Id);
            //设置主键自增
            builder.Property(e => e.Id)
                   .UseIdentityColumn();

            // 映射 CreateDate 字段
            builder.Property(e => e.CreateDate)
                .HasColumnType("timestamp without time zone");

            // 映射 ModifyDate 字段
            builder.Property(e => e.ModifyDate)
                .HasColumnType("timestamp without time zone");
            //在对应的实体的config类文件里面的config方法加上
            builder.HasQueryFilter(e => e.Enable);

            //将 Version 属性设置为每次插入或更新时自增，并且将其设置为乐观并发标识。
            //builder.Property(e => e.Version).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
            //使用pqsql自带的xmin隐式字段为版本控制
            //builder.Property(e => e.Version).IsRowVersion();
            builder.Property(e => e.Version)
                  .HasComputedColumnSql("xmin") // 使用 HasComputedColumnSql 方法指定 xmin 列为计算列
                  .HasColumnType("xid") // 使用 HasColumnType 方法指定 xmin 的数据类型为 xid
                  .ValueGeneratedOnAddOrUpdate()
                  .IsConcurrencyToken(); // 使用 IsConcurrencyToken 方法将 Version 属性标记为并发令牌
        }
    }
}
