namespace NumberSortingApi.Services.SortingStrategies;

public class BubbleSortStrategy : ISortingStrategy
{
    public string Name => "BubbleSort";

    public IList<int> Execute(IList<int> numbers)
    {
        var n = numbers.Count;
        var numbersArr = numbers.ToArray();
        bool swapped;

        for (var i = 0; i < n - 1; i++)
        {
            swapped = false;
            for (var j = 0; j < n - i - 1; j++)
            {
                if (numbersArr[j] > numbersArr[j + 1])
                {
                    (numbersArr[j], numbersArr[j + 1]) = (numbersArr[j + 1], numbersArr[j]);
                    swapped = true;
                }
            }

            if (!swapped) break;
        }

        return numbersArr;
    }
}