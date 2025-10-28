using AirportProject4.Project.core.Entities.main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirportProject4.Project.Repo.Data.Configration
{
    public class FlightConfiguration : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.Property(f => f.FlightNumber)
                 .IsRequired()
                 .HasMaxLength(20);

            builder.Property(f => f.DepartureTime)
                   .IsRequired();

            builder.Property(f => f.ArrivalTime)
                   .IsRequired();

            builder.Property(f => f.DepartureAirport)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(f => f.ArrivalAirport)
                   .IsRequired()
                   .HasMaxLength(100);

            // علاقة Flight -> Tickets (One-to-Many)
            builder.HasMany(f => f.Tickets)
                   .WithOne(t => t.Flight)
                   .HasForeignKey(t => t.FlightId)
                   .OnDelete(DeleteBehavior.Cascade); // أو NoAction
        }
    }
}
