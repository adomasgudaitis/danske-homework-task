namespace NumberSortingApi.Services.SortingStrategies;

public class BubbleSortStrategy : ISortingStrategy
{
    public string Name => "BubbleSort";

    public int[] Execute(int[] numbers)
    {
        var n = numbers.Length;
        bool swapped;

        for (var i = 0; i < n - 1; i++)
        {
            swapped = false;
            for (var j = 0; j < n - i - 1; j++)
            {
                if (numbers[j] > numbers[j + 1])
                {
                    (numbers[j], numbers[j + 1]) = (numbers[j + 1], numbers[j]);
                    swapped = true;
                }
            }

            if (!swapped) break;
        }

        return numbers;
    }
}