namespace Shared.String.Extensions;

public static class SlugHelpers
{
    public static string ToSlug(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            return string.Empty;

        // Convert to lower case, trim, and replace spaces with hyphens
        var slug = str
            .Trim()
            .ToLowerInvariant()
            .Replace(" ", "-");

        // Remove invalid characters (keep letters, numbers, and hyphens)
        slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-z0-9\-]", "");

        // Replace multiple hyphens with a single hyphen
        slug = System.Text.RegularExpressions.Regex.Replace(slug, @"\-{2,}", "-");

        return slug;
    }
}