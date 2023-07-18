namespace hrms.Shared.Models
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; } = false;
        public bool ErrorOccured { get; set; } = false;
        public string? ErrorMessage { get; set; } = null;
        public T? Data { get; set; } = default;


        public static ServiceResult<T> SuccessResult(T data)
        {
            return new ServiceResult<T>()
            {
                Success = true,
                ErrorOccured = false,
                Data = data
            };
        }

        public static ServiceResult<T> ErrorResult(string? error)
        {
            return new ServiceResult<T>()
            {
                Success = false,
                ErrorOccured = true,
                ErrorMessage = error ?? "Unexpected error occured"
            };
        }
    }
}
