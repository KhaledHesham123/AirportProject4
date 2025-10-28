using AirportProject4.Project.core.DTOS.AircraftDtos;
using AirportProject4.Project.core.Entities.main;
using AirportProject4.Project.core.NewFolder.InterfaceContrect;
using MediatR;

namespace AirportProject4.CQRS.Aircrafts.Commends
{
    public record UpdateAircraftQuery(AddAircraftDto AircraftDto):IRequest<bool>;

    public class UpdateAircraftQueryHandler : IRequestHandler<UpdateAircraftQuery, bool>
    {
        private readonly IRepo<Aircraft> repo;

        public UpdateAircraftQueryHandler(IRepo<Aircraft> repo )
        {
            this.repo = repo;
        }
        public async Task<bool> Handle(UpdateAircraftQuery request, CancellationToken cancellationToken)
        {
            var aircraft= new Aircraft
            {
                Id = request.AircraftDto.Id,
                Model = request.AircraftDto.Model,
                Capacity = request.AircraftDto.Capacity,
                AirLineId = request.AircraftDto.AirLineId
            };
            try
            {
                repo.SaveInclude(aircraft);
                await repo.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while saving airline: {ex.Message}");
                return false;
            }
        }
    }


}
