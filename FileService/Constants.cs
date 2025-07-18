using System.Text.RegularExpressions;

namespace FileService;

public static partial class Constants
{
    public static class Directories
    {
        private static readonly string FilesDirectory =
            Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "FileService", "Files"));

        public static string Failed => Path.GetFullPath(Path.Combine(FilesDirectory, "Failed"));
        public static string Succeeded => Path.GetFullPath(Path.Combine(FilesDirectory, "Succeeded"));
        public static string Processing => Path.GetFullPath(Path.Combine(FilesDirectory, "Processing"));
    }

    public static partial class Patterns
    {
        [GeneratedRegex(@"\x03\s*\x01", RegexOptions.Compiled | RegexOptions.Singleline)]
        public static partial Regex FileParseRegex();
    }
}