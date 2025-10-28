using AirportProject4.Project.core;
using AirportProject4.Project.core.Entities.main;
using AirportProject4.Project.core.NewFolder.InterfaceContrect;
using AirportProject4.Project.Repo.Repositories;
using AirportProject4.Shared;
using AirportProject4.Shared.Specification;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AirportProject4.CQRS.Flights.Quries
{
    public record GetAllFlightsQuery(int pageSize, int pageindex, string? sortby = "id"
        , string? sortdirc = "desc",
        string? search = null) : IRequest<PaginatedListDto<Flight>>;


    public class GetAllFlightsHandler :BaseQueryHandler, IRequestHandler<GetAllFlightsQuery, PaginatedListDto<Flight>>
    {
        private readonly IRepo<Flight> flgihtrepo;

        public GetAllFlightsHandler(IRepo<Flight> Flgihtrepo)
        {
            flgihtrepo = Flgihtrepo;
        }

        public async Task<PaginatedListDto<Flight>> Handle(GetAllFlightsQuery request, CancellationToken cancellationToken)
        {
            var flights = flgihtrepo.GetAll()
                .Include(f => f.Aircraft).ThenInclude(a => a.Airline)
                .Include(f => f.Seats)
                .Include(f => f.Tickets).ThenInclude(t => t.User)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.search))
                flights = ApplySearch(flights, request.search, x => x.FlightNumber);

            flights = ApplySorting(flights,
                request.sortby ?? "id",
                request.sortdirc ?? "desc",
                x => x.Id,
                new Dictionary<string, System.Linq.Expressions.Expression<Func<Flight, object>>>
                {
            { "flightnumber", x => x.FlightNumber },
            { "departuretime", x => x.DepartureTime },
            { "arrivaltime", x => x.ArrivalTime },
            { "departureairport", x => x.DepartureAirport },
            { "arrivalairport", x => x.ArrivalAirport }
                });

            var totalCount = await flights.CountAsync(cancellationToken);

            flights = Applypagination(flights, request.pageindex, request.pageSize);

            var items = await flights.ToListAsync(cancellationToken);

            return new PaginatedListDto<Flight>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.pageindex,
                PageSize = request.pageSize
            };
        }
    }
}
