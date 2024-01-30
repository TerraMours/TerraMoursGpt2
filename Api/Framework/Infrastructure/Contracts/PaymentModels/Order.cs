namespace TerraMours_Gpt.Framework.Infrastructure.Contracts.PaymentModels
{
    /// <summary>
    /// 订单表
    /// </summary>
    public class Order
    {
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

        /// <summary>
        /// 订单状态 交易状态：WAIT_BUYER_PAY（交易创建，等待买家付款）、TRADE_CLOSED（未付款交易超时关闭，或支付完成后全额退款）、TRADE_SUCCESS（交易支付成功）、TRADE_FINISHED（交易结束，不可退款）
        /// </summary>
        /*public enum OrderStatus
        {
            //交易创建，等待买家付款
            WAIT_BUYER_PAY,
            //未付款交易超时关闭，或支付完成后全额退款
            TRADE_CLOSED,
            //交易支付成功
            TRADE_SUCCESS,
            //交易结束，不可退款
            TRADE_FINISHED
        }*/

        //无参构造函数 攻 EF Core 调用
        public Order()
        {

        }

        /// <summary>
        /// 新建订单
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <param name="userId"></param>
        /// <param name="orderid"></param>
        /// <param name="isVIP"></param>
        /// <param name="vipLevel"></param>
        public Order(long productId, string name, string description, decimal price, string userId, string orderid, bool? isVIP, int? vipLevel, int? vipTime)
        {
            //初始化用户 ：以下是有用的字段
            this.ProductId = productId;
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.UserId = userId;
            this.IsVIP = isVIP;
            this.VipLevel = vipLevel;
            this.VipTime = vipTime;
            //系统自动生成orderid
            this.OrderId = orderid;
            //默认新建订单是未支付的，后续查询支付宝订单状态再覆盖此状态 取消设计枚举
            this.Status = "WAIT_BUYER_PAY";
            //EntityBase
            CreatedTime = DateTime.Now;
            VipTime = vipTime;
        }

        /// <summary>
        /// 修改订单状态，并存储支付宝返回的支付订单号
        /// </summary>
        /// <param name="tradeNo"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public Order PayOrder(string tradeNo, string status)
        {
            //初始化用户 ：以下是有用的字段
            this.TradeNo = tradeNo;
            this.Status = status;
            //EntityBase
            this.PaidTime = DateTime.Now;
            return this;
        }


    }
}
