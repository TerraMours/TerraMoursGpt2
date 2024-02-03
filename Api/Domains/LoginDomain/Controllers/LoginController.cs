using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TerraMours.Domains.LoginDomain.Contracts.Req;
using TerraMours.Domains.LoginDomain.IServices;
using TerraMours.Framework.Infrastructure.Redis;

namespace TerraMours_Gpt_Api.Domains.LoginDomain.Controllers {
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase {
        private readonly ISysUserService _sysUserService;

        public LoginController(ISysUserService sysUserService) {
            _sysUserService = sysUserService;
        }
        /// <summary>
        /// 用户登录   
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IResult> Login(IValidator<SysLoginUserReq> validator, [FromBody] SysLoginUserReq userReq) {
            var validationResult = await validator.ValidateAsync(userReq);
            if (!validationResult.IsValid) {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var res = await _sysUserService.Login(userReq);
            return Results.Ok(res);
        }

        /// <summary>
        /// 注册成功，此方法注册的用户只是普通用户，不能访问后台管理页面，或者能登录，但是没有菜单显示即可
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IResult> Register(IValidator<SysUserReq> validator, [FromBody] SysUserReq userReq) {
            //这里可以用来验证返回，但是体验不好，后续优化 现在先放着，不做处理，注入方法貌似与controller不太一样，正常注入不生效
            //var result = await validator.ValidateAsync(userReq);
            var validationResult = await validator.ValidateAsync(userReq);
            if (!validationResult.IsValid) {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var res = await _sysUserService.Register(userReq);
            return Results.Ok(res);
        }

        /// <summary>
        /// 登出  //目前做法直接删除user的Token
        /// </summary>
        /// <param name="validator"></param>
        /// <param name="userReq"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> Logout(IValidator<SysLoginUserReq> validator, SysLoginUserReq userReq) {
            var validationResult = await validator.ValidateAsync(userReq);
            if (!validationResult.IsValid) {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }
            var res = await _sysUserService.Logout(userReq);
            return Results.Ok(res);
        }
        [HttpPost]
        public async Task<IResult> ChangePassword(IValidator<SysUserReq> validator, [FromBody] SysUserReq userReq) {
            var validationResult = await validator.ValidateAsync(userReq);
            if (!validationResult.IsValid) {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }
            var res = await _sysUserService.ChangePassword(userReq);
            return Results.Ok(res);
        }
    }
}
