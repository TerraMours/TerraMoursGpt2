using Pgvector;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Terramours_Gpt_Vector.Converters;
using Terramours_Gpt_Vector.Dto;

namespace Terramours_Gpt_Vector.Entities
{
    public class VectorItem: BaseEntity
    {
        [Key]
        public int VectorId { get; set; }
        public string Id { get; set; }
        [Column(TypeName = "vector(1536)")]
        public Vector? Embedding { get; set; }

        public SparseVector? SparseValues { get; set; }
        [NotMapped] // 不映射至数据库
        public MetadataMap? Metadata { get; set; }

        [Column("Metadata", TypeName = "jsonb")]
        public string? MetadataJson
        {
            get => Metadata != null ? JsonSerializer.Serialize(Metadata) : null;
            set => Metadata = !string.IsNullOrEmpty(value) ? JsonSerializer.Deserialize<MetadataMap>(value) : null; 
        }

        public string IndexName { get; set; }
        public string Namespace { get; set; }
    }
}
