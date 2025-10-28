using AirportProject4.Project.core;
using AirportProject4.Project.core.DTOS.FlightsDto;
using AirportProject4.Project.core.Entities.main;
using AirportProject4.Project.core.ServiceContrect;
using AirportProject4.Shared;

namespace AirportProject4.Project.Service
{
    public class FlightAvailabilityService
    {
        public class FlightAvailabilityServices : IFlightAvailabilityService
        {
            //private readonly IunitofWork _unitOfWork;

            //public FlightAvailabilityServices(IunitofWork unitOfWork)
            //{
            //    _unitOfWork = unitOfWork;
            //}

            public async Task<List<AvailableFlightDto>> GetAvailableFlightsAsync(List<Flight> flights)
            {
                var availableFlights = new List<AvailableFlightDto>();

                foreach (var flight in flights)
                {
                    int totalSeats = flight.Seats?.Count ?? 0;
                    int bookedTickets = flight.Tickets?.Count ?? 0;
                    int availableSeats = totalSeats - bookedTickets;

                    if (availableSeats > 0)
                    {
                        availableFlights.Add(new AvailableFlightDto
                        {
                            FlightId = flight.Id,
                            FlightNumber = flight.FlightNumber,
                            DepartureAirport = flight.DepartureAirport,
                            ArrivalAirport = flight.ArrivalAirport,
                            DepartureTime = flight.DepartureTime,
                            ArrivalTime = flight.ArrivalTime,
                            AirlineName = flight.Aircraft?.Airline?.Name ?? "Unknown Airline",
                            AircraftModel = flight.Aircraft?.Model ?? "Unknown Model",
                            TotalSeats = totalSeats,
                            BookedSeats = bookedTickets,
                            AvailableSeats = availableSeats
                        });
                    }
                }

                return await Task.FromResult(availableFlights);
            }
        }
    }
}
