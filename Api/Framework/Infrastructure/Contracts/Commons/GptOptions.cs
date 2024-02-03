namespace TerraMours_Gpt.Framework.Infrastructure.Contracts.Commons
{
    public class GptOptions
    {
        
        public OpenAIOptions OpenAIOptions { get; set; }
        public ImagOptions ImagOptions { get; set; }
    }
    /// <summary>
    /// AI配置
    /// </summary>
    public class OpenAIOptions
    {
        /// <summary>
        /// 1K token定价
        /// </summary>
        public decimal TokenPrice { get; set; }
        /// <summary>
        /// 新用户赠送金额
        /// </summary>
        public decimal NewUserBalance { get; set; }
        public OpenAI? OpenAI { get; set; }
        public AzureOpenAI? AzureOpenAI { get; set; }
    }
    /// <summary>
    /// 图片生成配置
    /// </summary>
    public class ImagOptions
    {
        /// <summary>
        /// 图片定价
        /// </summary>
        public decimal ImagePrice { get; set; }

        /// <summary>
        /// 生成图片的静态文件地址
        /// </summary>
        public string? ImagFileBaseUrl { get; set; }

        //Stable Diffusion 配置
        public List<SDOptions>? SDOptions { get; set; }
    }
    /// <summary>
    /// Stable Diffusion 配置
    /// </summary>
    public class SDOptions
    {
        /// <summary>
        /// 标签
        /// </summary>
        public string? Label { get; set; }
        /// <summary>
        /// Stable Diffusion API地址
        /// </summary>
        public string? BaseUrl { get; set; }
        /// <summary>
        /// 反向描述词
        /// </summary>
        public string? Negative_Prompt { get; set; }
    }
    /// <summary>
    /// OpenAI配置
    /// </summary>
    public class OpenAI
    {
        /// <summary>
        /// OpenAI Key池
        /// </summary>
        public KeyOption[] KeyList { get; set; }
        public int MaxTokens { get; set; }
        public float Temperature { get; set; }
        public int FrequencyPenalty { get; set; }
        public int PresencePenalty { get; set; }
        public string ChatModel { get; set; }
        /// <summary>
        /// Diversity (0.0 - 1.0).
        /// </summary>
        public float TopP { get; set; }
        /// <summary>
        /// 上下文数量
        /// </summary>
        public int ContextCount { get; set; }
        /// <summary>
        /// 最大提问数量
        /// </summary>
        public int MaxQuestions { get; set; }
    }

    public class KeyOption
    {
        /// <summary>
        /// key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 代理地址
        /// </summary>
        public string BaseUrl { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// key 适用的模型
        /// </summary>
        public string[] ModelTypes { get; set; }
        /// <summary>
        /// 模型类型：OpenAi = 1, Baidu =2,Ali=3,SD=4, MJ=5
        /// </summary>
        public int Type { get; set; }
    }
    /// <summary>
    /// AzureOpenAI配置
    /// </summary>
    public class AzureOpenAI
    {
    }
}
