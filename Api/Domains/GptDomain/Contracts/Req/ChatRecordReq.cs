using System.Text.Json.Serialization;
using TerraMours_Gpt.Domains.LoginDomain.Contracts.Common;

namespace TerraMours_Gpt.Domains.GptDomain.Contracts.Req {
    /// <summary>
    /// 聊天记录查询
    /// </summary>
    public class ChatRecordReq:PageReq {
        /// <summary>
        /// 会话id
        /// </summary>
        public long? ConversationId { get; set; }
        /// <summary>
        /// 用户id 自增
        /// </summary>
        [JsonIgnore]
        public long UserId { get; set; }
    }
}
