namespace NumberSortingApi.Services;

public interface IFileService
{
    Task WriteToFileAsync(IList<int> numbers);
    Task<IList<int>> ReadFromFileAsync();
}