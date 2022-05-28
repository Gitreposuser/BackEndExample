using Domain.Contracts.Filters.Models;
using System.Text.Json.Serialization;

namespace Host.Models
{
    public class MangaFilterModel : IMangaFilterParameters
    {
        [JsonPropertyName("bookType")]
        public string Genre { get; set; } = string.Empty;

        [JsonPropertyName("author")]
        public string Author { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
    }
}
