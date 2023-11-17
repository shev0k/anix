namespace AniX_Shared.DomainModels;

public abstract class UserAnimeAction
{
    public int UserId { get; set; }
    public Anime Anime { get; set; }
}