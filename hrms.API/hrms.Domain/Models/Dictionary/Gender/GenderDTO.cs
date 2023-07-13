namespace hrms.Domain.Models.Dictionary.Gender
{
    public class GenderDTO
    {
        public int? Id { get; set; }

        public string Value { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool? IsActive { get; set; }
    }
}
