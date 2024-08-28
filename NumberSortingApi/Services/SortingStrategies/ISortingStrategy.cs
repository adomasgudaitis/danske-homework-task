namespace NumberSortingApi.Services.SortingStrategies;

public interface ISortingStrategy
{
    string Name { get; }
    IList<int> Execute(IList<int> numbers);
}