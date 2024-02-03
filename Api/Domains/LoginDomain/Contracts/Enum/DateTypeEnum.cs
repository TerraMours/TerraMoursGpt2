using System.Runtime.Serialization;
using YamlDotNet.Core.Tokens;

namespace TerraMours_Gpt.Domains.LoginDomain.Contracts.Enum
{
    /// <summary>
    /// 时间类型枚举
    /// </summary>
    public enum DateTypeEnum
    {
        [EnumMember(Value = "全部")]
        All,
        [EnumMember(Value = "今天")]
        Today,
        [EnumMember(Value = "最近7天")]
        Lately7,
        [EnumMember(Value = "最近30天")]
        Lately30,
        [EnumMember(Value = "月份")]
        Month,
        [EnumMember(Value = "年份")]
        Year
    }
}
