using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TerraMours.Framework.Infrastructure.Contracts.SystemModels;

namespace TerraMours_Gpt.Framework.Infrastructure.Contracts.GptModels
{
    /// <summary>
    /// 聊天会话表
    /// </summary>
    public class ChatConversation:BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long ConversationId { get; set; }

        /// <summary>
        /// 会话名称
        /// </summary>
        public string? ConversationName { get; set; }
      
        /// <summary>
        /// 用户id
        /// </summary>
        public long? UserId { get; set; }

        public int? KnowledgeId { get; set; }

        public ChatConversation() {
        }

        public ChatConversation(string? conversationName, long? userId, int? knowledgeId) {
            ConversationName = conversationName;
            KnowledgeId = knowledgeId;
            UserId = userId;
            //EntityBase
            Enable = true;
            CreateDate = DateTime.Now;
            CreateID = userId;
        }

        public ChatConversation Delete(long? userId) {
            this.Enable = false;
            //EntityBase
            this.ModifyDate = DateTime.Now;
            this.ModifyID = userId;
            return this;
        }

        public ChatConversation Change(string conversationName, long? userId) {
            this.ConversationName = conversationName;
            //EntityBase
            this.ModifyDate = DateTime.Now;
            this.ModifyID = userId;
            return this;
        }
    }

}
