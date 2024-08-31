namespace NumberSortingApi.Services.SortingStrategies;

public class MergeSortStrategy : ISortingStrategy
{
    public string Name => "MergeSort";
    public int[] Execute(int[] numbers)
    {
        var n = numbers.Length;

        MergeSort(numbers, 0, n- 1);

        return numbers;
    }
    
    // Merges two subarrays of []arr.
    // First subarray is arr[l..m]
    // Second subarray is arr[m+1..r]
    static void Merge(int[] arr, int l, int m, int r)
    {
        // Find sizes of two
        // subarrays to be merged
        var n1 = m - l + 1;
        var n2 = r - m;

        // Create temp arrays
        var L = new int[n1];
        var R = new int[n2];
        int i, j;

        // Copy data to temp arrays
        for (i = 0; i < n1; ++i)
            L[i] = arr[l + i];
        for (j = 0; j < n2; ++j)
            R[j] = arr[m + 1 + j];

        // Merge the temp arrays

        // Initial indexes of first
        // and second subarrays
        i = 0;
        j = 0;

        // Initial index of merged
        // subarray array
        var k = l;
        while (i < n1 && j < n2) {
            if (L[i] <= R[j]) {
                arr[k] = L[i];
                i++;
            }
            else {
                arr[k] = R[j];
                j++;
            }
            k++;
        }

        // Copy remaining elements
        // of L[] if any
        while (i < n1) {
            arr[k] = L[i];
            i++;
            k++;
        }

        // Copy remaining elements
        // of R[] if any
        while (j < n2) {
            arr[k] = R[j];
            j++;
            k++;
        }
    }

    // Main function that
    // sorts arr[l..r] using
    // merge()
    static void MergeSort(int[] arr, int l, int r)
    {
        if (l < r) {

            // Find the middle point
            var m = l + (r - l) / 2;

            // Sort first and second halves
            MergeSort(arr, l, m);
            MergeSort(arr, m + 1, r);

            // Merge the sorted halves
            Merge(arr, l, m, r);
        }
    }
}