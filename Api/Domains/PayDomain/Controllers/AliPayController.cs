using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TerraMours_Gpt.Domains.LoginDomain.Contracts.Common;
using TerraMours_Gpt.Domains.PayDomain.Contracts.Req;
using TerraMours_Gpt.Domains.PayDomain.IServices;

namespace TerraMours_Gpt_Api.Domains.PayDomain.Controllers {
    /// <summary>
    /// 支付宝支付
    /// </summary>
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class AliPayController : ControllerBase {
        private readonly IPayService _payService;

        public AliPayController(IPayService payService) {
            _payService = payService;
        }
        /// <summary>
        /// 当面付-扫码支付
        /// </summary>
        [HttpPost]
        public async Task<IResult> PreCreate(AlipayTradePreCreateReq viewModel) {
            if (viewModel.UserId == null) {
                viewModel.UserId = HttpContext.User.FindFirstValue(ClaimTypes.UserData);
            }
            var res = await _payService.PreCreate(viewModel);
            return Results.Ok(res);
        }

        /// <summary>
        /// 支付宝交易查询
        /// </summary>
        [HttpPost]
        public async Task<IResult> Query(AlipayTradeQueryReq viewMode) {
            var res = await _payService.Query(viewMode);
            return Results.Ok(res);
        }
        /// <summary>
        /// 支付宝回调接口
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task Callback([FromBody] AlipayPayCallbackReq req) {
            _payService.Callback(req);
        }

        /// <summary>
        /// 订单管理
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> OrderList(PageReq page) {
            var roleId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Role));

            var res = await _payService.OrderList(page, roleId);
            return Results.Ok(res);
        }
    }
}
