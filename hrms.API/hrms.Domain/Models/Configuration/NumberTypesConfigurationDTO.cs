namespace hrms.Domain.Models.Configuration
{
    public class NumberTypesConfigurationDTO
    {
        public int Id { get; set; }

        public string ConfigName { get; set; } = null!;

        public int Value { get; set; }

        public bool? IsActive { get; set; }
    }
}
