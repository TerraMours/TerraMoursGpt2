using TerraMours_Gpt.Domains.LoginDomain.Contracts.Enum;

namespace TerraMours_Gpt.Domains.LoginDomain.Contracts.Req {
    public class AnalysisListReq:AnalysisBaseReq {
        public AnalysisTypeEnum? AnalysisType { get; set; }

        public AnalysisListReq() {

        }

        public AnalysisListReq(AnalysisTypeEnum? analysisType, AnalysisBaseReq baseReq) {
            AnalysisType = analysisType;
            DateType=baseReq.DateType;
            StartTime = baseReq.StartTime;
            EndTime = baseReq.EndTime;
        }
    }
}
