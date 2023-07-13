namespace hrms.Domain.Models.Dictionary.JobPositions
{
    public class JobPositionFilter
    {
        public int? Id { get; set; } = null!;

        public string? Name { get; set; } = null!;

        public bool? IsActive { get; set; } = null!;
    }
}
