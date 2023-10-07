namespace hrms.Domain.Models.Vacations.Location
{
    public class StateDTO
    {
        public int? Id { get; set; }

        public int? CountryId { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public int SortIndex { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<CityDTO>? Cities { get; set; } = null;
    }
}
