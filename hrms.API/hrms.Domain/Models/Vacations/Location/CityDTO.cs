namespace hrms.Domain.Models.Vacations.Location
{
    public class CityDTO
    {
        public int Id { get; set; }

        public int? CountryId { get; set; }

        public int? StateId { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public int SortIndex { get; set; }

        public bool? IsActive { get; set; }
    }
}
