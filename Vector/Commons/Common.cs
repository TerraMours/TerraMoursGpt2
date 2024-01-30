using Terramours_Gpt_Vector.Req;

namespace Terramours_Gpt_Vector.Commons
{
    public static class Common
    {
        public static T GetHeader<T>(T item,HttpRequest request) where T : BaseReq
        {
            string apiKey = request.Headers["Api-Key"];
            item.Key = apiKey;
            return item;
        }
    }
}
