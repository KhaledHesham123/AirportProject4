using AirportProject4.Project.core.Entities.main;
using AirportProject4.Project.core.NewFolder.InterfaceContrect;
using AirportProject4.Project.Repo.Data.Context;
using AirportProject4.Shared.Specification;
using Microsoft.EntityFrameworkCore;

namespace AirportProject4.Project.Repo.Repositories
{
    public class SeatsRepo : Repository<Seat>, ISetsRepo
    {
        public SeatsRepo(AirlineDbContext dbContext):base(dbContext) 
        {
            
        }
        public async Task<int> SeatsCount(int flightId)
        {
            return await _dbContext.Set<Seat>()
        .CountAsync(s => s.FlightId == flightId);

        }
    }
}
