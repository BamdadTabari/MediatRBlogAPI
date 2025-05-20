namespace DataLayer;
public static class SlugHelper
{
    public static string GenerateSlug(string title)
    {
        return title.ToLower()
                    .Replace(" ", "-")  // Replace spaces with hyphens
                    .Replace(".", "")   // Remove dots
                    .Replace(",", "")   // Remove commas
                    .Replace("?", "")   // Remove question marks
                    .Replace("!", "");  // Remove exclamation marks
    }
}