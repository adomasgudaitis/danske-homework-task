namespace NumberSortingApi.Services.SortingStrategies;

public class SelectionSortStrategy : ISortingStrategy
{
    public string Name => "SelectionSort";

    public int[] Execute(int[] numbers)
    {
        var n = numbers.Length;

        for (var i = 0; i < n - 1; i++)
        {
            var minIdx = i;
            for (var j = i + 1; j < n; j++)
            {
                if (numbers[j] < numbers[minIdx])
                {
                    minIdx = j;
                }
            }
            (numbers[minIdx], numbers[i]) = (numbers[i], numbers[minIdx]);
        }

        return numbers;
    }
}