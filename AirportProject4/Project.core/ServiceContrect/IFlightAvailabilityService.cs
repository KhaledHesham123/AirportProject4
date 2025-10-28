using AirportProject4.Project.core.DTOS.FlightsDto;
using AirportProject4.Project.core.Entities.main;
using AirportProject4.Shared;

namespace AirportProject4.Project.core.ServiceContrect
{
    public interface IFlightAvailabilityService
    {
        Task<List<AvailableFlightDto>> GetAvailableFlightsAsync(List<Flight> flights);

    }
}
