using TerraMours.Framework.Infrastructure.Contracts.SystemModels;

namespace TerraMours_Gpt.Framework.Infrastructure.Contracts.ProductModels
{
    /// <summary>
    /// 商品表
    /// </summary>
    public class Product : BaseEntity
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
        public decimal? Discount { get; set; }
        /// <summary>
        /// 商品图片路径
        /// </summary>
        public string? ImagePath { get; set; }
        /// <summary>
        /// 商品库存
        /// </summary>
        public int? Stock { get; set; }
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
        /// 商品分类Id
        /// </summary>
        public long CategoryId { get; set; }
        /// <summary>
        /// 商品分类
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// 初始化商品实体类
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <param name="categoryId"></param>
        /// <param name="stock"></param>
        /// <param name="isVIP"></param>
        /// <param name="vipLevel"></param>
        /// <param name="vipTime"></param>
        public Product(string name, string description, decimal price, long categoryId, int? stock, bool? isVIP, int? vipLevel, int? vipTime, string? imagePath)
        {
            //初始化用户 ：以下是有用的字段
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.CategoryId = categoryId;
            this.IsVIP = isVIP;
            this.VipLevel = vipLevel;
            this.VipTime = vipTime;
            //EntityBase
            Enable = true;
            CreateDate = DateTime.Now;
            this.Stock = stock;
            this.ImagePath = imagePath;
        }

        /// <summary>
        /// 修改商品实体类信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <param name="categoryId"></param>
        /// <param name="isVIP"></param>
        /// <param name="vipLevel"></param>
        /// <param name="vipTime"></param>
        /// <returns></returns>
        public Product UpdateProduct(string name, string description, decimal price, long categoryId, bool? isVIP, int? vipLevel, int? vipTime,string? imagePath)
        {
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.IsVIP = isVIP;
            this.VipLevel = vipLevel;
            this.VipTime = vipTime;
            this.CategoryId = categoryId;
            this.ImagePath = imagePath;
            this.ModifyDate = DateTime.Now;
            return this;
        }

        /// <summary>
        /// 下架分类 或者删除分类
        /// </summary>
        /// <returns></returns>
        public Product DeleteProduct()
        {
            this.Enable = false;
            this.ModifyDate = DateTime.Now;
            return this;
        }

    }
}
