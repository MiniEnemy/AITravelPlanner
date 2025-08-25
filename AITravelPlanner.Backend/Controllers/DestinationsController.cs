using Microsoft.AspNetCore.Mvc;

namespace AITravelPlanner.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DestinationsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetDestinations()
        {
            var destinations = new List<object>
            {
                new { Id = 1, Name = "Pokhara", Country = "Nepal" },
                new { Id = 2, Name = "Paris", Country = "France" },
                new { Id = 3, Name = "Tokyo", Country = "Japan" }
            };

            return Ok(destinations);
        }
    }
}
