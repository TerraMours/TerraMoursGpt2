namespace TerraMours_Gpt.Domains.LoginDomain.Contracts.Res {
    public class AnalysisListRes {
        /// <summary>
        /// 展示内容
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 提问数量
        /// </summary>
        public int AskCount { get; set; }
        /// <summary>
        /// 使用人数
        /// </summary>
        public int UserCount { get; set; }
        /// <summary>
        /// 生成图片数量
        /// </summary>
        public int ImageCount { get; set; }
    }
}
