using System.Text.Json.Serialization;

namespace TerraMours.Domains.LoginDomain.Contracts.Req
{
    public class LoginRes
    {
        public LoginRes(string token, string refreshToken)
        {
            this.Token = token;
            this.RefreshToken = refreshToken;
        }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }

}
