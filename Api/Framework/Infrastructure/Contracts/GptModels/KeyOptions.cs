using TerraMours.Framework.Infrastructure.Contracts.SystemModels;

namespace TerraMours_Gpt.Framework.Infrastructure.Contracts.GptModels
{
    /// <summary>
    ///  key配置和使用情况表
    /// </summary>
    public class KeyOptions:BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long KeyId { get; set; }
        /// <summary>
        /// ApiKey
        /// </summary>
        public string? ApiKey { get; set; }
       
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpirationTime { get; set; }
        /// <summary>
        /// 使用量
        /// </summary>
        public decimal? Used { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public decimal? UnUsed { get; set; }

        /// <summary>
        /// 总量
        /// </summary>
        public decimal? Total { get; set; }


        public KeyOptions()
        {
        }
        public KeyOptions(string apiKey, long? userId)
        {
            ApiKey = apiKey;
            //EntityBase
            Enable = true;
            CreateDate = DateTime.Now;
            CreateID = userId;
        }

        public KeyOptions Delete(long? userId)
        {
            this.Enable = false;
            //EntityBase
            this.ModifyDate = DateTime.Now;
            this.ModifyID = userId;
            return this;
        }

        public KeyOptions Change(string apiKey, long? userId)
        {
            this.ApiKey = apiKey;
            //EntityBase
            this.ModifyDate = DateTime.Now;
            this.ModifyID = userId;
            return this;
        }
        public KeyOptions UpdateUsed(decimal? used, decimal? unUsed, decimal? total, long? userId)
        {
            this.Used = used;
            this.UnUsed = unUsed;
            this.Total = total;
            //EntityBase
            this.ModifyDate = DateTime.Now;
            this.ModifyID = userId;
            if(unUsed < 0)
            {
                this.Enable = false;
            }
            return this;
        }
    }
}
