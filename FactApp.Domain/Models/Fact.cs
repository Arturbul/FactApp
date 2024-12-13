using System.Text.Json.Serialization;

namespace FactApp.Domain.Models
{
    public class Fact
    {
        [JsonPropertyName("fact")]
        public string FactContent { get; set; } = null!;
        public int Length { get; set; }
    }
}
