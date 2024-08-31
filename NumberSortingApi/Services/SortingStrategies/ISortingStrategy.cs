namespace NumberSortingApi.Services.SortingStrategies;

public interface ISortingStrategy
{
    string Name { get; }
    int[] Execute(int[] numbers);
}