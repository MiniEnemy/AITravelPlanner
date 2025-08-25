using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AITravelPlanner.Backend.Models
{
    public class Itinerary
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // FK to Trip
        [Required]
        public Guid TripId { get; set; }

        [Range(1, 365)]
        public int DayNumber { get; set; }

        [Required, MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string? Description { get; set; }

        [MaxLength(150)]
        public string? LocationName { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 1_000_000)]
        public decimal? EstimatedCost { get; set; }

        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }

        // Navigation
        public Trip? Trip { get; set; }
    }
}
