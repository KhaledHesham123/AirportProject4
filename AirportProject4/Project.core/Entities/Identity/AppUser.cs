using AirportProject4.Project.core.Entities.main;
using Microsoft.AspNetCore.Identity;

namespace AirportProject4.Project.core.Entities.Identity
{
    public class AppUser : IdentityUser<int>
    {

        public string FullName { get; set; }
        public string PassportNumber { get; set; }

        public ICollection<Ticket>? Tickets { get; set; }

        public string? ImageUrl { get; set; }

    }
}
