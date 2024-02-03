using TerraMours.Framework.Infrastructure.Contracts.SystemModels;

namespace TerraMours_Gpt.Framework.Infrastructure.Contracts.GptModels
{
    /// <summary>
    /// 图片生成记录
    /// </summary>
    public class ImageRecord: BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long ImageRecordId { get; set; }

        /// <summary>
        /// 模型类型
        /// </summary>
        public int? ModelType { get; set; }
        /// <summary>
        /// 聊天模型
        /// </summary>
        public string? Model { get; set; }

        public string? IP { get; set; }
        /// <summary>
        /// 提问词
        /// </summary>
        public string? Prompt { get; set; }

        /// <summary>
        /// 翻译后的提问词
        /// </summary>
        public string? PranslatePrompt { get; set; }

        /// <summary>
        /// 图片尺寸 256/512/1024
        /// </summary>
        public int? Size { get; set; }
        /// <summary>
        /// 生成图片地址
        /// </summary>
        public string? ImagUrl { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public long? UserId { get; set; }
        /// <summary>
        /// 是否公开到图片广场
        /// </summary>
        public bool? IsPublic { get; set;}
        /// <summary>
        /// 转发量
        /// </summary>
        public int? ForwardCount { get; set; }

        /// <summary>
        /// 收藏量
        /// </summary>
        public int? CollectCount { get;set; }
        /// <summary>
        /// 点赞量
        /// </summary>
        public int? LikeCount { get; set;}

        public ImageRecord() {
        }

        public ImageRecord(string? prompt, string? pranslatePrompt, string? imagUrl, int? modelType, string? model,int? size,long? userId) {
            this.Prompt = prompt;
            this.PranslatePrompt = pranslatePrompt;
            this.ImagUrl=imagUrl; 
            this.ModelType = modelType;
            this.Model = model;
            this.Size = size;
            this.UserId = userId;
            //EntityBase
            this.Enable = true;
            this.CreateDate = DateTime.Now;
            this.CreateID = userId;
            this.IsPublic = false;
        }

        public ImageRecord Delete(long? userId) {
            this.Enable = false;
            //EntityBase
            this.ModifyDate = DateTime.Now;
            this.ModifyID = userId;
            return this;
        }

        public ImageRecord Change(string? imagUrl, long? userId) {
            this.ImagUrl = imagUrl;
            //EntityBase
            this.ModifyDate = DateTime.Now;
            this.ModifyID = userId;
            return this;
        }

        public ImageRecord Share(bool IsPublic,long? userId) {
            this.IsPublic = IsPublic;
            //EntityBase
            this.ModifyDate = DateTime.Now;
            this.ModifyID = userId;
            return this;
        }
    }
}
