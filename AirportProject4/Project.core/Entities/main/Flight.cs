namespace AirportProject4.Project.core.Entities.main
{
    public class Flight:BaseEntity
    {
        public string FlightNumber { get; set; } = null!;
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

        public int AircraftId { get; set; }
        public Aircraft Aircraft { get; set; } = null!;

        public string DepartureAirport { get; set; } = null!;
        public string ArrivalAirport { get; set; } = null!;

        public ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();

        public ICollection<Seat> Seats { get; set; } = new HashSet<Seat>();


    }



}

