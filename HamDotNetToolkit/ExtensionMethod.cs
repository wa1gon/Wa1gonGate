

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

    /// <summary>
    /// Added file method and line number information to a string.
    /// changes string to be in the format of "Line: 153|Original String - someFile.cs (Some method name)
    /// Usage:  anyString.LineInfo();
    /// </summary>
    /// <param name="message"></param>
    /// <param name="memberName">Added by the compiler</param>
    /// <param name="path">Path added by the compiler, but strips path and reports only file name</param>
    /// <param name="lineNumber">Line number added by the compiler</param>
    /// <returns>The original string with location information.</returns>
    //public static string LineInfo(this string message,
    //    [CallerMemberName] string memberName = "",
    //    [CallerFilePath] string path = "",
    //    [CallerLineNumber] int lineNumber = 0)
    //{
    //    var list = path.Split('\\');
    //    var len = list.Length;
    //    var fileName = list[len - 1];
    //    return $"Line: {lineNumber}|{message} - {fileName} ({memberName}) ";
    //}

    //public static T DeepClone(this T source)
    //{
    //    if (source is null)
    //    {
    //        return default;
    //    }

    //    var original = JsonSerializer.Serialize<T>(source);
    //    var clone = JsonSerializer.Deserialize<T>(original);
    //    return clone;
    //}
    //public static string ToClassString(this WhichShowEnum value)
    //{
    //    switch (value)
    //    {
    //        case WhichShowEnum.Concurrent:
    //            return "Concurrent";
    //        case WhichShowEnum.ShowA:
    //            return "Show A";
    //        case WhichShowEnum.ShowB:
    //            return "Show B";
    //        case WhichShowEnum.ShowC:
    //            return "Show C";
    //        default:
    //            throw new ArgumentException("Invalid enum value.");
    //    }
    //}
}