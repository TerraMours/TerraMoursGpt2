using System.Text.Json.Serialization;

namespace TerraMours_Gpt.Domains.GptDomain.Contracts.Req {
    /// <summary>
    /// 模型调用基础参数类
    /// </summary>
    public class BaseModelReq {
        /// <summary>
        /// 模型
        /// </summary>
        public string? Model { get; set; }
        [JsonIgnore]
        public string? Key { get; set; }

        /// <summary>
        /// 代理地址
        /// </summary>
        [JsonIgnore]
        public string? BaseUrl { get; set; }

        /// <summary>
        /// 模型类型：OpenAi = 1, Baidu =2,Ali=3,SD=4, MJ=5
        /// </summary>
        [JsonIgnore]
        public int? BaseType { get; set; }

        [JsonIgnore]
        public string? IP { get; set; }
        /// <summary>
        /// 用户id 自增
        /// </summary>
        [JsonIgnore]
        public long UserId { get; set; }
        /// <summary>
        /// 角色id
        /// </summary>
        [JsonIgnore]
        public long? RoleId { get; set; }
    }
}
