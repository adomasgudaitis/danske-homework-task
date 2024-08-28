using NumberSortingApi.Services;
using NumberSortingApi.Services.SortingStrategies;
using NumberSortingApi.Utils;
using NumberSortingApi.Validation;

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

    public async Task HandleNumberSortingAsync(string body)
    {
        var numbers = SortingRequestBodyParser.Parse(body);
        SortingRequestValidator.Validate(numbers);

        ResolveSortingStrategy();
        var sortedNumbers = _sortingService.Sort(numbers);

        await _fileService.WriteToFileAsync(sortedNumbers);
    }
    
    public async Task HandleNumberSortingAsync(List<int> numbers)
    {
        ResolveSortingStrategy();
        var sortedNumbers = _sortingService.Sort(numbers);

        await _fileService.WriteToFileAsync(sortedNumbers);
    }

    private void ResolveSortingStrategy()
    {
        var sortingMethod = _configuration.GetValue<string>("SortingMethod") ?? "Default";
        ISortingStrategy sortingStrategy = sortingMethod switch
        {
            "SelectionSort" => new SelectionSortStrategy(),
            "BubbleSort" => new BubbleSortStrategy(),
            _ => new SelectionSortStrategy()
        };

        _sortingService.SetStrategy(sortingStrategy);
    }
}