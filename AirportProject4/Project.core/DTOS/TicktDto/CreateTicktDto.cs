using AirportProject4.Project.core.Entities.main;

namespace AirportProject4.Project.core.DTOS.TicktDto
{
    public class CreateTicktDto
    {
        // هتاخدها من الـ Route أو من الفورم
        public int FlightId { get; set; }

        public string SeatType { get; set; }
        // بيانات الراكب (لو مش موجود في الداتابيس)
        public string PassengerFullName { get; set; } = null!;
        public string PassengerPassportNumber { get; set; } = null!;

        // لو عندك اختيار مقعد معين
        //public int? SeatId { get; set; } // اختياري

        // السعر اللي هيتحسب أو يدخل يدوي
        //public decimal Price { get; set; }
    }
}
