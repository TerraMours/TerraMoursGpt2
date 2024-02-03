using System.Text.Json.Serialization;

namespace TerraMours.Domains.LoginDomain.Contracts.Req
{
    public class EmailReq
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        public string UserEmail { get; set; }
    }
}
