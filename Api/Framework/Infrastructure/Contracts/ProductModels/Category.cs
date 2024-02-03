using TerraMours.Framework.Infrastructure.Contracts.SystemModels;

namespace TerraMours_Gpt.Framework.Infrastructure.Contracts.ProductModels
{
    /// <summary>
    /// 商品分类
    /// </summary>
    public class Category : BaseEntity
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
        public List<Product> Products { get; set; }

        /// <summary>
        /// 初始化商品分类实体类
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public Category(string name, string description)
        {
            //初始化用户 ：以下是有用的字段
            this.Name = name;
            this.Description = description;
            //EntityBase
            Enable = true;
            CreateDate = DateTime.Now;
        }

        /// <summary>
        /// 修改商品分类实体类信息
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Description"></param>
        public Category UpdateCategory(string name, string description)
        {
            this.Name = name;
            this.Description = description;
            this.ModifyDate = DateTime.Now;
            return this;
        }

        /// <summary>
        /// 下架分类 或者删除分类
        /// </summary>
        /// <returns></returns>
        public Category DeleteCategory()
        {
            this.Enable = false;
            this.ModifyDate = DateTime.Now;
            return this;
        }


    }

}
