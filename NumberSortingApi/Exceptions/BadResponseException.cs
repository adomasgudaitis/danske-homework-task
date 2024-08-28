namespace NumberSortingApi.Exceptions;

public class BadResponseException : Exception
{
    public BadResponseException(string message) : base(message)
    {
    }
}