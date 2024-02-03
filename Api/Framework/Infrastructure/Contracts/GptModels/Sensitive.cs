using TerraMours.Framework.Infrastructure.Contracts.SystemModels;

namespace TerraMours_Gpt.Framework.Infrastructure.Contracts.GptModels
{
    /// <summary>
    /// 敏感词表
    /// </summary>
    public class Sensitive:BaseEntity
    {
        public long SensitiveId { get; set; }
        public string Word { get; set; }

        public Sensitive()
        {
        }
        public Sensitive(string word, long? userId)
        {
            Word = word;
            //EntityBase
            Enable = true;
            CreateDate = DateTime.Now;
            CreateID = userId;
        }

        public Sensitive Delete(long? userId)
        {
            this.Enable=false;
            //EntityBase
            this.ModifyDate = DateTime.Now;
            this.ModifyID = userId;
            return this;
        }

        public Sensitive Change(string word, long? userId)
        {
            this.Word = word;
            //EntityBase
            this.ModifyDate = DateTime.Now;
            this.ModifyID = userId;
            return this;
        }
    }
}
