namespace Tech.Challenge4.Domain.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string? message) : base(message)
        {
        }
    }
}
