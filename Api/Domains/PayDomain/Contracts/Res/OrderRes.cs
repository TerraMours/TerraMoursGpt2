namespace TerraMours_Gpt.Domains.PayDomain.Contracts.Res {
    public class OrderRes {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 订单ID 本系统自己的交易号（唯一）生成格式TM_时间加上Guid
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 支付宝下单之后的回传的交易号，后续也可以作为订单查询的id
        /// </summary>
        public string? TradeNo { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 商品信息描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 商品库存
        /// </summary>
        public int? Stock { get; set; }

        /// <summary>
        /// 商品图片路径
        /// </summary>
        public string? ImagePath { get; set; }

        /// <summary>
        /// 用户ID(邮箱)
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 订单状态  这个状态以支付宝查询接口的状态为准直接覆盖
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? PaidTime { get; set; }

        /// <summary>
        /// 是否是Vip(包月会员)
        /// </summary>
        public bool? IsVIP { get; set; }
        /// <summary>
        /// 会员等级 (1 为月度会员，2为季度会员，3会年度会员这与 价格相关，由于user表已经设计了，下次遇到相似问题设计为enum类)
        /// </summary>
        public int? VipLevel { get; set; }
        /// <summary>
        /// vip充值时间 按月算  数字就是月数
        /// </summary>
        public int? VipTime { get; set; }
    }
}
