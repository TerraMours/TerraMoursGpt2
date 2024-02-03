using Terramours_Gpt_Vector.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Terramours_Gpt_Vector.Res.Vector {
    public class DescribeIndexRes {
        [JsonPropertyName("database")]
        public required IndexDto Details { get; init; }

        public required IndexStatus Status { get; init; }
    }
}
