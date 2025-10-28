using AirportProject4.Project.core.Entities.Identity;

namespace AirportProject4.Project.core.Entities.main
{
    public class Ticket:BaseEntity
    {
        public int Id { get; set; }

        public int FlightId { get; set; }
        public Flight Flight { get; set; } = null!;

        public int UserId { get; set; }
        public AppUser User { get; set; }

        public int? SeatId { get; set; }
        public Seat? Seat { get; set; }

        public decimal Price { get; set; }

    }
}
