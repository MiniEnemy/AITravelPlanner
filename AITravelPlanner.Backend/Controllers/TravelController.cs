using Microsoft.AspNetCore.Mvc;
using AITravelPlannerAPI.Models;

namespace AITravelPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelController : ControllerBase
    {
        [HttpGet("destinations")]
        public ActionResult<IEnumerable<Destination>> GetDestinations()
        {
            var destinations = new List<Destination>
            {
                new Destination { Id = 1, City = "Paris", Country = "France", Description = "Eiffel Tower and museums" },
                new Destination { Id = 2, City = "Tokyo", Country = "Japan", Description = "Anime, temples, and sushi" },
                new Destination { Id = 3, City = "New York", Country = "USA", Description = "Times Square, Broadway, Central Park" }
            };

            return Ok(destinations);
        }
    }
}
