using NumberSortingApi.Exceptions;

namespace NumberSortingApi.Validation;

public static class SortingResponseValidator
{
    public static void Validate(IList<int> numbers)
    {
        var isEmpty = numbers is null || numbers.Count == 0;
        if (isEmpty)
        {
            throw new ResultNotFoundException("The number list is empty.");
        }
        
        var isValid = numbers is {Count: > 0 and <= 10} &&
                      numbers.All(n => n is > 0 and <= 10) &&
                      numbers.Distinct().Count() == numbers.Count;
        if (!isValid)
        {
            throw new BadResponseException("The sorting response is not valid.");
        }
    }

    public static bool IsValid(IList<int> numbers)
    {
        return numbers.Count > 0 && numbers.All(n => n is > 0 and <= 10);
    }
}