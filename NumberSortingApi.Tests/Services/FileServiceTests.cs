using Microsoft.Extensions.Configuration;
using Moq;
using NumberSortingApi.Services;

namespace NumberSortingApi.Tests.Services;

public class FileServiceTests : IDisposable
{
    private readonly Mock<IConfiguration> _configurationMock;
    private const string DirectoryName = "FilesTest";
    private const string FileName = "fileTest.txt";

    public FileServiceTests()
    {
        _configurationMock = new Mock<IConfiguration>();
        _configurationMock.Setup(config => config["Result:DirectoryName"]).Returns(DirectoryName);
        _configurationMock.Setup(config => config["Result:FileName"]).Returns(FileName);
    }
    
    public void Dispose()
    {
        if (Directory.Exists(DirectoryName))
        {
            Directory.Delete(DirectoryName, true);
        }
    }
    
    [Fact]
    public async Task WriteAsync_CreatesDirectoryAndAddFile()
    {
        // Arrange
        var numbers = new List<int> { 1, 2, 3 };
        
        var fileService = new FileService(_configurationMock.Object);

        // Act
        await fileService.WriteAsync(numbers);
        
        // Assert
        Assert.True(Directory.Exists(DirectoryName));
        Assert.True(File.Exists(Path.Combine(DirectoryName, FileName)));
        
        Directory.Delete(DirectoryName, true);
    }

    [Fact]
    public async Task WriteAsync_WritesNumbersToFile()
    {
        // Arrange
        var numbers = new List<int> { 1, 2, 3, 4, 5 };
        var fileService = new FileService(_configurationMock.Object);

        // Act
        await fileService.WriteAsync(numbers);
        
        // Assert
        var fileContent = await File.ReadAllTextAsync(Path.Combine(DirectoryName, FileName));

        Assert.Equal("1 2 3 4 5", fileContent);
    }

    [Fact]
    public async Task ReadAsync_ReadsNumbersFromFile()
    {
        // Arrange
        var numbers = "1 2 3 4 5";
        Directory.CreateDirectory(DirectoryName);
        await File.WriteAllTextAsync(Path.Combine(DirectoryName, FileName), numbers);
        
        var fileService = new FileService(_configurationMock.Object);

        // Act
        var result = await fileService.ReadAsync();
        
        // Assert
        Assert.Equal("1 2 3 4 5", result);
    }

    [Fact]
    public async Task ReadAsync_FileDoesNotExist_ReturnsEmptyList()
    {
        // Arrange
        var fileService = new FileService(_configurationMock.Object);
        
        // Act
        var numbers = await fileService.ReadAsync();
        
        // Assert
        Assert.Empty(numbers);
    }
}