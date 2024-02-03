namespace TerraMours_Gpt.Domains.PayDomain.Contracts.Res
{
    public class CategoryRes
    {
        /// <summary>
        /// 分类ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 分类描述
        /// </summary>
        public string Description { get; set; }
    }
}
