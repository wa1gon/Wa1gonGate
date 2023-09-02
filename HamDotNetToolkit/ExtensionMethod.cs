

namespace HamDotNetToolkit;

public static class CommonExtMethods
{
    /// <summary>
    /// Extension method for a string to test if it is null, empty or white space.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            return true;
        return false;
    }

    public static bool IsNullOrEmpty<T>(this IEnumerable<T> genericEnumerable)
    {
        return ((genericEnumerable == null) || (!genericEnumerable.Any()));
    }

    public static bool IsNullOrEmpty<T>(this ICollection<T> genericCollection)
    {
        if (genericCollection == null)
        {
            return true;
        }

        return genericCollection.Count < 1;
    }

    public static T ParseOrDefault<T>(this string input) where T : struct
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return default;
        }

        input = GetNumericString(input);

        if (typeof(T) == typeof(int))
        {
            if (int.TryParse(input, out int parsedValue))
            {
                return (T)(object)parsedValue;
            }
        }
        else if (typeof(T) == typeof(decimal))
        {
            if (decimal.TryParse(input, out decimal parsedValue))
            {
                return (T)(object)parsedValue;
            }
        }
        else if (typeof(T) == typeof(float))
        {
            if (float.TryParse(input, out float parsedValue))
            {
                return (T)(object)parsedValue;
            }
        }
        else if (typeof(T) == typeof(double))
        {
            if (double.TryParse(input, out double parsedValue))
            {
                return (T)(object)parsedValue;
            }
        }

        return default;
    }
    private static string GetNumericString(string input)
    {
        char[] validChars = { '.', '-', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        char[] numericChars = input.Where(c => validChars.Contains(c)).ToArray();
        return new string(numericChars);
    }
}