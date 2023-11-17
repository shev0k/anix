using AniX_Shared.DomainModels;

namespace AniX_Shared.Extensions;

public class AnimeDetailModel : AnimeWithRatings
{
    public Anime Anime { get; set; }
    public List<Review> Reviews { get; set; } = new List<Review>();
    public List<Genre> Genres { get; set; }
    public double? AverageRating { get; set; }
    public int NumberOfReviews { get; set; }
    public int ViewCount { get; set; }
    public int WatchlistCount { get; set; }
    public List<Review> PendingReviews { get; set; } = new List<Review>();
    public double? CurrentUserRating { get; set; }
}