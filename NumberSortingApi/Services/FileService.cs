using NumberSortingApi.Exceptions;

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

        _directoryPath = Directory
            .CreateDirectory(Path.Combine(AppContext.BaseDirectory,
                directoryName))
            .FullName;
        _fileName = fileName;
    }
    
    public async Task WriteToFileAsync(IList<int> numbers)
    {
        try
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            await using var writer = new StreamWriter(Path.Combine(_directoryPath, _fileName));
            foreach (var number in numbers)
            {
                await writer.WriteLineAsync(number.ToString());
            }
        }
        catch (Exception e)
        {
            throw new FileProcessingException(e.Message);
        }
    }

    public async Task<IList<int>> ReadFromFileAsync()
    {
        var numbers = new List<int>();
        var filePath = Path.Combine(_directoryPath, _fileName);
        if (!File.Exists(filePath))
        {
            return numbers;
        }

        try
        {
            using var reader = new StreamReader(Path.Combine(_directoryPath, _fileName));
            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                numbers.Add(int.Parse(line));
            }

            return numbers;
        }
        catch (Exception e)
        {
            throw new FileProcessingException(e.Message);
        }
    }
}