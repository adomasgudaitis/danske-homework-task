using Microsoft.Extensions.Configuration;
using Moq;
using NumberSortingApi.Handlers;
using NumberSortingApi.Services;
using NumberSortingApi.Services.SortingStrategies;

namespace NumberSortingApi.Tests.Handlers;

public class NumberSortingHandlerTests
{
    private Mock<IFileService> _mockFileService;
    private Mock<ISortingService> _mockSortingService;
    private Mock<IConfiguration> _mockConfiguration;

    public NumberSortingHandlerTests()
    {
        _mockFileService = new Mock<IFileService>();
        _mockSortingService = new Mock<ISortingService>();
        _mockConfiguration = new Mock<IConfiguration>();
    }

    [Fact]
    public async Task HandleNumberSorting_SetSortingStrategyCorrectly()
    {
        // Arrange
        var numbers = new[] { 1, 2, 3, 4, 5 };
        _mockConfiguration.Setup(config => config["SortingMethod"]).Returns(Strategies.BubbleSort);
        var handler = new NumberSortingHandler(_mockFileService.Object, _mockSortingService.Object,
            _mockConfiguration.Object);

        // Act
        await handler.HandleNumberSorting(numbers);
        
        // Assert
        _mockSortingService.Verify(
            service => service.SetStrategy(It.Is<ISortingStrategy>(x => x is BubbleSortStrategy)), Times.Once);
    }

    [Fact]
    public async Task HandleNumberSorting_CallsSortingServiceAndFileServiceCorrectly()
    {
        // Arrange
        var unsortedNumbers = new[] { 4, 5, 1, 7, 3, 9 };
        var sortedNumbers = new[] { 1, 3, 4, 5, 7, 9 };
        _mockSortingService.Setup(service => service.Sort(unsortedNumbers)).Returns(sortedNumbers);
        var handler = new NumberSortingHandler(_mockFileService.Object, _mockSortingService.Object,
            _mockConfiguration.Object);

        // Act
        await handler.HandleNumberSorting(unsortedNumbers);
        
        // Assert
        _mockSortingService.Verify(service => service.Sort(unsortedNumbers), Times.Once);
        _mockFileService.Verify(service => service.WriteAsync(sortedNumbers), Times.Once);
    }
}