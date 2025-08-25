using AITravelPlanner.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace AITravelPlanner.Backend.Data
{
    public class TravelPlannerDbContext : DbContext
    {
        public TravelPlannerDbContext(DbContextOptions<TravelPlannerDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Trip> Trips => Set<Trip>();
        public DbSet<Itinerary> Itineraries => Set<Itinerary>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique Email for User
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // User 1 - * Trips
            modelBuilder.Entity<Trip>()
                .HasOne(t => t.User)
                .WithMany(u => u.Trips)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Trip 1 - * Itineraries
            modelBuilder.Entity<Itinerary>()
                .HasOne(i => i.Trip)
                .WithMany(t => t.Itineraries)
                .HasForeignKey(i => i.TripId)
                .OnDelete(DeleteBehavior.Cascade);

            // Default dates from SQL Server (server-side)
            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAtUtc)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Trip>()
                .Property(t => t.CreatedAtUtc)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
