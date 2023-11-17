namespace AniX_Shared.DomainModels;

public class WatchLater : UserAnimeAction
{
    public DateTime AddedDate { get; set; } = DateTime.Now;

}