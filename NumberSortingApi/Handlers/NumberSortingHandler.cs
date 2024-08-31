using NumberSortingApi.Services;
using NumberSortingApi.Services.SortingStrategies;

namespace NumberSortingApi.Handlers;

public class NumberSortingHandler : INumberSortingHandler
{
    private readonly IFileService _fileService;
    private readonly ISortingService _sortingService;
    private readonly IConfiguration _configuration;

    public NumberSortingHandler(IFileService fileService, ISortingService sortingService, IConfiguration configuration)
    {
        _sortingService = sortingService;
        _configuration = configuration;
        _fileService = fileService;
    }
    
    public async Task HandleNumberSorting(IList<int> numbers)
    {
        ResolveSortingStrategy();
        var sortedNumbers = _sortingService.Sort(numbers);

        await _fileService.WriteAsync(sortedNumbers);
    }

    private void ResolveSortingStrategy()
    {
        var sortingMethod = _configuration["SortingMethod"] ?? "Default";
        ISortingStrategy sortingStrategy = sortingMethod switch
        {
            Strategies.SelectionSort => new SelectionSortStrategy(),
            Strategies.BubbleSort => new BubbleSortStrategy(),
            Strategies.QuickSort => new QuickSortStrategy(),
            Strategies.MergeSort => new MergeSortStrategy(),
            _ => new SelectionSortStrategy()
        };

        _sortingService.SetStrategy(sortingStrategy);
    }
}