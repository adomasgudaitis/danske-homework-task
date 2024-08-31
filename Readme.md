
# Danske Homework Assignment

This is the solution of the Danske Homework Assignment. Assignment:

We need Web API number ordering solution. This solution should have 2 endpoints:
- We can pass line of numbers from 1 - 10 (few can be skipped) and these numbers should be ordered and saved to file (for ex. result.txt). For ex. we pass 5 2 8 10 1, this file should be saved with following content 1 2 5 8 10
- We should be able to load content of latest saved file

Requirements:
- Latest .NET project
- Business service(s) with unit tests
- Sorting algorithm should be written yourself for ex. bubble sort (it would be nice if this algorithm would be able to handle any size of numbers not only 1 to 10). You can use AI tools if you want as help for this part, but sorting code should be in this project (not some library).
- Put source code in GIT
- Use the best software engineering practices
  Bonus points:
- Multiple sorting algorithms are used, and time performance is measured between them.

## Details
- .NET 8
- Minimal API
- Testing with xUnit 2.5.3

## Sorting Algorithms
Sorting algorithms used in the solution:
- Selection sort
- Bubble sort
- Merge sort
- Quick sort

### Selection Sort
The time complexity of Selection Sort is O(N^2). There is the table presenting performance measurements with various sizes of arrays:

| Array Size (N) | Elapsed Time (ms) |
|---------------:|-------------------|
|           1000 | less than 1       |
|          10000 | 69                |
|          15000 | 155               |
|          20000 | 276               |
|          50000 | 1714              |
|         100000 | 6882              |

### Bubble Sort

The time complexity of Bubble Sort is O(N^2). There is the table presenting performance measurements with various sizes of arrays:

| Array Size (N) | Elapsed Time (ms) |
|----------------|-------------------|
| 1000           | 2                 |
| 10000          | 157               |
| 15000          | 371               |
| 20000          | 677               |
| 50000          | 5070              |
| 100000         | 22484             |

### Quick Sort

The time complexity of Quick Sort is O(N*logN) in the average and the best cases, and O(N^2) in the worst case. There is the table presenting performance measurements with various sizes of arrays:

| Size of Array (n) | Elapsed Time (ms) |
|-------------------|-------------------|
| 1000              | less than 1       |
| 10000             | 1                 |
| 15000             | 1                 |
| 20000             | 1                 |
| 50000             | 5                 |
| 100000            | 11                |

### Merge Sort

The time complexity of Merge Sort is O(N*logN). There is the table presenting performance measurements with various sizes of arrays:

| Size of Array (n) | Elapsed Time (ms) |
|-------------------|-------------------|
| 1000              | 0                 |
| 10000             | 1                 |
| 15000             | 2                 |
| 20000             | 2                 |
| 50000             | 8                 |
| 100000            | 16                |


### Outcomes

- Bubble Sort and Selection Sort:
    - Poor Performance with Larger Datasets: Both bubble sort and selection sort become slower as the dataset size increases. They have a time complexity of O(N^2), which means they take longer to finish with large datasets.
    - Better for Small Datasets: Despite their inefficiency with larger datasets, these algorithms can still be used for small datasets because they are simple and easy to implement.

- Quick Sort:
    - Efficient in Most Cases: Quick sort is generally fast with an average time complexity of O(N*logN), making it a good choice for large datasets.
    - Risk of Slow Performance in Some Cases: In some cases, like when the pivot choice is poor, quick sort can slow down to O(N^2. This risk can be reduced by choosing a pivot randomly.
    - Doesn’t Need Extra Memory: Unlike merge sort, quick sort doesn’t need extra memory, as it sorts the data within the original array.

- Merge Sort:
    - Consistent Performance: Merge sort works efficiently even as the dataset size grows due to its O(N*logN) time complexity.
    - Additional Memory Requirement: A key trade-off is its requirement for additional memory due to its recursive nature and the need for temporary arrays.




