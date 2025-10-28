using AirportProject4.Project.core.Entities.main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirportProject4.Project.Repo.Data.Configration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            

            // Properties
            builder.Property(p => p.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Method)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(p => p.PaymentDate)
                   .IsRequired();

            builder.Property(p => p.Status)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(p => p.BookingId)
                   .IsRequired();

        }
    }
}
