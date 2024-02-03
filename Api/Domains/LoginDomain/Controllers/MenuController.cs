using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TerraMours.Domains.LoginDomain.Contracts.Req;
using TerraMours.Domains.LoginDomain.IServices;

namespace TerraMours_Gpt_Api.Domains.LoginDomain.Controllers {
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class MenuController : ControllerBase {
        private readonly ISysMenuService _sysMenuService;

        public MenuController(ISysMenuService sysMenuService) {
            _sysMenuService = sysMenuService;
        }
        /// <summary>
		/// 全部菜单列表 
		/// </summary>
		/// <returns></returns>
		[Authorize]
        [HttpGet]
        public async Task<IResult> GetAllMenuList() {
            var res = await _sysMenuService.GetAllMenuList();
            return Results.Ok(res);
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> DelMenu([FromBody] SysMenuBaseReq req) {
            var res = await _sysMenuService.DelMenu(req);
            return Results.Ok(res);
        }

        /// <summary>
        /// 修改菜单信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> UpdateMenu([FromBody] SysMenuReq req) {
            var res = await _sysMenuService.UpdateMenu(req);
            return Results.Ok(res);
        }
        /// <summary>
        /// 新增菜单（管理员）
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> AddMenu([FromBody] SysMenuReq req) {
            var res = await _sysMenuService.AddMenu(req);
            return Results.Ok(res);
        }
        /// <summary>
        /// 菜单树
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> GetMenuTree([FromBody] MenuToRoleAddReq req) {
            var res = await _sysMenuService.GetMenuTree(req.RoleId);
            return Results.Ok(res);
        }

        /// <summary>
        /// 给角色配置菜单
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> AddMenuToRole([FromBody] MenuToRoleAddReq req) {
            var res = await _sysMenuService.AddMenuToRole(req);
            return Results.Ok(res);
        }

        /// <summary>
        /// 角色下的菜单id（因为前端的选中）
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> GetRoleMenusIds([FromBody] MenuToRoleAddReq req) {
            var res = await _sysMenuService.GetRoleMenusIds(req);
            return Results.Ok(res);
        }
        /// <summary>
        /// 用户路由
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> GetUserRoutes([FromBody] SysMenuBaseReq req) {
            if (req.RoleId == null) {
                req.RoleId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Role));
            }
            var res = await _sysMenuService.GetUserRoutes(req.RoleId);
            return Results.Ok(res);
        }
        /// <summary>
        /// 菜单下拉框
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IResult> GetMenuSelect([FromBody] SysMenuBaseReq req) {
            if (req.RoleId == null) {
                req.RoleId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Role));
            }
            var res = await _sysMenuService.GetMenuSelect(req);
            return Results.Ok(res);
        }
    }
}
