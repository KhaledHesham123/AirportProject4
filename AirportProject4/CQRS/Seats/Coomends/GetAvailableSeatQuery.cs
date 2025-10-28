using AirportProject4.Project.core.Entities.main;
using MediatR;

namespace AirportProject4.CQRS.Seats.Coomends
{
    public record GetAvailableSeatQuery(Flight Flight,string seatType) :IRequest<Seat?>;

    public class GetAvailableSeatQueryHandler : IRequestHandler<GetAvailableSeatQuery, Seat?>
    {
        public Task<Seat?> Handle(GetAvailableSeatQuery request, CancellationToken cancellationToken)
        {
            var flight= request.Flight;
            if (flight == null)
                throw new Exception("Flight not found");


            // 2️⃣ هات كل الـ SeatId المحجوزة من التذاكر في نفس الـ Flight
            var availableSeat = flight.Seats
      .Where(s =>
          s.Class.Equals(request.seatType, StringComparison.OrdinalIgnoreCase)
          && !flight.Tickets.Any(t => t.SeatId == s.Id))
      .FirstOrDefault();

            return Task.FromResult(availableSeat); // عشان مسخدمتش await فمش async

        }
    }
}
