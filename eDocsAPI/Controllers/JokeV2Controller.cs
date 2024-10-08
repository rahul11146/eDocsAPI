using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CircuitBreaker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JokeV2Controller : ControllerBase
    {
        public readonly IJokeService _jokeService;

        public JokeV2Controller(IJokeService jokeService)
        {
            _jokeService = jokeService;
        }

        [HttpGet(Name = "GetRandomeJokesV2")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _jokeService.GetRandomJokesV2();
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

