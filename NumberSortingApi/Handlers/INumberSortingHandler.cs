namespace NumberSortingApi.Handlers;

public interface INumberSortingHandler
{
    Task HandleNumberSorting(IList<int> numbers);
}