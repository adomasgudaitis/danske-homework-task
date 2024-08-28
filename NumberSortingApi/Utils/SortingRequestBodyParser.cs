using NumberSortingApi.Exceptions;

namespace NumberSortingApi.Utils;

public static class SortingRequestBodyParser
{
    public static IList<int> Parse(string body)
    {
        var numbers = body.Split(' ')
            .Select(s =>
            {
                if (!int.TryParse(s, out var number))
                {
                    throw new BadRequestException("Could not parse number from body.");
                }

                return number;
            })
            .ToList();

        return numbers;
    }
}