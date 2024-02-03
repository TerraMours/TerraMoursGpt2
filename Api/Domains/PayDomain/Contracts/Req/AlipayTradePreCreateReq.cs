using System.ComponentModel.DataAnnotations;

namespace TerraMours_Gpt.Domains.PayDomain.Contracts.Req
{
    public class AlipayTradePreCreateReq
    {
        /*[Required]
        [Display(Name = "out_trade_no")]
        public string OutTradeNo { get; set; }

        [Required]
        [Display(Name = "subject")]
        public string Subject { get; set; }

        [Display(Name = "body")]
        public string Body { get; set; }

        [Required]
        [Display(Name = "total_amount")]
        public string TotalAmount { get; set; }*/

        [Display(Name = "notify_url")]
        public string? NotifyUrl { get; set; }

        /// <summary>
        /// 商品id
        /// </summary>
        public long ProductId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 商品折扣 默认不打折
        /// </summary>
        public decimal Discount { get; set; }
        /// <summary>
        /// 商品分类Id
        /// </summary>
        public long CategoryId { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string? UserId { get; set; }
        /// <summary>
        /// 是否是Vip(包月会员)
        /// </summary>
        public bool? IsVIP { get; set; }
        /// <summary>
        /// 会员等级 
        /// </summary>
        public int? VipLevel { get; set; }
        /// <summary>
        /// vip充值时间 按月算  数字就是月数
        /// </summary>
        public int? VipTime { get; set; }

    }
}
