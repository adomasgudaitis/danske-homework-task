namespace NumberSortingApi.Handlers;

public interface INumberSortingHandler
{
    Task HandleNumberSortingAsync(string body);
    Task HandleNumberSortingAsync(List<int> numbers);
}