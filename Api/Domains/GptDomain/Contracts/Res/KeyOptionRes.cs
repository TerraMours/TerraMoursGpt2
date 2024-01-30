namespace TerraMours_Gpt.Domains.GptDomain.Contracts.Res
{
    public class KeyOptionRes
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long KeyId { get; set; }
        /// <summary>
        /// ApiKey
        /// </summary>
        public string? ApiKey { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpirationTime { get; set; }
        /// <summary>
        /// 使用量
        /// </summary>
        public decimal? Used { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public decimal? UnUsed { get; set; }

        /// <summary>
        /// 总量
        /// </summary>
        public decimal? Total { get; set; }

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
