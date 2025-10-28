using AirportProject4.Project.core;
using AirportProject4.Project.core.DTOS.FlightsDto;
using AirportProject4.Project.core.Entities.main;
using AirportProject4.Project.core.NewFolder.InterfaceContrect;
using AirportProject4.Project.core.ServiceContrect;
using AirportProject4.Project.Repo;
using AirportProject4.Project.Repo.Repositories;
using AirportProject4.Shared;
using AirportProject4.Shared.Specification;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AirportProject4.CQRS.Flights.FlightOrchestor
{
    
    public record GetAvailableFlightOrchestrator(int pageSize = 10,
        int pageIndex = 1,
        string? sortBy = "departuretime",
        string? sortDir = "asc",
        string? search = null) : IRequest<PaginatedListDto<AvailableFlightDto>>;

    public class GetAvailableFlightOrchestratorHandler :BaseQueryHandler ,IRequestHandler<GetAvailableFlightOrchestrator, PaginatedListDto<AvailableFlightDto>>
    {
        private readonly IRepo<Flight> flightrepo;
        private readonly IFlightAvailabilityService _flightAvailabilityService;

        public GetAvailableFlightOrchestratorHandler(IRepo<Flight> flightrepo,

            IFlightAvailabilityService flightAvailabilityService)
        {
            this.flightrepo = flightrepo;
            _flightAvailabilityService = flightAvailabilityService;
        }
        public async Task<PaginatedListDto<AvailableFlightDto>> Handle(GetAvailableFlightOrchestrator request, CancellationToken cancellationToken)
        {


            var flights = flightrepo.GetAll()
                .Where(f => f.DepartureTime >= DateTime.UtcNow)
                .Include(f => f.Aircraft).ThenInclude(a => a.Airline)
                .Include(f => f.Seats)
                .Include(f => f.Tickets)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.search))
                flights = ApplySearch(flights, request.search, f => f.FlightNumber);

            flights = ApplySorting(
                flights,
                request.sortBy ?? "departuretime",
                request.sortDir ?? "asc",
                f => f.DepartureTime,
                new Dictionary<string, System.Linq.Expressions.Expression<Func<Flight, object>>>
                {
                    { "flightnumber", f => f.FlightNumber },
                    { "departureairport", f => f.DepartureAirport },
                    { "arrivalairport", f => f.ArrivalAirport },
                    { "departuretime", f => f.DepartureTime },
                    { "arrivaltime", f => f.ArrivalTime }
                });

            var totalCount = await flights.CountAsync(cancellationToken);

            flights = Applypagination(flights, request.pageIndex, request.pageSize);

            var flightList = await flights.ToListAsync(cancellationToken);

            var availableFlights = await _flightAvailabilityService.GetAvailableFlightsAsync(flightList);

           return  new PaginatedListDto<AvailableFlightDto>
            {
                Items = availableFlights,
                TotalCount = totalCount,
                PageNumber = request.pageIndex,
                PageSize = request.pageSize
            };

        }
    }
}
