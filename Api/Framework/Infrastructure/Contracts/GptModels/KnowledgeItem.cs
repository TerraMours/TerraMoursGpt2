using System.ComponentModel.DataAnnotations.Schema;
using TerraMours.Framework.Infrastructure.Contracts.SystemModels;

namespace TerraMours_Gpt_Api.Framework.Infrastructure.Contracts.GptModels
{
    /// <summary>
    /// 知识库
    /// </summary>
    public class KnowledgeItem : BaseEntity
    {
        public int KnowledgeId { get; set; }

        
        public string? KnowledgeName { get; set; }
        /// <summary>
        /// 是否开放
        /// </summary>
        public bool? IsCommon { get; set; }

        /// <summary>
        /// 知识库类型：1.pgvector 2.pinecone
        /// </summary>
        public int? KnowledgeType { get; set; }

        public string? ApiKey { get; set; }

        public string? IndexName { get; set; }
        /// <summary>
        /// 工作空间
        /// </summary>
        public string?  WorkSpace { get; set; }
        /// <summary>
        /// 命名空间
        /// </summary>
        public string? NameSpace { get; set; }
        /// <summary>
        /// 代理地址，只在pgvector中使用
        /// </summary>
        public string? BaseUrl {  get; set; }
        /// <summary>
        /// 商品图片路径
        /// </summary>
        public string? ImagePath { get; set; }
    }
}
