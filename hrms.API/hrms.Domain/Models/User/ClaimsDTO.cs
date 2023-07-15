namespace hrms.Domain.Models.User
{
    public class ClaimsDTO
    {
        public int? Id { get; set; }

        public int? ParentId { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool IsActive { get; set; }

        public int? SortIndex { get; set; }
    }
}
