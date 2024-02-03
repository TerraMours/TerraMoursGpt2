using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TerraMours.Domains.LoginDomain.Contracts.Req;
using TerraMours.Domains.LoginDomain.IServices;

namespace TerraMours_Gpt_Api.Domains.LoginDomain.Controllers {
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase {
        private readonly ISysRoleService _sysRoleService;

        public RoleController(ISysRoleService sysRoleService) {
            _sysRoleService = sysRoleService;
        }
        /// <summary>
		/// 全部角色列表 todo：jwt添加权限
		/// </summary>
		/// <returns></returns>
		[Authorize]
        [HttpGet]
        public async Task<IResult> GetAllRoleList() {
            var res = await _sysRoleService.GetAllRoleList();
            return Results.Ok(res);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> DelRole([FromBody] SysRoleBaseReq req) {
            var res = await _sysRoleService.DelRole(req);
            return Results.Ok(res);
        }

        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> UpdateRole([FromBody] SysRoleBaseReq req) {
            var res = await _sysRoleService.UpdateRole(req);
            return Results.Ok(res);
        }
        /// <summary>
        /// 新增角色（管理员）
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> AddRole([FromBody] SysRoleBaseReq req) {
            var res = await _sysRoleService.AddRole(req);
            return Results.Ok(res);
        }
        /// <summary>
        /// 菜单下拉框
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IResult> GetRoleSelect() {
            var res = await _sysRoleService.GetRoleSelect();
            return Results.Ok(res);
        }
    }
}
