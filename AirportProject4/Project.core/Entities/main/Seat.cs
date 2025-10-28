using System.Runtime.Serialization;

namespace AirportProject4.Project.core.Entities.main
{
    public enum SeatClass
    {
        [EnumMember(Value = "Economy")] //دي اللي بتخلي القيمة تتسلسل كـ string مخصص
        Economy = 1,

        [EnumMember(Value = "Business")]
        Business = 2,

        [EnumMember(Value = "First")]
        First = 3
    }
    public class Seat:BaseEntity
    {
        public string SeatNumber { get; set; } = string.Empty;
        public string Class { get; set; } // Economy, Business, First


        public int FlightId { get; set; }



    }
}
