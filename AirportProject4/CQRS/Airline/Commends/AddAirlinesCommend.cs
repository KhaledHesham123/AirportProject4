using AirportProject4.Project.core.DTOS.AirlineDto;
using AirportProject4.Project.core.Entities.main;
using MediatR;

namespace AirportProject4.CQRS.Airline.Commends
{
    public record AddAirlinesCommend(AirlineCreateDto AirlineCreateDto):IRequest<Airlines>;

    public class AddAirlinesCommendHandler : IRequestHandler<AddAirlinesCommend, Airlines>
    {
        public Task<Airlines> Handle(AddAirlinesCommend request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
