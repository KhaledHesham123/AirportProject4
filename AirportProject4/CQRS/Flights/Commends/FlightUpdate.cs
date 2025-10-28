using Azure.Core;
using AirportProject4.Project.core;
using AirportProject4.Project.core.DTOS.FlightsDto;
using AirportProject4.Project.core.Entities.main;
using AirportProject4.Project.core.NewFolder.InterfaceContrect;
using AirportProject4.Project.Repo;
using AirportProject4.Shared.Specification;
using Humanizer;
using MediatR;

namespace AirportProject4.CQRS.Flights.Commends
{
    public record FlightUpdate( FlightUpdateDto FlightUpdateDto) : IRequest<bool>; // لي مش dto عشان متبقاش معتمده علي نوع بقوله هيجيلك داتا ده حطها


    public class FlightUpdateHandler : IRequestHandler<FlightUpdate, bool>
    {
        private readonly IRepo<Flight> repo;

        public FlightUpdateHandler(IRepo<Flight> repo)
        {
            this.repo = repo;
        }

        public async Task<bool> Handle(FlightUpdate request, CancellationToken cancellationToken)
        {
            //var spec = new FlighSpicification(request.FlightDto.Id);
            //var flight = await _unitofWork.FlightRepo.GetidAsync(spec);


            if ( request.FlightUpdateDto == null) return false;
            var updatedFlight = new Flight
            {
                Id = request.FlightUpdateDto.Id ,
                FlightNumber = request.FlightUpdateDto.FlightNumber ,
                DepartureTime = request.FlightUpdateDto.DepartureTime ,
                ArrivalTime = request.FlightUpdateDto.ArrivalTime ,
                AircraftId = request.FlightUpdateDto.AircraftId ,
                DepartureAirport = request.FlightUpdateDto.DepartureAirport ,
                ArrivalAirport = request.FlightUpdateDto.ArrivalAirport,
                
                
            };

            repo.SaveInclude(updatedFlight);

            await repo.SaveChanges();

            return true;






        }
        #region MyRegion
        //// عدّل القيم
        //flight.DepartureTime = request.DepartureTime ?? flight.DepartureTime;
        //flight.ArrivalTime = request.ArrivalTime ?? flight.ArrivalTime;
        //flight.DepartureAirport = string.IsNullOrEmpty(request.DepartureAirport)
        //    ? flight.DepartureAirport
        //    : request.DepartureAirport;
        //flight.ArrivalAirport = string.IsNullOrEmpty(request.ArrivalAirport)
        //    ? flight.ArrivalAirport
        //    : request.ArrivalAirport;

        //if (flight.Aircraft != null)
        //{
        //    flight.Aircraft.Model = string.IsNullOrEmpty(request.AirCraftModel)
        //        ? flight.Aircraft.Model
        //        : request.AirCraftModel;

        //    flight.Aircraft.Capacity = request.AircraftCapacity ?? flight.Aircraft.Capacity;
        //} 
        #endregion
    }
}
