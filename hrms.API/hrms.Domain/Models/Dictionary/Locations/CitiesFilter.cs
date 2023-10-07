namespace hrms.Domain.Models.Dictionary.Locations
{
    public class CitiesFilter
    {
        public int? CountryId { get; set; }

        public int? StateId { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; } 

        public bool? IsActive { get; set; }
    }
}
