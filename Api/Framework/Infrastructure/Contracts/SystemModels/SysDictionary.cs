using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using TerraMours.Framework.Infrastructure.Contracts.SystemModels;

namespace TerraMours_Gpt.Framework.Infrastructure.Contracts.SystemModels
{
    /// <summary>
    /// 字典表，存枚举，下拉框的值那些
    /// </summary>
    public class SysDictionary:BaseEntity
    {
        [Key]
        public long DictionaryId { get; set; }
        /// <summary>
        /// 字典json
        /// </summary>
        public JObject? Dictionary { get; set; }
    }
}
