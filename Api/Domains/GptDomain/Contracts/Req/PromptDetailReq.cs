using System.Text.Json.Serialization;

namespace TerraMours_Gpt.Domains.GptDomain.Contracts.Req
{
    /// <summary>
    /// 提示词
    /// </summary>
    public class PromptDetailReq: PromptOptionReq
    {
        /// <summary>
        /// 主键（新增时候不传）
        /// </summary>
        public long? PromptId { get; set; }

        /// <summary>
        /// 使用次数
        /// </summary>
        public int? UsedCount { get; set; }

        /// <summary>
        /// 用户id 自增
        /// </summary>
        [JsonIgnore]
        public long UserId { get; set; }
    }
}
