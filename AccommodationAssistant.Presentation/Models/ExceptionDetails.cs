namespace AccommodationAssistant.Presentation.Models
{
    public class ExceptionDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string? StackTrace { get; set; }
    }
}
