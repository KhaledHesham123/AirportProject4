namespace AirportProject4.Project.core.DTOS.FlightsDto
{
    public class FlightUpdateDto
    {
        public int Id { get; set; } // لازم نعرف الرحلة اللي هنحدثها

        public string? FlightNumber { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public int AircraftId { get; set; } // ممكن تتغير الطيارة المرتبطة بالرحلة

        public string? DepartureAirport { get; set; }

        public string? ArrivalAirport { get; set; }
    }
}
