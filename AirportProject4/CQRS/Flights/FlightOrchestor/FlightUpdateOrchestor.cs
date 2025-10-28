using AirportProject4.CQRS.Aircrafts.Commends;
using AirportProject4.CQRS.Airline.Commends;
using AirportProject4.CQRS.CQRSContracts;
using AirportProject4.CQRS.Flights.Commends;
using AirportProject4.CQRS.Flights.Quries;
using AirportProject4.Project.core;
using AirportProject4.Project.core.DTOS.AircraftDtos;
using AirportProject4.Project.core.DTOS.AirlineDto;
using AirportProject4.Project.core.DTOS.FlightsDto;
using AirportProject4.Project.core.DTOS.ViewModles;
using AirportProject4.Project.core.Entities.main;
using AirportProject4.Shared.Specification;
using MediatR;

namespace AirportProject4.CQRS.Flights.FlightOrchestor
{
    public class FlightUpdateOrchestor: IUpdateFlightsCommandOrchestrator
    {
        private readonly IMediator _mediator;
        private readonly IunitofWork _unitofWork;

        public FlightUpdateOrchestor(IMediator mediator ,IunitofWork unitofWork)
        {
            this._mediator = mediator;
            this._unitofWork = unitofWork;
        }

        public async Task<Flight> UpdateFlightsAsync(int? id , UpdateFlightviewmodle flightUpdateDto) 
        {
            var existingFlight = await _mediator.Send(new GetFlightbyidQyery(id));
            if (existingFlight == null) return null;

            var flightDto = new FlightUpdateDto
            {
                Id = existingFlight.Id,
                FlightNumber = string.IsNullOrWhiteSpace(flightUpdateDto.FlightNumber)
                ? existingFlight.FlightNumber
                : flightUpdateDto.FlightNumber,

                DepartureAirport = string.IsNullOrWhiteSpace(flightUpdateDto.DepartureAirport)
                ? existingFlight.DepartureAirport
                : flightUpdateDto.DepartureAirport,

                ArrivalAirport = string.IsNullOrWhiteSpace(flightUpdateDto.ArrivalAirport)
                ? existingFlight.ArrivalAirport
                : flightUpdateDto.ArrivalAirport,

                DepartureTime = flightUpdateDto.DepartureTime ?? existingFlight.DepartureTime,
                ArrivalTime = flightUpdateDto.ArrivalTime ?? existingFlight.ArrivalTime,
                AircraftId = existingFlight.AircraftId
            };

            var flightUpdated = await _mediator.Send(new FlightUpdate( flightDto));
            if (!flightUpdated)
                throw new Exception("Failed to update Flight.");

            if (flightUpdateDto.AirCraftModel != null || flightUpdateDto.AircraftCapacity.HasValue)
            {
                var aircraftDto = new AddAircraftDto
                {
                    Id = existingFlight.Aircraft.Id,
                    Model = flightUpdateDto.AirCraftModel ?? existingFlight.Aircraft.Model,
                    Capacity = flightUpdateDto.AircraftCapacity ?? existingFlight.Aircraft.Capacity,
                    AirLineId = existingFlight.Aircraft.AirLineId
                };

                var aircraftUpdated = await _mediator.Send(new UpdateAircraftQuery(aircraftDto));
                if (!aircraftUpdated)
                    throw new Exception("Failed to update Aircraft.");
            }
            if (flightUpdateDto.AirlineCode != null || flightUpdateDto.AirlineName != null) 
            {
               var AirllineDto = new UpdateAirlineDto
               {
                   Id = existingFlight.Aircraft.AirLineId,
                   Name = flightUpdateDto.AirlineName ?? existingFlight.Aircraft.Airline.Name,
                   Code = flightUpdateDto.AirlineCode ?? existingFlight.Aircraft.Airline.Code
               };
                var AirlineUpdated = await _mediator.Send(new UpdateAirlinesCommend(AirllineDto));
                if (!AirlineUpdated)
                    throw new Exception("Failed to update Airline.");
            }

            return existingFlight;
        }
    }
}
