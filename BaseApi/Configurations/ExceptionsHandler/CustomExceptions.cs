namespace BaseApi.Configurations.ExceptionsHandler
{
    public class CustomExceptions
    {
    }

    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }

    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
    }

}
