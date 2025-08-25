using AITravelPlanner.Backend.Data;
using AITravelPlanner.Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AITravelPlanner.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly TravelPlannerDbContext _db;
        public UsersController(TravelPlannerDbContext db) => _db = db;

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAll()
        {
            // Do not expose PasswordHash
            var users = await _db.Users
                .Select(u => new { u.Id, u.FullName, u.Email, u.Phone, u.CreatedAtUtc })
                .ToListAsync();

            return Ok(users);
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<object>> Create([FromBody] User user)
        {
            // In production: hash the password before saving!
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAll), new { id = user.Id },
                new { user.Id, user.FullName, user.Email, user.Phone, user.CreatedAtUtc });
        }
    }
}
