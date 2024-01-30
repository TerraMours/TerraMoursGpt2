using System.ComponentModel.DataAnnotations.Schema;
using TerraMours.Framework.Infrastructure.Contracts.SystemModels;

namespace TerraMours_Gpt_Api.Framework.Infrastructure.Contracts.GptModels
{
    public class EmbedRecord: BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long EmbedRecordId { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int? EmbedType { get; set; }
        /// <summary>
        /// ApiKey
        /// </summary>
        public string? ApiKey { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        [Column("Result", TypeName = "jsonb")]
        public string? ResultJson { get; set; }


        /// <summary>
        /// 输入信息
        /// </summary>
        [Column("Input", TypeName = "jsonb")]
        public string? InputJson { get; set; }

        /// <summary>
        /// 提问词Tokens
        /// </summary>
        public int? PromptTokens { get; set; }
        /// <summary>
        /// 回答Tokens
        /// </summary>

        public int? CompletionTokens { get; set; }
        /// <summary>
        /// 总Tokens
        /// </summary>

        public int? TotalTokens { get; set; }
    }
}
