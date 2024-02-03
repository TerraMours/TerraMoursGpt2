namespace TerraMours.Framework.Infrastructure.Contracts.SystemModels
{
    /// <summary>
    /// 基础表属性 创建时间，创建人，修改人等等
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 数据版本号 乐观并发控制
        /// </summary>
        public uint Version { get; set; }
        /// <summary>
        /// 是否可用 重要
        /// </summary>
        public bool Enable { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建人id
        /// </summary>
        public long? CreateID { get; set; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string? CreatorName { get; set; }
        /// <summary>
        /// 修改人id
        /// </summary>
        public long? ModifyID { get; set; }
        /// <summary>
        /// 修改人名称
        /// </summary>
        public string? ModifierName { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
        /// <summary>
        /// 排序号  猜测是会根据那个部门的id来排序 职位等级等 目前用不到
        /// </summary>
        public int? OrderNo { get; set; }

        public void Delete(long userId)
        {
            this.ModifyDate = DateTime.Now;
            this.Enable = false;
            this.ModifyID = userId;
        }

    }
}
