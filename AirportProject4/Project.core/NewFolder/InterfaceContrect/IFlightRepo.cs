using AirportProject4.Project.core.Entities.main;
using AirportProject4.Shared.Specification;

namespace AirportProject4.Project.core.NewFolder.InterfaceContrect
{
    public interface IFlightRepo:IRepo<Flight>
    {

        public Task<IEnumerable<Flight>> Getupcomingflights(ISpecification<Flight> spec);
        public Task<IEnumerable<Flight?>> GetFlights_ByFlightNumber(ISpecification<Flight> spec);




    }
}
