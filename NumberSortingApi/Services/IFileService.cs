namespace NumberSortingApi.Services;

public interface IFileService
{
    Task WriteAsync(IList<int> numbers);
    Task<string> ReadAsync();
}