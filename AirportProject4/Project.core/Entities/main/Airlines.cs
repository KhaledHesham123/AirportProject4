namespace AirportProject4.Project.core.Entities.main
{
    public class Airlines:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;


        public ICollection<Aircraft> Aircraft { get; set; } = new HashSet<Aircraft>();

    }
}
