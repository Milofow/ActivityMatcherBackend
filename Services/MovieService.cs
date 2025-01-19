using ActivityMatcher.API.Interfaces;
using ActivityMatcher.API.Models;
using System.Text.Json;

namespace ActivityMatcher.API.Services
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://api.themoviedb.org/3/movie";
        private readonly string _apiKey;

        public MovieService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _apiKey = Environment.GetEnvironmentVariable("TMDB_API_KEY")
                      ?? throw new ArgumentNullException("TMDB_API_KEY environment variable not set");
        }

        public async Task<IEnumerable<Movie>> GetPopularMoviesAsync(int amount = 10)
        {
            var popularMovies = new List<Movie>();
            int page = 1;
            int moviesPerPage = 20;
            
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _apiKey);

            while (popularMovies.Count < amount)
            {
                var response = await _httpClient.GetAsync(_baseUrl + "/popular?page=" + page);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var movieResult = JsonSerializer.Deserialize<TMDBMovieResult>(content, options);

                if (movieResult.Results == null || !movieResult.Results.Any())
                {
                    break;
                }

                popularMovies.AddRange(movieResult.Results.Select(m => new Movie
                {
                    Id = m.Id,
                    Title = m.Title,
                    Year = int.Parse(m.ReleaseDate.Split('-')[0]),
                    PosterImage = "https://image.tmdb.org/t/p/w500" + m.PosterPath
                }));

                if (movieResult.Results.Count < moviesPerPage)
                {
                    break;
                }

                page++;
            }
            return popularMovies.Take(amount).ToList();

        }
    }
}
