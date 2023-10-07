namespace hrms.Domain.Models.Dictionary
{
    public class DefaultMultiLevelDictionaryResponse
    {
        public int? Id { get; set; } = null;
        public int? ParentId { get; set; } = null;

        public string? Code { get; set; } = null;

        public string? Name { get; set; } = null;

        public bool? HasNextLevel { get; set; } = null;
        public string? NextLevelChildName { get; set; } = null;

        public int? SortIndex { get; set; } = null;

        public bool? IsActive { get; set; } = null;

        public List<DefaultMultiLevelDictionaryResponse>? Children { get; set; } = null;
    }
}
