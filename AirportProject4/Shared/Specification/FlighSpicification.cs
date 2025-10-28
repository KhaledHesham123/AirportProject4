using AirportProject4.Project.core.Entities.main;
using Microsoft.EntityFrameworkCore;

namespace AirportProject4.Shared.Specification
{
    public class FlighSpicification:BaseSpecification<Flight>
    {
        public FlighSpicification():base()
        {
            addIncludes();
        }

        public FlighSpicification(DateTime dateTime):base(f => f.DepartureTime >= dateTime)
        {
            addIncludes();


        }
        public FlighSpicification(int? id) : base(f => f.Id == id)
        {
            
            addIncludes();

            
        }
        public FlighSpicification(string FlightNumber) : base(f => f.FlightNumber.ToLower() == FlightNumber.ToLower())
        {
            
            addIncludes();

            
        }
        public void addIncludes() 
        {
            _includes.Add(f => f.Aircraft);
            //_includes.Add(f => f.DepartureAirport);
            //_includes.Add(f => f.ArrivalAirport);
            _includes.Add(f => f.Seats);
            _includes.Add(f => f.Tickets);
            _Thenincludes.Add(q => q.Include(f => f.Aircraft)
                                 .ThenInclude(a => a.Airline));
            _Thenincludes.Add(q => q.Include(f => f.Tickets)
                                 .ThenInclude(t => t.User));
        }
    }
}
