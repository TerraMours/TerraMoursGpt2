using System.ComponentModel.DataAnnotations;

namespace Terramours_Gpt_Vector.Res
{
    public class ApikeyUpsertRes
    {
        /// <summary>
        /// key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// name
        /// </summary>
        public string Name { get; init; }
        /// <summary>
        /// 三方关联主键
        /// </summary>
        public string ThirdPartId { get; set; }

        /// <summary>
        /// 可用的index
        /// </summary>
        public string[] Indexs { get; set; }
    }
    public class ApikeyUpdateRes
    {
        public int KeyId { get; set; }
        /// <summary>
        /// key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// name
        /// </summary>
        public string Name { get; init; }
        /// <summary>
        /// 三方关联主键
        /// </summary>
        public string ThirdPartId { get; set; }

        /// <summary>
        /// 可用的index
        /// </summary>
        public string[] Indexs { get; set; }
    }
    public class ApikeyQueryRes
    {
        public int KeyId { get; set; }
        /// <summary>
        /// key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// name
        /// </summary>
        public string Name { get; init; }
        /// <summary>
        /// 三方关联主键
        /// </summary>
        public string ThirdPartId { get; set; }

        /// <summary>
        /// 可用的index
        /// </summary>
        public string[] Indexs { get; set; }
    }
}
