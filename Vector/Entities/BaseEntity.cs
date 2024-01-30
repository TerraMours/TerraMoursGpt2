namespace Terramours_Gpt_Vector.Entities
{
    public class BaseEntity
    {
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
