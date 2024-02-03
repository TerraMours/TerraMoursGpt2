using Terramours_Gpt_Vector.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Terramours_Gpt_Vector.Req
{
    public record VectorQueryReq : BaseReq
    {
        public string? Id { get; set; }
        public float[]? Vector { get; set; }
        public SparseVector? SparseVector { get; set; }
        public uint TopK { get; init; }
        public MetadataMap? Filter { get; init; }
        public string? Namespace { get; init; }
        public bool IncludeValues { get; init; }
        public bool IncludeMetadata { get; init; }
    }
    public record VectorUpsertReq : BaseReq
    {
        public IEnumerable<VectorDto> Vectors { get; init; }
        public string Namespace { get; init; }
    }
    public record VectorUpdateReq: BaseReq
    {
        public  string? Id { get; set; }
        public  float[]? Values { get; set; }
        public SparseVector? SparseValues { get; set; }
        public MetadataMap? SetMetadata { get; set; }
        public string? Namespace { get; set; }
    }
    public record VectorDeleteReq : BaseReq
    {
        public MetadataMap Filter { get; init; }

        public string? IndexNamespace { get; init; }
    }
}
