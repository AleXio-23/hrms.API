namespace hrms.Shared.Models
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; } = false;
        public bool ErrorOccured { get; set; } = false;
        public string? ErrorMessage { get; set; } = null;
        public T? Data { get; set; } = default;
    }
}
