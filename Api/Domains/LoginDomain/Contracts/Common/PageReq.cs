namespace TerraMours_Gpt.Domains.LoginDomain.Contracts.Common
{
    /// <summary>
    /// 基础分页信息
    /// </summary>
    public class PageReq
    {
        /// <summary>
        /// 查询关键词
        /// </summary>
        public string? QueryString { get; set; }

        /// <summary>
        /// page 从1开始
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// size
        /// </summary>
        public int PageSize { get; set; }
    }
}
