namespace NumberSortingApi.Validation;

public static class SortingRequestBodyValidator
{
    public static bool TryParseAndValidate(string? body, out IList<int> numbers)
    {
        numbers = [];
        if (string.IsNullOrWhiteSpace(body))
        {
            return false;
        }
        
        var entries = body.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        if (entries.Length == 0)
        {
            return false;
        }

        foreach (var entry in entries)
        {
            if (!int.TryParse(entry, out var number))
            {
                numbers.Clear();
                return false;
            }

            if (!IsNumberValid(number))
            {
                numbers.Clear();
                return false;
            }

            numbers.Add(number);
        }

        return true;
    }

    private static bool IsNumberValid(int number)
    {
        return number is > 0 and <= 10;
    }
}