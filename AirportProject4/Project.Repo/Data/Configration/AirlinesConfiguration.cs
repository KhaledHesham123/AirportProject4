using AirportProject4.Project.core.Entities.main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirportProject4.Project.Repo.Data.Configration
{
    public class AirlinesConfiguration : IEntityTypeConfiguration<Airlines>
    {
        public void Configure(EntityTypeBuilder<Airlines> builder)
        {
            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            // علاقة Airline -> Aircrafts (One-to-Many)
            builder.HasMany(a => a.Aircraft)
                   .WithOne(ac => ac.Airline)
                   .HasForeignKey(ac => ac.AirLineId)
                   .OnDelete(DeleteBehavior.Cascade); // أو NoAction لو حابب
        }
    }
}
