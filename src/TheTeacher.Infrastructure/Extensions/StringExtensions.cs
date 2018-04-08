namespace TheTeacher.Infrastructure.Extensions
{
    public static class StringExtensions 
    {
        public static bool Empty(this string String)
            => string.IsNullOrWhiteSpace(String);

    }
}