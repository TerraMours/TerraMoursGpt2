using TerraMours.Framework.Infrastructure.Contracts.SystemModels;

namespace TerraMours_Gpt.Framework.Infrastructure.Contracts.GptModels
{
    /// <summary>
    /// 提示词管理
    /// </summary>
    public class PromptOptions:BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long PromptId { get; set; }

        /// <summary>
        /// 扮演
        /// </summary>
        public string? Act { get; set; }

        /// <summary>
        /// 提示词
        /// </summary>
        public string? Prompt { get; set; }

        /// <summary>
        /// 使用次数
        /// </summary>
        public int? UsedCount { get; set; }

        public PromptOptions()
        {
        }

        public PromptOptions(string? act,string? prompt, long? userId)
        {
            Act = act;
            Prompt = prompt;
            //EntityBase
            Enable = true;
            CreateDate = DateTime.Now;
            CreateID = userId;
        }

        public PromptOptions Delete(long? userId)
        {
            this.Enable = false;
            //EntityBase
            this.ModifyDate = DateTime.Now;
            this.ModifyID = userId;
            return this;
        }

        public PromptOptions Change(string? act, string? prompt, long? userId)
        {
            this.Act = act;
            this.Prompt = prompt;
            //EntityBase
            this.ModifyDate = DateTime.Now;
            this.ModifyID = userId;
            return this;
        }
    }
}
