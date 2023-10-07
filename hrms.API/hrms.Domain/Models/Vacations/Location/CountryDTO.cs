namespace hrms.Domain.Models.Vacations.Location
{
    public class CountryDTO
    {
        public int? Id { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public bool HasStates { get; set; }

        public int SortIndex { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<CityDTO>? Cities { get; set; } = null;

        public virtual ICollection<StateDTO>? States { get; set; } = null;
    }
}
