namespace CV_Manager.Application.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string? message)
            : base(message)
        {
            
        }
    }
}
