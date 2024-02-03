namespace TerraMours_Gpt.Domains.GptDomain.Utils;

/// <summary>
/// 获取access_token
/// </summary>
public class BaiduAPiTokenRes
{
    /// <summary>
    /// 访问凭证
    /// </summary>
    public string access_token;

    /// <summary>
    /// 有效期，Access Token的有效期。说明：单位是秒，有效期30天
    /// </summary>
    public int expires_in;

    /// <summary>
    /// 错误码 说明：响应失败时返回该字段，成功时不返回
    /// </summary>
    public string error;

    /// <summary>
    /// 错误描述信息，帮助理解和解决发生的错误 说明：响应失败时返回该字段，成功时不返回
    /// </summary>
    public string error_description;
}