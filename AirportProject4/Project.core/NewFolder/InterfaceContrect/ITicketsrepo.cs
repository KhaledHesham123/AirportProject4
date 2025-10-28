using AirportProject4.Project.core.Entities.main;
using AirportProject4.Shared.Specification;

namespace AirportProject4.Project.core.NewFolder.InterfaceContrect
{
    public interface ITicketsrepo : IRepo<Ticket>
    {
        Task<int> TicketsCount(int id);

    }
}
