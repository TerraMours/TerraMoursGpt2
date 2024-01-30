using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TerraMours.Domains.LoginDomain.Contracts.Req;
using TerraMours.Domains.LoginDomain.IServices;

namespace TerraMours_Gpt_Api.Domains.LoginDomain.Controllers {
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class EmailController : ControllerBase {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService) {
            _emailService = emailService;
        }
        /// <summary>
        /// 邮件发验证码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IResult> CreateCheckCode([FromBody] EmailReq req) {
            var res = await _emailService.CreateCheckCode(req);
            return Results.Ok(res);
        }
    }
}
