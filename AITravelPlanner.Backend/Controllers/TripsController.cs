using AITravelPlanner.Backend.Data;
using AITravelPlanner.Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AITravelPlanner.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private readonly TravelPlannerDbContext _db;
        public TripsController(TravelPlannerDbContext db) => _db = db;

        // GET: api/trips?userId=...
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetByUser([FromQuery] Guid userId)
        {
            var trips = await _db.Trips
                .Where(t => t.UserId == userId)
                .Select(t => new {
                    t.Id,
                    t.UserId,
                    t.Destination,
                    t.StartDate,
                    t.EndDate,
                    t.Budget,
                    t.Preferences,
                    t.TravelersCount,
                    t.CreatedAtUtc
                })
                .ToListAsync();

            return Ok(trips);
        }

        // POST: api/trips
        [HttpPost]
        public async Task<ActionResult<object>> Create([FromBody] Trip trip)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // quick sanity check
            if (trip.EndDate < trip.StartDate)
                return BadRequest("EndDate cannot be earlier than StartDate.");

            _db.Trips.Add(trip);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByUser), new { userId = trip.UserId }, new { trip.Id });
        }
    }
}
