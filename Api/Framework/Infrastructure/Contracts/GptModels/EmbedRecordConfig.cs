using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TerraMours_Gpt_Api.Framework.Infrastructure.Contracts.GptModels
{
    public class EmbedRecordConfig : IEntityTypeConfiguration<EmbedRecord>
    {
        public void Configure(EntityTypeBuilder<EmbedRecord> builder)
        {
            builder.ToTable("EmbedRecord");
            //设置表主键
            builder.HasKey(e => e.EmbedRecordId);
            //设置主键自增
            builder.Property(e => e.EmbedRecordId)
                   .UseIdentityColumn();
            builder.HasIndex(i => i.ApiKey);
        }
    }
}
