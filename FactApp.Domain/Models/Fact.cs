using Newtonsoft.Json;

namespace FactApp.Domain.Models
{
    public class Fact
    {
        [JsonProperty("fact")]
        public string FactContent { get; set; } = null!;
        public int Length { get; set; }
    }
}
