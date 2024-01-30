namespace TerraMours_Gpt.Domains.GptDomain.Contracts.Res
{
    /// <summary>
    /// 系统提示词
    /// </summary>
    public class PromptOptionRes
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long PromptId { get; set; }

        /// <summary>
        /// 扮演
        /// </summary>
        public string? Act { get; set; }

        /// <summary>
        /// 提示词
        /// </summary>
        public string? Prompt { get; set; }

        /// <summary>
        /// 使用次数
        /// </summary>
        public int? UsedCount { get; set; }

        /// <summary>
        /// 是否可用 重要
        /// </summary>
        public bool Enable { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate { get; set; }
    }
}
