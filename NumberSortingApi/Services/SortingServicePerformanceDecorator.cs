using System.Diagnostics;
using NumberSortingApi.Services.SortingStrategies;

namespace NumberSortingApi.Services;

public class SortingServicePerformanceDecorator : ISortingService
{
    private readonly ISortingService _sortingService;
    private string _strategyName = string.Empty;

    public SortingServicePerformanceDecorator(ISortingService sortingService)
    {
        _sortingService = sortingService;
    }

    public IList<int> Sort(IList<int> numbers)
    {
        Console.WriteLine($"Starting sorting for {_strategyName}");
        var stopwatch = Stopwatch.StartNew();
        var sortedNumbers = _sortingService.Sort(numbers);
        stopwatch.Stop();
        var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        
        Console.WriteLine($"Sorting strategy: {_strategyName}: Sorting took {elapsedMilliseconds} milliseconds.");

        return sortedNumbers;
    }

    public void SetStrategy(ISortingStrategy strategy)
    {
        _sortingService.SetStrategy(strategy);
        _strategyName = strategy.Name;
    }
}