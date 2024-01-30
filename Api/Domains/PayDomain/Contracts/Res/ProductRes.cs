namespace TerraMours_Gpt.Domains.PayDomain.Contracts.Res
{
    public class ProductRes
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 商品折扣 默认不打折
        /// </summary>
        public decimal Discount { get; set; }
        /// <summary>
        /// 商品库存
        /// </summary>
        public int? Stock { get; set; }
        /// <summary>
        /// 商品分类Id
        /// </summary>
        public long CategoryId { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 分类描述
        /// </summary>
        public string CategoryDescription { get; set; }
        /// <summary>
        /// 是否是Vip(包月会员)
        /// </summary>
        public bool? IsVIP { get; set; }
        /// <summary>
        /// 会员等级 （1、2、3、设置青铜会员，白银会员，黄金会员，或者什么普通vip，超级vip都行）
        /// </summary>
        public int? VipLevel { get; set; }
        /// <summary>
        /// vip充值时间 按月算  数字就是月数
        /// </summary>
        public int? VipTime { get; set; }
        /// <summary>
        /// 商品图片路径
        /// </summary>
        public string? ImagePath { get; set; }
    }
}
