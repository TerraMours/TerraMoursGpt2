using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TerraMours_Gpt.Framework.Infrastructure.Contracts.PaymentModels
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //在 PostgreSQL 中，表名以及列名是不区分大小写的。这意味着在数据库中创建表时，无论您是使用大写、小写或混合大小写的名称，最终都会使用相同的名称来访问和操作该表。
            //设置表
            builder.ToTable("Order");
            //设置表主键
            builder.HasKey(e => e.Id);
            //设置主键自增
            builder.Property(e => e.Id)
                   .UseIdentityColumn();

            //0.000014 为gpt3.5一个token的价格 这位单位1
            builder.Property(e => e.Price)
           .HasPrecision(18, 6); // 设置 Precision 和 Scale，例子中 Scale 为8

            // 映射 CreateDate 字段
            builder.Property(e => e.CreatedTime)
                .HasColumnType("timestamp without time zone");

            // 映射 ModifyDate 字段
            builder.Property(e => e.PaidTime)
                .HasColumnType("timestamp without time zone");

            /*builder.Property(e => e.Status)
            .HasConversion<string>(); // 通过HasConversion指定将枚举类型映射为字符串类型的列*/

            /* builder.Property(o => o.Name).IsRequired().HasMaxLength(100);
             builder.Property(o => o.Description).IsRequired().HasMaxLength(500);
             builder.Property(o => o.Price).IsRequired().HasColumnType("decimal(18, 2)");
             builder.Property(o => o.Stock);
             builder.Property(o => o.ImagePath).HasMaxLength(500);
             builder.Property(o => o.UserId).IsRequired();
             builder.Property(o => o.Status).HasMaxLength(50);
             builder.Property(o => o.CreatedTime).IsRequired();
             builder.Property(o => o.PaidTime).HasColumnType("datetime");*/
        }
    }
}
