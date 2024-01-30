using Essensoft.Paylink.Alipay;
using Essensoft.Paylink.Alipay.Domain;
using Essensoft.Paylink.Alipay.Request;
using Essensoft.Paylink.Alipay.Response;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json;
using TerraMours.Framework.Infrastructure.EFCore;
using TerraMours_Gpt.Domains.PayDomain.Contracts.Req;
using TerraMours_Gpt.Framework.Infrastructure.Contracts.Commons.Enums;
using ILogger = Serilog.ILogger;

namespace TerraMours_Gpt.Domains.PayDomain.Hubs
{
    /// <summary>
    /// 支付相关的长连接
    /// </summary>
    [EnableCors("MyPolicy")]
    public class PaymentHub : Hub
    {
        private readonly IAlipayClient _client;
        private readonly FrameworkDbContext _dbContext;
        private readonly Serilog.ILogger _logger;

        public PaymentHub(IAlipayClient client,FrameworkDbContext dbContext, ILogger logger)
        {
            _client = client;
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// 即时查询状态
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task QueryPaymentStatus(AlipayTradeQueryReq req)
        {
            var options =await _dbContext.SysSettings.FirstOrDefaultAsync();
            _logger.Information($"即时查询状态，订单号:{req.OutTradeNo}");
            //获取当前连接id
            var connectionId = this.Context.ConnectionId;

            //先查询系统里面是否有此账单
            // todo  支付成功 则修改用户vip信息
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == req.OutTradeNo) ?? throw new Exception("此订单不存在");

            var model = new AlipayTradeQueryModel
            {
                OutTradeNo = req.OutTradeNo,
                //可以不传
                TradeNo = req.TradeNo
            };

            var request = new AlipayTradeQueryRequest();
            request.SetBizModel(model);
            //循环查询3分钟这个账单，超时或者状态位已支付则停止
            using var httpClient = new HttpClient();
            var startTime = DateTime.Now;
            AlipayTradeQueryResponse queryPayRes = new AlipayTradeQueryResponse();
            //是否超时，超时调用支付宝关闭订单接口
            bool isSuccess = false;
            while ((DateTime.Now - startTime).TotalMinutes <= 3)
            {
                queryPayRes = await _client.ExecuteAsync(request, options.Alipay);
                string jsonContent = System.Text.Json.JsonSerializer.Serialize(queryPayRes, new JsonSerializerOptions
                {
                    IgnoreNullValues = true
                });
                if (jsonContent != null)
                {
                    _logger.Information($"【{DateTime.Now}】ali接口查詢結果:{jsonContent}");
                    _logger.Warning($"ali接口查詢結果:{jsonContent}");
                }
                //交易状态：WAIT_BUYER_PAY（交易创建，等待买家付款）、TRADE_CLOSED（未付款交易超时关闭，或支付完成后全额退款）、TRADE_SUCCESS（交易支付成功）、TRADE_FINISHED（交易结束，不可退款）
                if (queryPayRes.Code == "10000" && (queryPayRes.TradeStatus == AlipayTradeStatusEnum.TRADE_SUCCESS.ToString() || queryPayRes.TradeStatus == AlipayTradeStatusEnum.TRADE_CLOSED.ToString()))
                {
                    isSuccess = true;
                    _logger.Information($"【{DateTime.Now}】订单号状态改变:{req.OutTradeNo},交易状态：{queryPayRes.TradeStatus}");
                    _logger.Warning($"【{DateTime.Now}】订单号状态改变:{req.OutTradeNo},交易状态：{queryPayRes.TradeStatus}");
                    break;
                }

                //等待 3 秒后调用 [alipay.trade.query](https://opendocs.alipay.com/open/02ekfh?scene=23)（统一收单线下交易查询接口），通过支付时传入的商户订单号（out_trade_no）查询支付结果（返回参数 TRADE_STATUS）。
                await Task.Delay(3000);
            }
            //再次查询一次，保证延迟的三秒不是刚刚支付的同时不是三分钟的临界点，导致返回的数据与实际的数据不一致
            queryPayRes = await _client.ExecuteAsync(request, options.Alipay);
            if (queryPayRes.Code == "10000" && (queryPayRes.TradeStatus == AlipayTradeStatusEnum.TRADE_SUCCESS.ToString() || queryPayRes.TradeStatus == AlipayTradeStatusEnum.TRADE_CLOSED.ToString()))
            {
                isSuccess = true;
                _logger.Information($"【{DateTime.Now}】订单号状态改变:{req.OutTradeNo},交易状态：{queryPayRes.TradeStatus}");
                _logger.Warning($"【{DateTime.Now}】订单号状态改变:{req.OutTradeNo},交易状态：{queryPayRes.TradeStatus}");
            }

            // 如果超过3分钟仍未支付，发送订单状态信息
            if (!isSuccess)
            {
                order.PayOrder(string.Empty, AlipayTradeStatusEnum.TRADE_CLOSED.ToString());

                //超时调用支付宝关闭订单接口
                var closeModel = new AlipayTradeCloseModel
                {
                    OutTradeNo = req.OutTradeNo,
                    //可以不传
                    TradeNo = req.TradeNo
                };
                var closeReq = new AlipayTradeCloseRequest();
                closeReq.SetBizModel(closeModel);
                _client.ExecuteAsync(closeReq, options.Alipay);
                _dbContext.Orders.Update(order);
                await _dbContext.SaveChangesAsync();
                await Clients.Client(connectionId).SendAsync("QueryPaymentStatus", new { OutTradeNo = req.OutTradeNo, TradeStatus = AlipayTradeStatusEnum.TRADE_CLOSED.ToString() });
            }
            else
            {
                order.PayOrder(queryPayRes.TradeNo, queryPayRes.TradeStatus);
                _dbContext.Orders.Update(order);
                await _dbContext.SaveChangesAsync();
                //如果支付成功，则把user的余额加上新充值的钱
                //2023.08.29 支付成功分2中，如果是会员，则把会员等级以及过期时间写入user表里面
                //1 为月度会员，2为季度会员，3会年度会员
                var user = await _dbContext.SysUsers.FirstOrDefaultAsync(x => x.UserId == long.Parse(order.UserId)) ?? throw new Exception("此用户不存在");

                var vipExpireTime = DateTime.Now;
                if (user.VipExpireTime < vipExpireTime || user.VipExpireTime == null)
                { //过期时间为空或者小于当前时间，则是新充或者续费的vip
                    user.VipExpireTime = vipExpireTime;
                }
                else
                {
                    vipExpireTime = user.VipExpireTime.Value;
                }

                if (order.IsVIP == true)
                {//购买会员

                    user.VipLevel = order.VipLevel;
                    user.VipExpireTime = vipExpireTime.AddMonths(order.VipTime.Value);
                    _dbContext.SysUsers.Update(user);
                    await _dbContext.SaveChangesAsync();

                }
                else
                {//充值余额
                    user.Balance = (user.Balance ?? 0) + order.Price;
                    _dbContext.SysUsers.Update(user);
                    await _dbContext.SaveChangesAsync();
                }
                await Clients.Client(connectionId).SendAsync("QueryPaymentStatus", new { OutTradeNo = req.OutTradeNo, TradeStatus = queryPayRes.TradeStatus });
            }

        }

    }
}
