namespace TerraMours.Framework.Infrastructure.Contracts.SystemModels
{
    /// <summary>
    /// 工作流基础类审核
    /// </summary>
    public class WorkflowEntity
    {
        /// <summary>
        /// 审核状态 
        /// </summary>
        public int? AuditStatus { get; set; }
        /// <summary>
        /// 审核人ID
        /// </summary>
        public int? AuditorID { get; set; }
        /// <summary>
        /// 审核人名称
        /// </summary>
        public string? AuditorName { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? AuditDate { get; set; }
        /// <summary>
        /// 审核过期时间
        /// </summary>
        public DateTime? ExpireAuditDate { get; set; }
    }
}
