using NumberSortingApi.Services;
using NumberSortingApi.Services.SortingStrategies;

namespace NumberSortingApi.Tests.Services;

public class SortingServiceTests
{
    [Theory]
    [MemberData(nameof(GetTestingData))]
    public void Sort_UsingVariousSortingStrategies_ReturnsSortedArray(int[] numbers, int[] expectedSortedNumbers,
        ISortingStrategy sortingStrategy)
    {
        // Arrange
        var sortingService = new SortingService();
        sortingService.SetStrategy(sortingStrategy);

        // Act
        var result = sortingService.Sort(numbers);

        // Assert
        Assert.Equal(expectedSortedNumbers, result);
    }

    [Fact]
    public void Sort_StrategyNotSet_ThrowsException()
    {
        // Arrange
        var sortingService = new SortingService();
        int[] numbers = [ 1, 2, 3, 4, 5, 6, 7, 8, 9 ];
        
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => sortingService.Sort(numbers));
    }

    public static IEnumerable<object[]> GetTestingData()
    {
        yield return [new[] { 5, 7, 1, 3, 9, 8 }, new[] { 1, 3, 5, 7, 8, 9 }, new BubbleSortStrategy()];
        yield return [new[] { 5, 4, 3, 2, 1 }, new[] { 1, 2, 3, 4, 5 }, new BubbleSortStrategy()];
        yield return [new[] { 1, 2, 3, 4, 5, 6 }, new[] { 1, 2, 3, 4, 5, 6 } , new BubbleSortStrategy()];
        yield return [Array.Empty<int>(), Array.Empty<int>(), new BubbleSortStrategy()];
        
        yield return [new[] { 5, 7, 1, 3, 9, 8 }, new[] { 1, 3, 5, 7, 8, 9 }, new SelectionSortStrategy()];
        yield return [new[] { 5, 4, 3, 2, 1 }, new[] { 1, 2, 3, 4, 5 }, new SelectionSortStrategy()];
        yield return [new[] { 1, 2, 3, 4, 5, 6 }, new[] { 1, 2, 3, 4, 5, 6 } , new SelectionSortStrategy()];
        yield return [Array.Empty<int>(), Array.Empty<int>(), new SelectionSortStrategy()];
        
        yield return [new[] { 5, 7, 1, 3, 9, 8 }, new[] { 1, 3, 5, 7, 8, 9 }, new MergeSortStrategy()];
        yield return [new[] { 5, 4, 3, 2, 1 }, new[] { 1, 2, 3, 4, 5 }, new MergeSortStrategy()];
        yield return [new[] { 1, 2, 3, 4, 5, 6 }, new[] { 1, 2, 3, 4, 5, 6 } , new MergeSortStrategy()];
        yield return [Array.Empty<int>(), Array.Empty<int>(), new MergeSortStrategy()];
        
        yield return [new[] { 5, 7, 1, 3, 9, 8 }, new[] { 1, 3, 5, 7, 8, 9 }, new QuickSortStrategy()];
        yield return [new[] { 5, 4, 3, 2, 1 }, new[] { 1, 2, 3, 4, 5 }, new QuickSortStrategy()];
        yield return [new[] { 1, 2, 3, 4, 5, 6 }, new[] { 1, 2, 3, 4, 5, 6 } , new QuickSortStrategy()];
        yield return [Array.Empty<int>(), Array.Empty<int>(), new QuickSortStrategy()];
    }
}