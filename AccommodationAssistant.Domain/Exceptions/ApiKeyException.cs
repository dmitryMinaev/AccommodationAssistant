namespace AccommodationAssistant.Domain.Exceptions
{
    public class ApiKeyException : Exception
    {
        public ApiKeyException(string message)
            : base(message)
        {
        }

        public ApiKeyException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
