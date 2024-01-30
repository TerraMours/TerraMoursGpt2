using Newtonsoft.Json.Linq;

namespace TerraMours.Framework.Infrastructure.Contracts.Commons {
    public class SysSettings {
        /// <summary>
        /// 新增用户默认角色ID
        /// </summary>
        public Initial initial { get; set; }
        public JWT jwt { get; set; }
        public Connection connection { get; set; }
        public Secret secret { get; set; }

        public Email email { get; set; }
        /// <summary>
        /// seq的url
        /// </summary>
        public string SeqUrl { get; set; }
    }


    public class Connection {
        public string DBType { get; set; }
        public string DbConnectionString { get; set; }
        public string RedisHost { get; set; }
        public string RedisInstanceName { get; set; }
    }

    public class JWT {
        /// <summary>
        /// Token 密钥
        /// </summary>
        public string SecretKey { get; set; }
        /// <summary>
        /// 发布者
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 接收者
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 过期时间配置 默认配置天数 系统安全级别不高
        /// </summary>
        public string Expires { get; set; }

    }

    /// <summary>
    /// 加解密配置key
    /// </summary>
    public class Secret {
        /// <summary>
        /// 加密key
        /// </summary>
        public string Encrypt { get; set; }

    }

    /// <summary>
    /// 邮箱配置
    /// </summary>
    public class Email {
        /// <summary>
        /// 邮箱host qq是 smtp.qq.com
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// QQ邮箱端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 是否开启ssl协议
        /// </summary>
        public bool UseSsl { get; set; }
        /// <summary>
        /// 发送的邮箱账号
        /// </summary>
        public string SenderEmail { get; set; }
        /// <summary>
        /// 发送的邮箱名称类似于网名
        /// </summary>
        public string SenderName { get; set; }
        /// <summary>
        /// 邮箱的授权码而不是密码
        /// </summary>
        public string SenderPassword { get; set; }
    }
    /// <summary>
    /// 初始化参数
    /// </summary>
    public class Initial {
        /// <summary>
        /// 默认角色
        /// </summary>
        public long InitialRoleId { get; set; }
        /// <summary>
        /// 初始密码
        /// </summary>
        public string InitialPassWord { get; set; }

    }
}