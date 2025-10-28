using AirportProject4.Project.core.Entities.main;
using AirportProject4.Project.core.NewFolder.InterfaceContrect;
using AirportProject4.Project.Repo.Data.Context;
using AirportProject4.Shared.Specification;
using Microsoft.EntityFrameworkCore;

namespace AirportProject4.Project.Repo.Repositories
{
    public class FlyightRepo : Repository<Flight>, IFlightRepo
    {
        public FlyightRepo(AirlineDbContext dbContext):base(dbContext)
        {
            
        }

        public async Task<IEnumerable<Flight?>> GetFlights_ByFlightNumber(ISpecification<Flight> spec)
        {
            return await specificationEvaluator<Flight>.GetQuery(_dbContext.Set<Flight>(), spec).ToListAsync();
        }

        public async Task<IEnumerable<Flight>> Getupcomingflights(ISpecification<Flight> spec)
        {
            var Flights = specificationEvaluator<Flight>.GetQuery(_dbContext.Set<Flight>(), spec);
            return await Flights.ToListAsync();

        }

        
    }
}
