namespace AirportProject4.Project.core.DTOS.ViewModles
{
    public class UserviewModle
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public string passportNumber { get; set; }

        public string ImageUrl { get; set; }
        public IEnumerable<string>? Roles { get; set; }
    }
}
