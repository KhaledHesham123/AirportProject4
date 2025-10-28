namespace AirportProject4.Project.core.Entities.main
{
    public class Aircraft:BaseEntity
    {
        public string Model { get; set; } = string.Empty;
        public int Capacity { get; set; }

        public int AirLineId {  get; set; }

        public Airlines Airline { get; set; } = null!;
        public ICollection<Flight> Flights { get; set; } = new List<Flight>();
    }
}
