using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TerraMours_Gpt_Api.Framework.Infrastructure.Contracts.GptModels
{
    public class KnowledgeItemConfig: IEntityTypeConfiguration<KnowledgeItem>
    {
        
public void Configure(EntityTypeBuilder<KnowledgeItem> builder)
        {
            builder.ToTable("KnowledgeItem");
            //设置表主键
            builder.HasKey(e => e.KnowledgeId);
            //设置主键自增
            builder.Property(e => e.KnowledgeId)
                   .UseIdentityColumn();
            builder.HasQueryFilter(e => e.Enable);
            builder.HasIndex(i => i.ApiKey);
        }
    }
}
