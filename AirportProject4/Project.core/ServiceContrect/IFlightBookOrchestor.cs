using AirportProject4.Project.core.DTOS.TicktDto;
using AirportProject4.Project.core.Entities.main;

namespace AirportProject4.Project.core.ServiceContrect
{
    public interface IFlightBookOrchestor
    {
       Task<Flight> FlightBook(CreateTicktDto createTicktDto);

    }
}
