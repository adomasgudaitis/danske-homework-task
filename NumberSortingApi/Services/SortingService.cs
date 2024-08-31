
using NumberSortingApi.Services.SortingStrategies;

namespace NumberSortingApi.Services;

public class SortingService : ISortingService
{
    private ISortingStrategy? _sortingStrategy;

    public void SetStrategy(ISortingStrategy strategy)
    {
        _sortingStrategy = strategy;
    }

    public IList<int> Sort(IList<int> numbers)
    {
        if (_sortingStrategy == null)
        {
            throw new InvalidOperationException("SortingStrategy is not set.");
        }

        return _sortingStrategy.Execute(numbers.ToArray());
    }
}