using AniX_Shared.Interfaces;
using AniX_Utility;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using AniX_Shared.DomainModels;
using AniX_Shared.Extensions;

namespace AniX_DAL;

public class UserAnimeActionDAL : BaseDAL, IUserAnimeActionManagement
{
    private readonly IExceptionHandlingService _exceptionHandlingService;
    private readonly IErrorLoggingService _errorLoggingService;

    public UserAnimeActionDAL(
        IConfiguration configuration,
        IExceptionHandlingService exceptionHandlingService,
        IErrorLoggingService errorLoggingService
    ) : base(configuration)
    {
        _exceptionHandlingService = exceptionHandlingService;
        _errorLoggingService = errorLoggingService;
    }

    public async Task<OperationResult> AddUserAnimeActionAsync(UserAnimeAction action)
    {
        OperationResult result = new OperationResult();
        string query = string.Empty;

        if (action is WatchLater)
        {
            query = @"
            IF EXISTS (SELECT 1 FROM User_Anime WHERE UserId = @UserId AND AnimeId = @AnimeId)
                UPDATE User_Anime SET IsInWatchlist = 1 WHERE UserId = @UserId AND AnimeId = @AnimeId
            ELSE
                INSERT INTO User_Anime (UserId, AnimeId, IsInWatchlist) VALUES (@UserId, @AnimeId, 1)";
        }
        else if (action is PlaylistItem)
        {
            query = @"
            IF EXISTS (SELECT 1 FROM User_Anime WHERE UserId = @UserId AND AnimeId = @AnimeId)
                UPDATE User_Anime SET IsInPlaylist = 1 WHERE UserId = @UserId AND AnimeId = @AnimeId
            ELSE
                INSERT INTO User_Anime (UserId, AnimeId, IsInPlaylist) VALUES (@UserId, @AnimeId, 1)";
        }
        else
        {
            result.Success = false;
            result.Message = "Invalid action type.";
            return result;
        }

        try
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", action.UserId);
            command.Parameters.AddWithValue("@AnimeId", action.Anime.Id);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            result.Success = rowsAffected > 0;
            result.Message = result.Success ? "Action processed successfully." : "Failed to process action.";
        }
        catch (Exception ex)
        {
            await _exceptionHandlingService.HandleExceptionAsync(ex);
            result.Success = false;
            result.Message = "Error occurred while processing action.";
        }
        finally
        {
            await connection.CloseAsync();
        }

        return result;
    }

    public async Task<OperationResult> RemoveUserAnimeActionAsync(UserAnimeAction action)
    {
        OperationResult result = new OperationResult();
        string query = string.Empty;

        if (action is WatchLater)
        {
            query = "UPDATE User_Anime SET IsInWatchlist = 0 WHERE UserId = @UserId AND AnimeId = @AnimeId";
        }
        else if (action is PlaylistItem)
        {
            query = "UPDATE User_Anime SET IsInPlaylist = 0 WHERE UserId = @UserId AND AnimeId = @AnimeId";
        }
        else
        {
            result.Success = false;
            result.Message = "Invalid action type.";
            return result;
        }

        try
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", action.UserId);
            command.Parameters.AddWithValue("@AnimeId", action.Anime.Id);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            result.Success = rowsAffected > 0;
            result.Message = result.Success ? "Action removed successfully." : "Failed to remove action.";
        }
        catch (Exception ex)
        {
            await _exceptionHandlingService.HandleExceptionAsync(ex);
            result.Success = false;
            result.Message = "Error occurred while removing action.";
        }
        finally
        {
            await connection.CloseAsync();
        }

        return result;
    }

    public async Task<List<WatchLater>> GetUserWatchlistAsync(int userId)
    {
        var watchlist = new List<WatchLater>();
        try
        {
            await connection.OpenAsync();
            string query = @"
            SELECT a.* FROM [Anime] a
            INNER JOIN [User_Anime] ua ON a.Id = ua.AnimeId
            WHERE ua.UserId = @UserId AND ua.IsInWatchlist = 1";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", userId);

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var anime = MapReaderToAnime(reader);
                    var watchLater = new WatchLater
                    {
                        Anime = anime,
                        UserId = userId,
                    };
                    watchlist.Add(watchLater);
                }
            }
        }
        catch (Exception ex)
        {
            await _exceptionHandlingService.HandleExceptionAsync(ex);
            throw;
        }
        finally
        {
            await connection.CloseAsync();
        }
        return watchlist;
    }

    public async Task<List<PlaylistItem>> GetUserPlaylistAsync(int userId)
    {
        var playlist = new List<PlaylistItem>();
        try
        {
            await connection.OpenAsync();
            string query = @"
            SELECT a.* FROM [Anime] a
            INNER JOIN [User_Anime] ua ON a.Id = ua.AnimeId
            WHERE ua.UserId = @UserId AND ua.IsInPlaylist = 1";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", userId);

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var anime = MapReaderToAnime(reader);
                    var playlistItem = new PlaylistItem
                    {
                        Anime = anime,
                        UserId = userId,
                    };
                    playlist.Add(playlistItem);
                }
            }
        }
        catch (Exception ex)
        {
            await _exceptionHandlingService.HandleExceptionAsync(ex);
            throw;
        }
        finally
        {
            await connection.CloseAsync();
        }
        return playlist;
    }

    public async Task<bool> IsAnimeInUserWatchlist(int userId, int animeId)
    {
        try
        {
            await connection.OpenAsync();
            string query = @"
            SELECT COUNT(1) 
            FROM User_Anime 
            WHERE UserId = @UserId AND AnimeId = @AnimeId AND IsInWatchlist = 1";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@AnimeId", animeId);

            int count = (int)await command.ExecuteScalarAsync();
            return count > 0;
        }
        catch (Exception ex)
        {
            await _exceptionHandlingService.HandleExceptionAsync(ex);
            return false;
        }
        finally
        {
            await connection.CloseAsync();
        }
    }

    public async Task<bool> IsAnimeInUserPlaylist(int userId, int animeId)
    {
        try
        {
            await connection.OpenAsync();
            string query = @"
            SELECT COUNT(1) 
            FROM User_Anime 
            WHERE UserId = @UserId AND AnimeId = @AnimeId AND IsInPlaylist = 1";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@AnimeId", animeId);

            int count = (int)await command.ExecuteScalarAsync();
            return count > 0;
        }
        catch (Exception ex)
        {
            await _exceptionHandlingService.HandleExceptionAsync(ex);
            return false;
        }
        finally
        {
            await connection.CloseAsync();
        }
    }

    private Anime MapReaderToAnime(SqlDataReader reader)
    {
        return new Anime
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
            Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
            ReleaseDate = reader.IsDBNull(reader.GetOrdinal("ReleaseDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ReleaseDate")),
            TrailerLink = reader.IsDBNull(reader.GetOrdinal("TrailerLink")) ? null : reader.GetString(reader.GetOrdinal("TrailerLink")),
            Country = reader.IsDBNull(reader.GetOrdinal("Country")) ? null : reader.GetString(reader.GetOrdinal("Country")),
            Season = reader.IsDBNull(reader.GetOrdinal("Season")) ? null : reader.GetString(reader.GetOrdinal("Season")),
            Episodes = reader.IsDBNull(reader.GetOrdinal("Episodes")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Episodes")),
            Studio = reader.IsDBNull(reader.GetOrdinal("Studio")) ? null : reader.GetString(reader.GetOrdinal("Studio")),
            Type = reader.IsDBNull(reader.GetOrdinal("Type")) ? null : reader.GetString(reader.GetOrdinal("Type")),
            Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : reader.GetString(reader.GetOrdinal("Status")),
            Premiered = reader.IsDBNull(reader.GetOrdinal("Premiered")) ? null : reader.GetString(reader.GetOrdinal("Premiered")),
            Aired = reader.IsDBNull(reader.GetOrdinal("Aired")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Aired")),
            CoverImage = reader.IsDBNull(reader.GetOrdinal("CoverImage")) ? null : reader.GetString(reader.GetOrdinal("CoverImage")),
            Thumbnail = reader.IsDBNull(reader.GetOrdinal("Thumbnail")) ? null : reader.GetString(reader.GetOrdinal("Thumbnail")),
            Language = reader.IsDBNull(reader.GetOrdinal("Language")) ? null : reader.GetString(reader.GetOrdinal("Language")),
            Rating = reader.IsDBNull(reader.GetOrdinal("Rating")) ? null : reader.GetString(reader.GetOrdinal("Rating")),
            Year = reader.IsDBNull(reader.GetOrdinal("Year")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Year")),
        };
    }
}
