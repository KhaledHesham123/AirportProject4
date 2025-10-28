using AirportProject4.Project.core.Entities.main;
using AirportProject4.Project.core.NewFolder.InterfaceContrect;
using MediatR;

namespace AirportProject4.CQRS.Tickets.Commends
{
    public record AddTicketCommend(Ticket Ticket):IRequest<bool>;

    public class addTicketCommendHandler : IRequestHandler<AddTicketCommend, bool>
    {
        private readonly IRepo<Ticket> ticketrepository;
        public addTicketCommendHandler(IRepo<Ticket> Ticketrepository)
        {
            ticketrepository = Ticketrepository;
        }
        public async Task<bool> Handle(AddTicketCommend request, CancellationToken cancellationToken)
        {
            if (request.Ticket == null)
            {
                throw new Exception("error while adding Ticket");
            }
            try
            {
                await ticketrepository.addAsync(request.Ticket);

                await ticketrepository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // ممكن تسجل الخطأ هنا لو عندك logging
                Console.WriteLine($"Error while saving Ticket: {ex.Message}");
                return false;
            }
        }
    }


}
