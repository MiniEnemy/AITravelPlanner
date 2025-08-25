using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AITravelPlanner.Backend.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        // Store only a hash here (never plain text)
        [Required, MaxLength(500)]
        public string PasswordHash { get; set; } = string.Empty;

        [Phone, MaxLength(30)]
        public string? Phone { get; set; }

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        // Navigation: A user can have many trips
        public ICollection<Trip> Trips { get; set; } = new List<Trip>();
    }
}
