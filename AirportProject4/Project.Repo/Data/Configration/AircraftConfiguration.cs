using AirportProject4.Project.core.Entities.main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirportProject4.Project.Repo.Data.Configration
{
    public class AircraftConfiguration : IEntityTypeConfiguration<Aircraft>
    {
        public void Configure(EntityTypeBuilder<Aircraft> builder)
        {
            builder.Property(a => a.Model)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(a => a.Capacity)
                   .IsRequired();

            builder.Property(a => a.AirLineId)
                   .IsRequired();

            // علاقة Aircraft -> Flights (One-to-Many)
            builder.HasMany(ac => ac.Flights)
                   .WithOne(f => f.Aircraft)
                   .HasForeignKey(f => f.AircraftId)
                   .OnDelete(DeleteBehavior.Cascade); // أو NoAction


        }
    }
}
