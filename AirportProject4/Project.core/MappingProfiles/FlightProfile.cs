using AutoMapper;
using AirportProject4.Project.core.DTOS.FlightsDto;
using AirportProject4.Project.core.DTOS.TicktDto;
using AirportProject4.Project.core.DTOS.ViewModles;
using AirportProject4.Project.core.Entities.main;

namespace AirportProject4.Project.core.MappingProfiles
{
    public class FlightProfile:Profile
    {
        public FlightProfile()
        {
            CreateMap<Flight, UpdateFlightviewmodle>()
          .ForMember(dest => dest.AirCraftModel, opt => opt.MapFrom(src => src.Aircraft.Model))
          .ForMember(dest => dest.AircraftCapacity, opt => opt.MapFrom(src => src.Aircraft.Capacity));

            CreateMap<UpdateFlightviewmodle, FlightUpdateDto>();


            //CreateMap<Flight, FlightBookviewmodle>()
            //   .ForMember(dest => dest.FlightId, opt => opt.MapFrom(src => src.Id))
            //   .ForMember(dest => dest.FlightNumber, opt => opt.MapFrom(src => src.FlightNumber))
            //   // القيم التالية غير موجودة في الـ Flight فهنسيبها زي ما هي Default
            //   .ForMember(dest => dest.SeatType, opt=>opt.MapFrom(src=>src.Seats.clas)
            //   .ForMember(dest => dest.PassengerFullName, opt => opt.Ignore())
            //   .ForMember(dest => dest.PassengerPassportNumber, opt => opt.Ignore());


        }
    }
}
