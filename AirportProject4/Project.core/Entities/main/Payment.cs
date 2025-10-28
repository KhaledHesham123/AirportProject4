namespace AirportProject4.Project.core.Entities.main
{
    public class Payment:BaseEntity
    {
        public decimal Amount { get; set; }
        public string Method { get; set; } = string.Empty; // CreditCard, PayPal, Cash
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; } = string.Empty;

        public int BookingId { get; set; }

        // Navigation
    }
}

