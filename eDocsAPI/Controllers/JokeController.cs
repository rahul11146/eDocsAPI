using Microsoft.AspNetCore.Mvc;

namespace CircuitBreaker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JokeController : ControllerBase
    {
        public readonly IJokeService _jokeService;

        public JokeController(IJokeService jokeService)
        {
            _jokeService = jokeService;
        }

        [HttpGet(), Route("GetRandomeJokes")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _jokeService.GetRandomJokes();
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}