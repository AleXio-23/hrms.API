namespace hrms.Domain.Models.Dictionary.Gender
{
    public class GenderDTO
    {
        public int? Id { get; set; }

        public string Name { get; set; } = null!;

        public bool? IsActive { get; set; }
    }
}
