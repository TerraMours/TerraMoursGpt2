namespace TerraMours_Gpt.Domains.LoginDomain.Contracts.Res
{
    /// <summary>
    /// 统计
    /// </summary>
    public class TotalAnalysisRes
    {
        /// <summary>
        /// 展示内容
        /// </summary>
        public string? Key { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public double? Total { get; set; }

        /// <summary>
        /// 上一阶段总数（对比用）
        /// </summary>
        public double? LastTotal { get; set; }
    }
}
