using System.ComponentModel.DataAnnotations;

namespace TerraMours_Gpt.Domains.PayDomain.Contracts.Req
{
    /// <summary>
    /// 交易查询
    /// </summary>
    public class AlipayTradeQueryReq
    {
        [Display(Name = "out_trade_no")]
        public string OutTradeNo { get; set; }

        [Display(Name = "trade_no")]
        public string TradeNo { get; set; }
    }
}
