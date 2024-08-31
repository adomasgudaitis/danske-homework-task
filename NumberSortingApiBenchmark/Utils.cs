using ConsoleTables;

namespace NumberSortingApiBenchmark;

public static class Utils
{
    public static int[] GenerateRandomListOfNumbers(int size)
    {
        const int minValue = 1;
        const int maxValue = 100000;
        var numbers = new int[size];
        var random = new Random();

        for (var i = 0; i < size; i++)
        {
            numbers[i] = random.Next(minValue, maxValue + 1);
        }

        return numbers;
    }

    public static void PrintTimeResults(string sortingAlgorithm, IEnumerable<SortingTimeResult> rows)
    {
        var table = new ConsoleTable("Size of Array (n)", "Elapsed Time (ms)");

        foreach (var row in rows)
        {
            table.AddRow(row.SizeOfArray, row.ElapsedMilliseconds);
        }
        
        Console.WriteLine($"{sortingAlgorithm}:");
        Console.WriteLine(table.ToMinimalString());
    }
}