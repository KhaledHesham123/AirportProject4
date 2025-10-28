using AirportProject4.Project.core.Entities.Identity;
using AirportProject4.Project.core.Entities.main;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AirportProject4.Project.Repo.Data.Context
{
    public class AirlineDbContext: IdentityDbContext<AppUser, AppRole, int>

    {
        public AirlineDbContext(DbContextOptions<AirlineDbContext> options)
            : base(options)
        {
        }

        public DbSet<Airlines> Airlines { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // إعادة تسمية الجداول لو حابب
            builder.Entity<AppUser>().ToTable("Users");
            builder.Entity<AppRole>().ToTable("Roles");                // بدل IdentityRole<int>
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");

           

            builder.Entity<Ticket>()
          .HasOne(t => t.User)
          .WithMany(u => u.Tickets)
          .HasForeignKey(t => t.UserId)
          .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
