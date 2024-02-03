using Microsoft.EntityFrameworkCore;
using TerraMours.Domains.LoginDomain.Contracts.Res;
using TerraMours.Framework.Infrastructure.EFCore;
using TerraMours.Framework.Infrastructure.Redis;

namespace TerraMours.Framework.Infrastructure.Contracts.SystemModels
{
    /// <summary>
    /// 系统用户表
    /// </summary>
    public class SysUser : BaseEntity
    {
        /// <summary>
        /// 用户id 自增
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 用户名 类似网名 不可做登录使用 登录使用手机或者邮箱
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 邮箱 可以登录使用
        /// </summary>
        public string? UserEmail { get; set; }
        /// <summary>
        /// 用户手机号 国内需要验证11号位数 别的国家需要单独到时候加逻辑
        /// </summary>
        public string? UserPhoneNum { get; set; }
        /// <summary>
        /// 短号或者座机 只做记录不能登录
        /// </summary>
        public string? ShortPhone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// 密码（是否加密）UserTrueName
        /// </summary>
        public string UserPassword { get; set; }
        /// <summary>
        /// 是否手机用户 true是手机注册 false 是邮箱注册 支持手机或者邮箱登录
        /// </summary>
        public bool IsRegregisterPhone { get; set; }
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string? UserTrueName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string? Gender { get; set; }
        /// <summary>
        /// 头像url地址 
        /// </summary>
        public string? HeadImageUrl { get; set; }
        /// <summary>
        /// 部门id 后续扩展的
        /// </summary>
        public string? DeptId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string? DeptName { get; set; }
        /// <summary>
        /// 角色id
        /// </summary>
        public long? RoleId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string? RoleName { get; set; }
        /// <summary>
        /// 登陆token
        /// </summary>
        public string? Token { get; set; }
        /// <summary>
        /// 登录失败次数，超过某个值则锁定多少分钟不能登录，防止恶意登录
        /// </summary>
        public int? LoginFailCount { get; set; }
        /// <summary>
        /// 是否能登陆 账号被锁
        /// </summary>
        public bool EnableLogin { get; set; }
        /// <summary>
        /// 是否是超级管理员，默认false
        /// </summary>
        public bool IsSuperAdmin { get; set; }
        /// <summary>
        /// 过期时间  记录当时最大失败时候的时间，然后加上十五分钟，如果登陆时候的时间大于这个失效时间即可登陆。然后下次账号再被锁的时候，更新对应的过期时间即可
        /// </summary>
        public DateTime? ExpireTime { get; set; }

        #region 扩展字段
        /// <summary>
        /// 会员等级
        /// </summary>
        public int? VipLevel { get; set; }

        /// <summary>
        /// 会员过期时间
        /// </summary>
        public DateTime? VipExpireTime { get; set; }

        /// <summary>
        /// 图像剩余次数
        /// </summary>
        public int? ImageCount { get; set; }

        /// <summary>
        /// token余额价格
        /// </summary>
        public decimal? Balance { get; set; }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userEmail">邮箱账号</param>
        /// <param name="userPassword">加密后的密码</param>
        public SysUser(string userEmail, string userPassword)
        {
            //初始化用户 ：以下是有用的字段
            UserEmail = userEmail;
            //存的加密过后的密码
            UserPassword = userPassword;
            //默认用户昵称与邮箱一致
            UserName = userEmail;
            EnableLogin = true;
            LoginFailCount = 0;
            IsRegregisterPhone = false;
            IsSuperAdmin = false;
            //EntityBase
            Enable = true;
            CreateDate = DateTime.Now;
        }

        public SysUser()
        {
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="userPassword"></param>
        public SysUser ChangePassword(string userPassword)
        {
            this.UserPassword = userPassword;
            this.ModifyDate=DateTime.Now;
            return this;
        }

        /// <summary>
        /// 锁账号
        /// </summary>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public SysUser LockLogin()
        {
            this.EnableLogin = false;
            return this;
        }

        /// <summary>
        /// 解锁账号
        /// </summary>
        /// <returns></returns>
        public SysUser UnLockLogin()
        {
            this.EnableLogin = true;
            return this;
        }

        /// <summary>
        /// 删除用户  （逻辑删）
        /// </summary>
        /// <returns></returns>
        public SysUser Delete()
        {
            this.Enable = false;
            return this;
        }

        /// <summary>
        /// 登出，删除token
        /// </summary>
        /// <returns></returns>
        public SysUser Logout()
        {
            this.Token = "";
            return this;
        }

        public async Task<List<KeyValueRes>> GetUserNameList(IDistributedCacheHelper _helper, FrameworkDbContext _dbContext)
        {
            var userList = await _helper.GetOrCreateAsync("GetUserNameList", async options => { return await _dbContext.SysUsers.Select(m => new KeyValueRes(m.UserId, m.UserName)).ToListAsync(); });
            return userList;
        }

    }
}
