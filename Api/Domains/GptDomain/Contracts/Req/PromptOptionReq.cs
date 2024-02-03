using System.Text.Json.Serialization;

namespace TerraMours_Gpt.Domains.GptDomain.Contracts.Req
{
    public class PromptOptionReq
    {
        /// <summary>
        /// 扮演
        /// </summary>
        [JsonPropertyName("act")]
        public string? Act { get; set; }

        /// <summary>
        /// 提示词
        /// </summary>
        [JsonPropertyName("prompt")]
        public string? Prompt { get; set; }
    }
}
