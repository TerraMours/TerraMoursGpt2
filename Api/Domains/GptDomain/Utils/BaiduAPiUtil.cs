namespace TerraMours_Gpt.Domains.GptDomain.Utils;
/// <summary>
/// 百度接口
/// </summary>
public static class BaiduAPiUtil
{
    /// <summary>
    /// 获取access_token
    /// </summary>
    /// <param name="apiKey"></param>
    /// <param name="secretKey"></param>
    /// <returns></returns>
    public static async Task<string> GetToken(string apiKey,string secretKey)
    {
        var httpClient = new HttpClient();
        var requestUrl = "https://aip.baidubce.com/rpc/2.0/ai_custom/v1/wenxinworkshop/text2image/sd_xl";
        var message = await httpClient.PostAsync(requestUrl, null);
        BaiduAPiTokenRes res = await message.Content.ReadFromJsonAsync<BaiduAPiTokenRes>();
        return res.access_token;
    }
    
    
}