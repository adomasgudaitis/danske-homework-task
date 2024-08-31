namespace NumberSortingApi.Services;

public class FileService : IFileService
{
    private readonly string _directoryPath;
    private readonly string _fileName;

    public FileService(IConfiguration configuration)
    {
        var directoryName = configuration["Result:DirectoryName"];
        var fileName = configuration["Result:FileName"];

        if (string.IsNullOrWhiteSpace(directoryName) || string.IsNullOrWhiteSpace(fileName))
        {
            throw new KeyNotFoundException("Result:DirectoryName and Result:FileName are required.");
        }

        _directoryPath = Path.Combine(AppContext.BaseDirectory, directoryName);
        _fileName = fileName;
    }

    public async Task WriteAsync(IList<int> numbers)
    {
        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }

        await File.WriteAllTextAsync(Path.Combine(_directoryPath, _fileName), string.Join(' ', numbers));
    }

    public async Task<string> ReadAsync()
    {
        var filePath = Path.Combine(_directoryPath, _fileName);
        if (!File.Exists(filePath))
        {
            return string.Empty;
        }

        return await File.ReadAllTextAsync(Path.Combine(_directoryPath, _fileName));
    }
}