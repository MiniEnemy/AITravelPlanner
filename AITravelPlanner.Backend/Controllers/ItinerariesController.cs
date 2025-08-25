using AITravelPlanner.Backend.Data;
using AITravelPlanner.Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AITravelPlanner.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItinerariesController : ControllerBase
    {
        private readonly TravelPlannerDbContext _db;
        public ItinerariesController(TravelPlannerDbContext db) => _db = db;

        // GET: api/itineraries?tripId=...
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Itinerary>>> GetByTrip([FromQuery] Guid tripId)
        {
            var items = await _db.Itineraries
                .Where(i => i.TripId == tripId)
                .OrderBy(i => i.DayNumber)
                .ThenBy(i => i.StartTime)
                .ToListAsync();

            return Ok(items);
        }

        // POST: api/itineraries
        [HttpPost]
        public async Task<ActionResult<object>> Create([FromBody] Itinerary item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Ensure Trip exists
            var exists = await _db.Trips.AnyAsync(t => t.Id == item.TripId);
            if (!exists) return NotFound("Trip not found.");

            _db.Itineraries.Add(item);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByTrip), new { tripId = item.TripId }, new { item.Id });
        }
    }
}
