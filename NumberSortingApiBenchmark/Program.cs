using System.Diagnostics;
using NumberSortingApi.Services;
using NumberSortingApi.Services.SortingStrategies;
using NumberSortingApiBenchmark;

int[] sizes = [1000, 10000, 15000, 20000, 50000, 100000];
var sortingService = new SortingService();
var sortingAlgorithms = new List<ISortingStrategy>
{
    new BubbleSortStrategy(),
    new SelectionSortStrategy(),
    new QuickSortStrategy(),
    new MergeSortStrategy()
};

if (Directory.Exists("Results"))
{
    Directory.Delete("Results", true);
}
Directory.CreateDirectory("Results");

foreach (var sortingAlgorithm in sortingAlgorithms)
{
    sortingService.SetStrategy(sortingAlgorithm);
    var results = new List<SortingTimeResult>();
    foreach (var size in sizes)
    {
        var numbers = Utils.GenerateRandomListOfNumbers(size);

        var stopwatch = Stopwatch.StartNew();
        var sortedNumbers = sortingService.Sort(numbers);
        stopwatch.Stop();
        File.WriteAllText(Path.Combine("Results", $"result-{sortingAlgorithm.Name}-{size}.txt"),
            string.Join(' ', sortedNumbers));

        results.Add(new SortingTimeResult(size, stopwatch.ElapsedMilliseconds));
    }
    Utils.PrintTimeResults(sortingAlgorithm.Name, results);
}


