using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AITravelPlanner.Backend.Models
{
    public class Trip
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // FK to User
        [Required]
        public Guid UserId { get; set; }

        [Required, MaxLength(150)]
        public string Destination { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 1_000_000)]
        public decimal Budget { get; set; }

        // Optional JSON/string of preferences (e.g., "food, culture, hiking")
        [MaxLength(1000)]
        public string? Preferences { get; set; }

        public int TravelersCount { get; set; } = 1;

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        // Navigation
        public User? User { get; set; }
        public ICollection<Itinerary> Itineraries { get; set; } = new List<Itinerary>();
    }
}
