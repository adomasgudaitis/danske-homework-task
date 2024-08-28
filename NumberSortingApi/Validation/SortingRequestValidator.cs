using NumberSortingApi.Exceptions;

namespace NumberSortingApi.Validation;

public static class SortingRequestValidator
{
    public static bool IsValid(IList<int> numbers)
    {
        return numbers.Count is > 0 and <= 10 &&  // numbers count between 1-10
               numbers.Distinct().Count() == numbers.Count && // all numbers are distinct
               numbers.All(number => number is > 0 and <= 10); // all numbers are in range of 10
    }

    public static void Validate(IList<int>? numbers)
    {
        var isValid = numbers is { Count: > 0 and <= 10 } &&  // numbers count between 1-10
                      numbers.Distinct().Count() == numbers.Count && // all numbers are distinct
                      numbers.All(number => number is > 0 and <= 10); // all numbers are in range of 10

        if (!isValid)
        {
            throw new BadRequestException("Invalid number list.");
        }
    }
}