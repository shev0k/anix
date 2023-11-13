using System.Globalization;
using System.Text.RegularExpressions;

namespace AniX_Utility;

public static class SlugConverter
{
    public static string ToSlug(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return "";

        var slug = Regex.Replace(name.ToLowerInvariant(), @"\s+", "-");

        slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");

        slug = slug.Trim('-');

        return slug;
    }

    public static string FromSlug(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            return "";

        var nameLike = slug.Replace("-", " ");
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nameLike);
    }
}