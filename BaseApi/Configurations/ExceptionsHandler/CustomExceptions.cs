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
        public List<string> Errors { get; }

        public ValidationException() : base("Validation failed")
        {
            Errors = new List<string>();
        }

        public ValidationException(IEnumerable<string> errors) : this()
        {
            Errors.AddRange(errors);
        }
    }

}
