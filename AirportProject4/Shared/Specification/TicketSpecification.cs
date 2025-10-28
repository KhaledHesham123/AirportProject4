using AirportProject4.Project.core.Entities.main;

namespace AirportProject4.Shared.Specification
{
    public class TicketSpecification:BaseSpecification<Ticket>
    {
        public TicketSpecification(int? id):base(x=>x.Id==id)
        {

        }
        
            
        
    }
}
