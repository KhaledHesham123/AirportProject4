using AirportProject4.Project.core.DTOS.AirlineDto;
using AirportProject4.Project.core.Entities.main;
using AirportProject4.Project.core.NewFolder.InterfaceContrect;
using AirportProject4.Project.Repo.Repositories;
using MediatR;
using NuGet.Protocol.Core.Types;

namespace AirportProject4.CQRS.Airline.Commends
{
    public record UpdateAirlinesCommend(UpdateAirlineDto AirlineCreateDto) :IRequest<bool>;

    public class UpdateAirlinesCommendHandler : IRequestHandler<UpdateAirlinesCommend, bool>
    {
        private readonly IRepo<Airlines> _repository;

        public UpdateAirlinesCommendHandler(IRepo<Airlines> repo )
        {
            this._repository = repo;
        }
        public async Task<bool> Handle(UpdateAirlinesCommend request, CancellationToken cancellationToken)
        {
            if (request.AirlineCreateDto == null)
            {
                throw new Exception("error while updateing Airlins");
            }

            var airline = new Airlines
            {
                Id = request.AirlineCreateDto.Id,
                Code = request.AirlineCreateDto.Code,
                Name = request.AirlineCreateDto.Name

            };

            try
            {
                _repository.SaveInclude(airline);

                await _repository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // ممكن تسجل الخطأ هنا لو عندك logging
                Console.WriteLine($"Error while saving airline: {ex.Message}");
                return false;
            }


        }
    }




}
