using System.Text.Json.Serialization;

namespace TerraMours.Domains.LoginDomain.Contracts.Common
{
    public class ApiResponse<T>
    {
        public ApiResponse(int statusCode, string? message, T? data)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }
        
        [JsonPropertyName("code")]
        public int StatusCode { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }
        [JsonPropertyName("data")]
        public T? Data { get; set; }

        public static ApiResponse<T> Success(T data)
        {
            return new ApiResponse<T>(200, "ok", data);
        }

        public static ApiResponse<T> Fail(string message)
        {
            return new ApiResponse<T>(500, message, default(T));
        }
    }
}
