using NumberSortingApi.Services;
using NumberSortingApi.Services.SortingStrategies;

namespace NumberSortingApi.Tests.Services;

public class SortingServiceTests
{
    [Theory]
    [InlineData(new[] { 5, 7, 1, 3, 9, 8 }, new[] { 1, 3, 5, 7, 8, 9 })]
    [InlineData(new[] { 5, 4, 3, 2, 1 }, new[] { 1, 2, 3, 4, 5 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 1, 2, 3, 4, 5, 6 })]
    [InlineData( new int[] {}, new int[] {})]
    public void Sort_UsingSelectionSort_ReturnsSortedArray(int[] numbers, int[] expectedSortedNumbers)
    {
        // Arrange
        var sortingService = new SortingService();
        var sortingStrategy = new SelectionSortStrategy();
        sortingService.SetStrategy(sortingStrategy);
        
        // Act
        var result = sortingService.Sort(numbers);
        
        // Assert
        Assert.Equal(expectedSortedNumbers, result);
    }
    
    [Theory]
    [InlineData(new[] { 5, 7, 1, 3, 9, 8 }, new[] { 1, 3, 5, 7, 8, 9 })]
    [InlineData(new[] { 5, 4, 3, 2, 1 }, new[] { 1, 2, 3, 4, 5 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 1, 2, 3, 4, 5, 6 })]
    [InlineData( new int[] {}, new int[] {})]
    public void Sort_UsingBubbleSort_ReturnsSortedArray(int[] numbers, int[] expectedSortedNumbers)
    {
        // Arrange
        var sortingService = new SortingService();
        var sortingStrategy = new BubbleSortStrategy();
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
}