namespace TerraMours_Gpt.Domains.GptDomain.Contracts.Res {
    /// <summary>
    /// 图片信息
    /// </summary>
    public class ImageRes {
        /// <summary>
        /// 主键
        /// </summary>
        public long ImageRecordId { get; set; }

        /// <summary>
        /// 模型类型
        /// </summary>
        public int? ModelType { get; set; }
        /// <summary>
        /// 聊天模型
        /// </summary>
        public string? Model { get; set; }

        public string? IP { get; set; }
        /// <summary>
        /// 提问词
        /// </summary>
        public string? Prompt { get; set; }

        /// <summary>
        /// 翻译后的提问词
        /// </summary>
        public string? PranslatePrompt { get; set; }

        /// <summary>
        /// 图片尺寸 256/512/1024
        /// </summary>
        public int? Size { get; set; }
        /// <summary>
        /// 生成图片地址
        /// </summary>
        public string? ImagUrl { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public long? UserId { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// 是否公开到图片广场
        /// </summary>
        public bool? IsPublic { get; set; }
        /// <summary>
        /// 转发量
        /// </summary>
        public int? ForwardCount { get; set; }

        /// <summary>
        /// 收藏量
        /// </summary>
        public int? CollectCount { get; set; }
        /// <summary>
        /// 点赞量
        /// </summary>
        public int? LikeCount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate { get; set; }
    }
}
