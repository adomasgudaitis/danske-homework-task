using NumberSortingApi.Services.SortingStrategies;

namespace NumberSortingApi.Services;

public interface ISortingService
{
    IList<int> Sort(IList<int> numbers);
    void SetStrategy(ISortingStrategy strategy);
}