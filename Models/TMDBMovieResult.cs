using System.Text.Json.Serialization;

namespace ActivityMatcher.API.Models
{
    public class TMDBMovieResult
    {
        public int Page { get; set; }

        public List<TMDBMovie> Results { get; set; }
        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }
        [JsonPropertyName("total_results")]
        public int TotalResults { get; set; }
    }
}
