using AniX_Shared.DomainModels;
using AniX_Utility;

namespace AniX_Shared.Interfaces;

public interface IUserAnimeActionManagement
{
    Task<OperationResult> AddUserAnimeActionAsync(UserAnimeAction action);
    Task<OperationResult> RemoveUserAnimeActionAsync(UserAnimeAction action);
    Task<List<WatchLater>> GetUserWatchlistAsync(int userId);
    Task<List<PlaylistItem>> GetUserPlaylistAsync(int userId);
    Task<bool> IsAnimeInUserWatchlist(int userId, int animeId);
    Task<bool> IsAnimeInUserPlaylist(int userId, int animeId);

}