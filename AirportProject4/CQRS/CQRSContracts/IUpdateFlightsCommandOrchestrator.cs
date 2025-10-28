using AirportProject4.Project.core.DTOS.FlightsDto;
using AirportProject4.Project.core.DTOS.ViewModles;
using AirportProject4.Project.core.Entities.main;

namespace AirportProject4.CQRS.CQRSContracts
{
    public interface IUpdateFlightsCommandOrchestrator
    {
        Task<Flight> UpdateFlightsAsync(int? id, UpdateFlightviewmodle flightUpdateDto);

    }
}
