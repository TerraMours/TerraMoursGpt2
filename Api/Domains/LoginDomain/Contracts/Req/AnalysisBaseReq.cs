using Newtonsoft.Json;
using TerraMours_Gpt.Domains.LoginDomain.Contracts.Enum;

namespace TerraMours_Gpt.Domains.LoginDomain.Contracts.Req
{
    /// <summary>
    /// 统计入参
    /// </summary>
    public class AnalysisBaseReq
    {
        /// <summary>
        /// 筛选类型
        /// </summary>
        public DateTypeEnum? DateType { get; set; }
        /// <summary>
        /// 开始时间(后续扩展，v1.0不支持)
        /// </summary>
        [JsonIgnore]
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间(后续扩展，v1.0不支持)
        /// </summary>
        [JsonIgnore]
        public DateTime? EndTime { get; set; }
    }
}
