namespace NumberSortingApi.Services.SortingStrategies;

public class QuickSortStrategy : ISortingStrategy
{
    public string Name => "QuickSort";

    public int[] Execute(int[] numbers)
    {
        var n = numbers.Length;

        QuickSort(numbers, 0, n - 1);

        return numbers;
    }

    // A utility function to swap two elements
    private static void Swap(int[] arr, int i, int j)
    {
        (arr[i], arr[j]) = (arr[j], arr[i]);
    }

    // This function takes last element as pivot,
    // places the pivot element at its correct position
    // in sorted array, and places all smaller to left
    // of pivot and all greater elements to right of pivot
    private static int Partition(int[] arr, int low, int high)
    {
        // Choosing the pivot
        var pivot = arr[high];

        // Index of smaller element and indicates
        // the right position of pivot found so far
        var i = low - 1;

        for (var j = low; j <= high - 1; j++)
        {
            // If current element is smaller than the pivot
            if (arr[j] < pivot)
            {
                // Increment index of smaller element
                i++;
                Swap(arr, i, j);
            }
        }

        Swap(arr, i + 1, high);

        return i + 1;
    }

    // The main function that implements QuickSort
    // arr[] --> Array to be sorted,
    // low --> Starting index,
    // high --> Ending index
    private static void QuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            // pi is partitioning index, arr[p]
            // is now at right place
            var pi = Partition(arr, low, high);

            // Separately sort elements before
            // and after partition index
            QuickSort(arr, low, pi - 1);
            QuickSort(arr, pi + 1, high);
        }
    }
}