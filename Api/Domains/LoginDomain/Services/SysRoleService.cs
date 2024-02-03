using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;
using System.Collections.Generic;
using TerraMours.Domains.LoginDomain.Contracts.Common;
using TerraMours.Domains.LoginDomain.Contracts.Req;
using TerraMours.Domains.LoginDomain.Contracts.Res;
using TerraMours.Domains.LoginDomain.IServices;
using TerraMours.Framework.Infrastructure.Contracts.SystemModels;
using TerraMours.Framework.Infrastructure.EFCore;

namespace TerraMours.Domains.LoginDomain.Services
{
    /// <summary>
    /// 角色管理api实现类
    /// </summary>
    public class SysRoleService : ISysRoleService
    {
        private readonly FrameworkDbContext _dbContext;
        private readonly IMapper _mapper;
        public SysRoleService(FrameworkDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<ApiResponse<bool>> AddRole(SysRoleBaseReq req)
        {
            var role = new SysRole(req.RoleName,req.IsAdmin,req.IsNewUser);
             _dbContext.SysRoles.Add(role);
            await _dbContext.SaveChangesAsync();
            return ApiResponse<bool>.Success(true);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<ApiResponse<bool>> DelRole(SysRoleBaseReq req)
        {
            var role=await _dbContext.SysRoles.FirstOrDefaultAsync(m=>m.RoleId==req.RoleId);
            role?.Delete();
            await _dbContext.SaveChangesAsync();
            return ApiResponse<bool>.Success(true);
        }
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse<List<SysRoleRes>>> GetAllRoleList()
        {
            var roleList=await _dbContext.SysRoles.Where(m=>m.Enable == true).ToListAsync();
            List < SysRoleRes > sysRoleRes=_mapper.Map<List<SysRoleRes>>(roleList);
            return ApiResponse<List<SysRoleRes>>.Success(sysRoleRes);
        }
        /// <summary>
        /// 角色下拉框
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResponse<List<KeyValueRes>>> GetRoleSelect()
        {
            var selectValue = await _dbContext.SysRoles
            .Where(m => m.Enable==true)
           .Select(m => new KeyValueRes(m.RoleId, m.RoleName)).ToListAsync();
            return ApiResponse<List<KeyValueRes>>.Success(selectValue);
        }

        /// <summary>
        /// 更新（目前只更新名称）
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async  Task<ApiResponse<bool>> UpdateRole(SysRoleBaseReq req)
        {
            var role = await _dbContext.SysRoles.FirstOrDefaultAsync(m => m.RoleId == req.RoleId);
            if(role == null)
            {
                return ApiResponse<bool>.Fail("用户不存在");
            }
            role.ChangeName(req.RoleName);
            role.IsAdmin = req.IsAdmin;
            role.IsNewUser = req.IsNewUser;
            _dbContext.SysRoles.Update(role);
            _dbContext.SaveChanges();
            return ApiResponse<bool>.Success(true);
        }
    }
}
