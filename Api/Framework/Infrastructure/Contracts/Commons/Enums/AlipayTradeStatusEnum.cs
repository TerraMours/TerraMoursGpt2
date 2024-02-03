using System.ComponentModel;

namespace TerraMours_Gpt.Framework.Infrastructure.Contracts.Commons.Enums
{
    /// <summary>
    /// 交易状态：WAIT_BUYER_PAY（交易创建，等待买家付款）、TRADE_CLOSED（未付款交易超时关闭，或支付完成后全额退款）、TRADE_SUCCESS（交易支付成功）、TRADE_FINISHED（交易结束，不可退款）
    /// </summary>
    public enum AlipayTradeStatusEnum
    {
        [Description("WAIT_BUYER_PAY")]
        WAIT_BUYER_PAY,

        [Description("TRADE_CLOSED")]
        TRADE_CLOSED,

        [Description("TRADE_SUCCESS")]
        TRADE_SUCCESS,

        [Description("TRADE_FINISHED")]
        TRADE_FINISHED
    }
}
