namespace AirportProject4.Project.core.DTOS.FlightsDto
{
    public class AvailableFlightDto
    {
        public int FlightId { get; set; }
        public string FlightNumber { get; set; } = string.Empty;

        public string AirlineName { get; set; } = string.Empty;    // اسم شركة الطيران
        public string AircraftModel { get; set; } = string.Empty;  // الموديل للطائرة

        public string DepartureAirport { get; set; } = string.Empty;
        public string ArrivalAirport { get; set; } = string.Empty;

        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

        public int TotalSeats { get; set; }       // السعة الكاملة للطائرة
        public int BookedSeats { get; set; }      // عدد المقاعد المحجوزة
        public int AvailableSeats { get; set; }   // عدد المقاعد المتاحة
    }
}
