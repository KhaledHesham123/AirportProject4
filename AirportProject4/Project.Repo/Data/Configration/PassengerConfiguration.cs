//using FlyingProject.Project.core.Entities.main;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace FlyingProject.Project.Repo.Data.Configration
//{
//    public class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
//    {
//        public void Configure(EntityTypeBuilder<Passenger> builder)
//        {
//            builder.Property(p => p.FullName)
//                   .IsRequired()
//                   .HasMaxLength(150);

//            builder.Property(p => p.PassportNumber)
//                   .IsRequired()
//                   .HasMaxLength(20);

//            // علاقة Passenger -> Tickets (One-to-Many)
//            builder.HasMany(p => p.Tickets)
//                   .WithOne(t => t.Passenger)
//                   .HasForeignKey(t => t.PassengerId)
//                   .OnDelete(DeleteBehavior.Cascade); // أو NoAction

//        }
//    }
//}
