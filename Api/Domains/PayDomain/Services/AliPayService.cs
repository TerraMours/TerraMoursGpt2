using AutoMapper;
using Essensoft.Paylink.Alipay;
using Essensoft.Paylink.Alipay.Domain;
using Essensoft.Paylink.Alipay.Request;
using Essensoft.Paylink.Alipay.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TerraMours.Domains.LoginDomain.Contracts.Common;
using TerraMours.Domains.LoginDomain.IServices;
using TerraMours.Framework.Infrastructure.EFCore;
using TerraMours_Gpt.Domains.GptDomain.Contracts.Res;
using TerraMours_Gpt.Domains.LoginDomain.Contracts.Common;
using TerraMours_Gpt.Domains.PayDomain.Contracts.Req;
using TerraMours_Gpt.Domains.PayDomain.Contracts.Res;
using TerraMours_Gpt.Domains.PayDomain.IServices;
using TerraMours_Gpt.Framework.Infrastructure.Contracts.PaymentModels;
using ILogger = Serilog.ILogger;

namespace TerraMours_Gpt.Domains.PayDomain.Services
{
    public class AliPayService : IPayService
    {
        private readonly IAlipayClient _client;
        private readonly IMapper _mapper;
        private readonly FrameworkDbContext _dbContext;
        private readonly Serilog.ILogger _logger;
        private readonly ISysUserService _sysUserService;

        public AliPayService(IAlipayClient client, IMapper mapper, FrameworkDbContext dbContext, ILogger logger, ISysUserService sysUserService) {
            _client = client;
            _mapper = mapper;
            _dbContext = dbContext;
            _logger = logger;
            _sysUserService = sysUserService;
        }

        /// <summary>
        ///  当面付-扫码支付
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<ApiResponse<AlipayTradePrecreateResponse>> PreCreate(AlipayTradePreCreateReq req)
        {
            var options =await _dbContext.SysSettings.FirstOrDefaultAsync();
            //生成系统内唯一交易号
            var tradeNo = $"TerraMours-{Guid.NewGuid()}";
            // 支付宝规定：订单总金额，单位为元，精确到小数点后两位，取值范围为 [0.01,100000000]，金额不能为 0。
            //但是我们系统是decimal 保留六位小数，这里由于充值我们是整数，所以我们只需要传给支付宝的钱处理下，保留两位小数即可

            var model = new AlipayTradePrecreateModel
            {
                //系统内部的唯一交易号  $"TerraMours-{Guid.NewGuid()}"
                //OutTradeNo = req.OutTradeNo,
                OutTradeNo = tradeNo,
                //类似于邮件主题
                Subject = req.Name,
                //金额总数: 将商品价格转换为保留 2 位小数的 decimal 再转换为字符串
                TotalAmount = Math.Round(req.Price, 2).ToString(),
                //TotalAmount = req.Price.ToString(),
                //描述
                Body = req.Description
            };
            var request = new AlipayTradePrecreateRequest();
            request.SetBizModel(model);
            request.SetNotifyUrl(req.NotifyUrl);

            //此时应该先在自己的order表里面创建一个待支付的订单

            var order = new Order(req.ProductId, req.Name, req.Description, req.Price, req.UserId, tradeNo, req.IsVIP, req.VipLevel, req.VipTime);
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            //测试本地先生成一个支付二维码图片，后面再加vue项目
            var response = await _client.ExecuteAsync(request, options.Alipay);

            if (!response.IsError)
            {
                return ApiResponse<AlipayTradePrecreateResponse>.Success(response);
            }

            return ApiResponse<AlipayTradePrecreateResponse>.Fail("支付宝当面付二维码生成失败");
        }

        /// <summary>
        /// 交易查询
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>

        public async Task<ApiResponse<AlipayTradeQueryResponse>> Query(AlipayTradeQueryReq req)
        {
            var options =await _dbContext.SysSettings.FirstOrDefaultAsync();
            var model = new AlipayTradeQueryModel
            {
                OutTradeNo = req.OutTradeNo,
                //可以不传
                TradeNo = req.TradeNo
            };

            var request = new AlipayTradeQueryRequest();
            request.SetBizModel(model);

            var res = await _client.ExecuteAsync(request, options.Alipay);
            return ApiResponse<AlipayTradeQueryResponse>.Success(res);
        }
        /// <summary>
        /// 支付宝回调
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task Callback(AlipayPayCallbackReq queryPayRes)
        {
            //seq 健康检查info的日志太乱，打印测试用
            _logger.Warning($"支付宝回调，订单号:{queryPayRes.OutTradeNo},交易状态：{queryPayRes.TradeStatus}");
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == queryPayRes.OutTradeNo) ?? throw new Exception("此订单不存在");
            order.PayOrder(queryPayRes.TradeNo, queryPayRes.TradeStatus);
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
            //如果支付成功，则把user的余额加上新充值的钱
            var user = await _dbContext.SysUsers.FirstOrDefaultAsync(x => x.UserEmail == order.UserId) ?? throw new Exception("此用户不存在");
            user.Balance = user.Balance + order.Price;
            _dbContext.SysUsers.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 订单管理
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResponse<PagedRes<OrderRes>>> OrderList(PageReq page, long roleId)
        {
            var currentRole = _dbContext.SysRoles.FirstOrDefault(m => m.RoleId == roleId);
            if (!(currentRole.IsAdmin != null && currentRole.IsAdmin == true)) {
                return ApiResponse<PagedRes<OrderRes>>.Fail("没有调用权限（超级管理员）");
            }
            var query = _dbContext.Orders.AsNoTracking().Where(m => string.IsNullOrEmpty(page.QueryString) || m.OrderId.Contains(page.QueryString) || m.TradeNo.Contains(page.QueryString));
            var total = await query.CountAsync();
            var item = await query.OrderByDescending(m=>m.CreatedTime).Skip((page.PageIndex - 1) * page.PageSize).Take(page.PageSize).ToListAsync();
            var res = _mapper.Map<IEnumerable<OrderRes>>(item);
            //获取用户名称缓存
            var sysUser = await _sysUserService.GetUserNameList();
            foreach (var i in res) {
                i.UserName = sysUser.FirstOrDefault(m => m.Key ==long.Parse(i.UserId)).Value;
            }
            return ApiResponse<PagedRes<OrderRes>>.Success(new PagedRes<OrderRes>(res, total, page.PageIndex, page.PageSize));
        }
    }
}
