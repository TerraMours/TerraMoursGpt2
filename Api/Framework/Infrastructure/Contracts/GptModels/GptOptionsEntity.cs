using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using TerraMours.Framework.Infrastructure.Contracts.Commons;
using TerraMours.Framework.Infrastructure.Contracts.SystemModels;
using TerraMours_Gpt.Framework.Infrastructure.Contracts.Commons;
using TerraMours_Gpt.Framework.Infrastructure.Contracts.SystemModels;

namespace TerraMours_Gpt.Framework.Infrastructure.Contracts.GptModels
{
    /// <summary>
    /// Gpt配置表
    /// </summary>
    public class GptOptionsEntity:BaseEntity
    {
        [Key]
        public long GptOptionsId { get; set; }

        /// <summary>
        /// AI配置
        /// </summary>
        public OpenAIOptions? OpenAIOptions { get; set; }
        /// <summary>
        /// 图片生成配置
        /// </summary>
        public ImagOptions? ImagOptions { get; set; }

        public GptOptionsEntity()
        {
        }

        public GptOptionsEntity(OpenAIOptions? openAIOptions, ImagOptions? imagOptions)
        {
            OpenAIOptions = openAIOptions;
            ImagOptions = imagOptions;
            //EntityBase
            Version = 1;
            Enable = true;
            CreateDate = DateTime.Now;
        }

        public GptOptionsEntity ChangeOpenAIOptions(OpenAIOptions? openAIOptions, long? userId)
        {
            //EntityBase
            this.OpenAIOptions = openAIOptions;
            this.ModifyDate = DateTime.Now;
            this.ModifyID = userId;
            return this;
        }

        public GptOptionsEntity ChangeImagOptions(ImagOptions? imagOptions, long? userId)
        {
            //EntityBase
            this.ImagOptions = imagOptions;
            this.ModifyDate = DateTime.Now;
            this.ModifyID = userId;
            return this;
        }
    }
}
