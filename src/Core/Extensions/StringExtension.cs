namespace GenerateEverything.Utilities
{
    public static class StringExtension
    {
        public static string ToCamelCase(this string str)
        {
            if (string.IsNullOrEmpty(str) || str.Length <= 1)
                return str;
            return $"{char.ToLowerInvariant(str[0])}{str.Substring(1)}";
        }
    }
}
