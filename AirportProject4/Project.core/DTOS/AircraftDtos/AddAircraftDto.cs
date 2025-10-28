namespace AirportProject4.Project.core.DTOS.AircraftDtos
{
    public class AddAircraftDto
    {
        public int Id { get; set; }
        public string Model { get; set; } = string.Empty;
        public int Capacity { get; set; }

        public int AirLineId { get; set; }
    }
}
