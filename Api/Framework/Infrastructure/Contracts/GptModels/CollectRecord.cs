using TerraMours.Framework.Infrastructure.Contracts.SystemModels;

namespace TerraMours_Gpt.Framework.Infrastructure.Contracts.GptModels
{
    /// <summary>
    /// 收藏记录（点赞收藏转发）
    /// 收藏记录id+用户id 唯一索引
    /// </summary>
    public class CollectRecord:BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long CollectRecordId { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 收藏记录id
        /// </summary>
        public long? RecordId { get; set; }

        /// <summary>
        /// 收藏记录类型 0图片 1聊天记录
        /// </summary>
        public int? RecordType { get; set; }

        /// <summary>
        /// 添加人IP
        /// </summary>
        public string? IP { get; set; }

        /// <summary>
        /// 是否转发
        /// </summary>
        public bool? Forward { get; set; }

        /// <summary>
        /// 是否收藏
        /// </summary>
        public bool? Collect { get; set; }
        /// <summary>
        /// 是否点赞
        /// </summary>
        public bool? Like { get; set; }
    }
}
