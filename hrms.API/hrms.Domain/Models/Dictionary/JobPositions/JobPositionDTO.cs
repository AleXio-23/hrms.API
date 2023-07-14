namespace hrms.Domain.Models.Dictionary.JobPositions
{
    public class JobPositionDTO
    {
        public int? Id { get; set; }

        public string? Name { get; set; } = null!;

        public bool? IsActive { get; set; }
    }
}
