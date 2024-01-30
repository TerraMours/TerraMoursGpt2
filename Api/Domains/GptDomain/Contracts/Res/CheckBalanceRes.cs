namespace TerraMours_Gpt.Domains.GptDomain.Contracts.Res
{
    public class CheckBalanceRes
    {
        public CheckBalanceRes(string? apiKey, DateTime? expirationTime, decimal? used, decimal? unUsed, decimal? total)
        {
            ApiKey = apiKey;
            ExpirationTime = expirationTime;
            Used = used;
            UnUsed = unUsed;
            Total = total;
        }

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
    }
}
