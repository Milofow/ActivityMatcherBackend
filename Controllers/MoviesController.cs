using ActivityMatcher.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("popular")]
        public async Task<IActionResult> GetPopularMovies()
        {
            var movies = await _movieService.GetPopularMoviesAsync();
            return Ok(movies);
        }
    }
}
