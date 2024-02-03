using System.Runtime.Serialization;
using YamlDotNet.Core.Tokens;

namespace TerraMours_Gpt.Domains.LoginDomain.Contracts.Enum {
    /// <summary>
    /// 统计类型枚举
    /// </summary>
    public enum AnalysisTypeEnum {
        [EnumMember(Value = "提问量")]
        Ask,
        [EnumMember(Value = "使用人数")]
        Use,
        [EnumMember(Value = "生成图片数量")]
        Image,
        [EnumMember(Value = "订单量")]
        Odrer,
        [EnumMember(Value = "用户总数")]
        User,
        [EnumMember(Value = "销售额")]
        SaleMoney,
        [EnumMember(Value = "Token消耗量")]
        Token
    }
}
