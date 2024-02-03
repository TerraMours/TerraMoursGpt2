using System.ComponentModel.DataAnnotations;

namespace Terramours_Gpt_Vector.Entities
{
    public class IndexItem: BaseEntity
    {
        [Key]
        public int IndexId { get; set; }
        /// <summary>
        /// name
        /// </summary>
        public string Name { get; init; }
        /// <summary>
        /// key
        /// </summary>
        public string? Key { get; set; }
        /// <summary>
        /// 三方关联Id
        /// </summary>
        public string? ThirdPartId { get; set; }
    }
}
