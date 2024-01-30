using Essensoft.Paylink.Alipay;
using k8s.KubeConfigModels;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using TerraMours.Framework.Infrastructure.Contracts.Commons;
using TerraMours.Framework.Infrastructure.Contracts.SystemModels;

namespace TerraMours_Gpt.Framework.Infrastructure.Contracts.SystemModels
{
    /// <summary>
    /// 系统设置
    /// </summary>
    public class SysSettingsEntity:BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public long SysSettingsId { get; set; }
        /// <summary>
        /// 新建用户默认信息
        /// </summary>
        public Initial? Initial { get; set; }

        /// <summary>
        /// 邮箱配置
        /// </summary>
        public Email? Email { get; set; }

        public AlipayOptions? Alipay { get; set; }

        public SysSettingsEntity()
        {
        }

        public SysSettingsEntity( Initial? initial, Email? email, AlipayOptions? alipay)
        {
            Initial = initial;
            Email = email;
            Alipay = alipay;
            //EntityBase
            Version = 1;
            Enable = true;
            CreateDate = DateTime.Now;
        }

        public SysSettingsEntity Change(long? userId)
        {
            //EntityBase
            this.ModifyDate = DateTime.Now;
            this.ModifyID = userId;
            return this;
        }

        public SysSettingsEntity ChangeEmail(Email? email,long? userId)
        {
            //EntityBase
            this.Email= email;
            this.ModifyDate = DateTime.Now;
            this.ModifyID = userId;
            return this;
        }

        public SysSettingsEntity ChangeAlipayOptions(AlipayOptions? alipay, long? userId) {
            //EntityBase
            this.Alipay = alipay;
            this.ModifyDate = DateTime.Now;
            this.ModifyID = userId;
            return this;
        }
    }
}
