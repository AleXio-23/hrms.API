namespace hrms.Domain.Models.Dictionary.Locations
{
    public class CountryFilter
    {
        public string? Code { get; set; }

        public string? Name { get; set; }

        public bool? HasStates { get; set; } 

        public bool? IsActive { get; set; }
    }
}
