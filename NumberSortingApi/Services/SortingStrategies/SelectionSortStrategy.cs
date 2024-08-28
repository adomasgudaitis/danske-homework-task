
namespace NumberSortingApi.Services.SortingStrategies;

public class SelectionSortStrategy : ISortingStrategy
{
    public string Name => "SelectionSort";

    public IList<int> Execute(IList<int> numbers)
    {
        var n = numbers.Count;
        var numbersArray = numbers.ToArray();

        for (var i = 0; i < n - 1; i++)
        {
            var minIdx = i;
            for (var j = i + 1; j < n; j++)
            {
                if (numbersArray[j] < numbersArray[minIdx])
                {
                    minIdx = j;
                }
            }
            (numbersArray[minIdx], numbersArray[i]) = (numbersArray[i], numbersArray[minIdx]);
        }

        return numbersArray;
    }
}