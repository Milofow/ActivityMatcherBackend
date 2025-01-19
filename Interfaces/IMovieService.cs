using ActivityMatcher.API.Models;

namespace ActivityMatcher.API.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetPopularMoviesAsync(int amount = 10);
    }
}
