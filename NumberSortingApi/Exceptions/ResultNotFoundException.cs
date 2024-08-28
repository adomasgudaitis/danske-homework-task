namespace NumberSortingApi.Exceptions;

public class ResultNotFoundException : Exception
{
    public ResultNotFoundException(string message) : base(message)
    {
    }
}