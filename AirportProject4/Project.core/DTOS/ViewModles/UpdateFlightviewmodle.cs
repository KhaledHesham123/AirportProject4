namespace AirportProject4.Project.core.DTOS.ViewModles
{
    public class UpdateFlightviewmodle
    {
        public int Id { get; init; }                 // ده أساسي عشان تعرف أي Flight
        public DateTime? DepartureTime { get; init; }
        public DateTime? ArrivalTime { get; init; }
        public string? DepartureAirport { get; init; }
        public string? ArrivalAirport { get; init; }

        public string FlightNumber { get; init; } = string.Empty;   

        // Aircraft details (اختياري)
        public string? AirCraftModel { get; init; }
        public int? AircraftCapacity { get; init; }

        public string AirlineName { get; set; } = string.Empty;
        public string AirlineCode { get; set; } = string.Empty;
    }
}
