using System.Text.Json.Serialization;

namespace TerraMours_Gpt.Domains.GptDomain.Contracts.Req {
    /// <summary>
    /// 图片发起类
    /// </summary>
    public class ImageReq :BaseModelReq{
        /// <summary>
        /// 提问词
        /// </summary>
        public string Prompt { get; set; }

        /// <summary>
        /// 生成图片数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 图片尺寸 256/512/1024
        /// </summary>
        public int? Size { get; set; }

        /// <summary>
        /// signalR 客户端Id
        /// </summary>
        public string ConnectionId { get; set; }


        /// <summary>
        /// 反向提示词
        /// </summary>
        public string? NegativePrompt { get; set; }

    }
}
