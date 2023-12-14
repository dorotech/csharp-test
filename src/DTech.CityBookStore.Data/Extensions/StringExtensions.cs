namespace DTech.CityBookStore.Data.Extensions;

internal static class StringExtensions
{
    public static string MakeLikeOutput(this string input)
        => $"%{input}%";
}
