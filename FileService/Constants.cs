using System.Text.RegularExpressions;

namespace FileService;

public static partial class Constants
{
    public static class Directories
    {
        public static string Failed => Path.Combine(AppContext.BaseDirectory, "Failed");
        public static string Succeeded => Path.Combine(AppContext.BaseDirectory, "Succeeded");
        public static string Processing => Path.Combine(AppContext.BaseDirectory, "Processing");
    }

    public static partial class Patterns
    {
        [GeneratedRegex(@"\x03\s*\x01", RegexOptions.Compiled | RegexOptions.Singleline)]
        public static partial Regex FileParseRegex();
    }
}