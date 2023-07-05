namespace hrms.Shared.Exceptions
{
    public class RecordExistsException:Exception
    {
        public RecordExistsException(string message) : base(message)
        {
        }

        public RecordExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
