namespace AirportProject4.Project.core.DTOS.ViewModles
{
    public class FlightBookviewmodle
    {

        public int FlightId { get; set; }
        public string FlightNumber { get; set; } = null!;

        public string SeatType { get; set; } = "Economy";


        public string PassengerFullName { get; set; } = null!;
        public string PassengerPassportNumber { get; set; } = null!;

       
    }
}
