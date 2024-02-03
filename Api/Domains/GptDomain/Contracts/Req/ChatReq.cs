
using FluentValidation;
using System.Text.Json.Serialization;

namespace TerraMours_Gpt.Domains.GptDomain.Contracts.Req {
    /// <summary>
    /// AI聊天参数
    /// </summary>
    public class ChatReq : ChatCompletionBaseReq {
        /// <summary>
        /// 提问词
        /// </summary>
        public string Prompt { get; set; }

        /// <summary>
        /// 会话id  新建会话传-1
        /// </summary>
        public long? ConversationId { get; set; }

        /// <summary>
        /// 系统提示词
        /// </summary>
        public string? SystemMessage { get; set; }

        /// <summary>
        /// 上下文数量
        /// </summary>
        public int? ContextCount { get; set; }
        /// <summary>
        /// 模型类型
        /// </summary>
        public int? ModelType { get; set; }   
        
        /// <summary>
        /// 图文分析文件url（只适用于gpt-4-vision-preview）
        /// </summary>
        public string? FileUrl { get; set; }
    }
    /// <summary>
    /// 文本嵌入
    /// </summary>
    public class EmbedReq:BaseModelReq {
        [JsonIgnore]
        public List<string>? InputAsList { get; set; }
        [JsonIgnore]
        public string? Input { get; set; }


        [JsonPropertyName("input")]
        public IList<string>? InputCalculated
        {
            get
            {
                if (Input != null && InputAsList != null)
                {
                    throw new ValidationException("Input and InputAsList can not be assigned at the same time. One of them is should be null.");
                }

                if (Input != null)
                {
                    return new List<string> { Input };
                }

                return InputAsList;
            }
        }
    }
}
