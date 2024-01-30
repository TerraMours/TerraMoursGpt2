namespace TerraMours_Gpt_Api.Domains.GptDomain.Contracts.Req
{
    public class KnowledgeReq
    {
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
        public string? WorkSpace { get; set; }
        /// <summary>
        /// 命名空间
        /// </summary>
        public string? NameSpace { get; set; }

        public string? BaseUrl { get; set; }
        /// <summary>
        /// 商品图片路径
        /// </summary>
        public string? ImagePath { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string? Remark { get; set; }
    }
    public class KnowledgeUpdateReq
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
        public string? WorkSpace { get; set; }
        /// <summary>
        /// 命名空间
        /// </summary>
        public string? NameSpace { get; set; }

        public string? BaseUrl { get; set; }
        /// <summary>
        /// 商品图片路径
        /// </summary>
        public string? ImagePath { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string? Remark { get; set; }
    }
}
