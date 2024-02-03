using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TerraMours_Gpt.Framework.Infrastructure.Contracts.GptModels
{
    public class ChatRecordConfig : IEntityTypeConfiguration<ChatRecord>
    {
        public void Configure(EntityTypeBuilder<ChatRecord> builder)
        {
            //在 PostgreSQL 中，表名以及列名是不区分大小写的。这意味着在数据库中创建表时，无论您是使用大写、小写或混合大小写的名称，最终都会使用相同的名称来访问和操作该表。
            //设置表
            builder.ToTable("ChatRecord");
            //设置表主键
            builder.HasKey(e => e.ChatRecordId);
            //设置主键自增
            builder.Property(e => e.ChatRecordId)
                   .UseIdentityColumn();

            // 配置角色关系一个菜单对应的对应多个按钮 关系 外键是 MenuId
            //builder.HasOne<ChatConversation>(e => e.ChatConversation)
            //    .WithMany(e => e.ChatRecords)
            //    .HasForeignKey(e => e.ConversationId);

        }
    }
}
