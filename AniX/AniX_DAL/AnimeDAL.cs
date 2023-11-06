using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniX_Shared.DomainModels;
using AniX_Shared.Interfaces;
using AniX_Utility;
using Microsoft.Extensions.Configuration;

namespace AniX_DAL
{
    public class AnimeDAL : BaseDAL, IAnimeManagement
    {
        private readonly IExceptionHandlingService _exceptionHandlingService;
        private readonly IErrorLoggingService _errorLoggingService;

        public AnimeDAL(
            IConfiguration configuration,
            IExceptionHandlingService exceptionHandlingService,
            IErrorLoggingService errorLoggingService
        ) : base(configuration)
        {
            _exceptionHandlingService = exceptionHandlingService;
            _errorLoggingService = errorLoggingService;
        }

        public async Task<int> CreateAnimeAsync(Anime anime)
        {
            int animeId = -1;
            try
            {
                await connection.OpenAsync();
                var query = @"
                    INSERT INTO Anime 
                    (Name, Description, ReleaseDate, TrailerLink, Country, Season, Episodes, 
                    Studio, Type, Status, Premiered, Aired, CoverImage, Thumbnail, Language, 
                    Rating, Year)
                    VALUES 
                    (@Name, @Description, @ReleaseDate, @TrailerLink, @Country, @Season, @Episodes, 
                    @Studio, @Type, @Status, @Premiered, @Aired, @CoverImage, @Thumbnail, @Language, 
                    @Rating, @Year);
                    SELECT SCOPE_IDENTITY();";

                using (var command = new SqlCommand(query, connection))
                {
                    AddParametersForAnime(command, anime);
                    animeId = Convert.ToInt32(await command.ExecuteScalarAsync());
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

            return animeId;
        }

        public async Task<bool> UpdateAnimeAsync(Anime anime)
        {
            try
            {
                await connection.OpenAsync();
                var queryBuilder = new StringBuilder(@"
                UPDATE [Anime] SET 
                    Name = @Name, 
                    Description = @Description, 
                    ReleaseDate = @ReleaseDate, 
                    TrailerLink = @TrailerLink, 
                    Country = @Country, 
                    Season = @Season, 
                    Episodes = @Episodes, 
                    Studio = @Studio, 
                    Type = @Type, 
                    Status = @Status, 
                    Premiered = @Premiered, 
                    Aired = @Aired, 
                    CoverImage = @CoverImage, 
                    Thumbnail = @Thumbnail, 
                    Language = @Language, 
                    Rating = @Rating, 
                    Year = @Year
                WHERE Id = @Id");

                using (SqlCommand command = new SqlCommand(queryBuilder.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@Id", anime.Id);
                    AddParametersForAnime(command, anime);


                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
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
        }

        public async Task<bool> DeleteAnimeAsync(int animeId)
        {
            try
            {
                await connection.OpenAsync();
                string query = "DELETE FROM [Anime] WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", animeId);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                await _exceptionHandlingService.HandleExceptionAsync(ex);
                return false;
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
        }

        public async Task<Anime> GetAnimeByIdAsync(int animeId)
        {
            Anime anime = null;
            try
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM [Anime] WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", animeId);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            anime = MapReaderToAnime(reader);
                        }
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
            return anime;
        }

        public async Task<List<Anime>> GetAllAnimesAsync()
        {
            List<Anime> animes = new List<Anime>();
            try
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM [Anime]";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            animes.Add(MapReaderToAnime(reader));
                        }
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
            return animes;
        }

        public async Task<List<Anime>> GetAnimesByGenreAsync(string genreName)
        {
            List<Anime> animes = new List<Anime>();
            try
            {
                await connection.OpenAsync();
                string query = @"
                SELECT a.* 
                FROM [Anime] a
                INNER JOIN [Anime_Genre] ag ON a.Id = ag.AnimeId
                INNER JOIN [Genre] g ON ag.GenreId = g.Id
                WHERE g.Name = @genreName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@genreName", genreName);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            animes.Add(MapReaderToAnime(reader));
                        }
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
            return animes;
        }

        public async Task<List<Anime>> SearchAnimesAsync(string searchTerm)
        {
            List<Anime> animes = new List<Anime>();
            try
            {
                await connection.OpenAsync();
                string query = @"
                SELECT * 
                FROM [Anime]
                WHERE Name LIKE @searchTerm 
                   OR Description LIKE @searchTerm
                   OR Studio LIKE @searchTerm
                   OR Type LIKE @searchTerm
                   OR Status LIKE @searchTerm
                   OR Premiered LIKE @searchTerm
                   OR Language LIKE @searchTerm";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            animes.Add(MapReaderToAnime(reader));
                        }
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
            return animes;
        }

        public async Task<List<Anime>> FilterAnimesAsync(string filter)
        {
            List<Anime> animes = new List<Anime>();
            try
            {
                await connection.OpenAsync();
                var filterParts = filter.Split(':');
                if (filterParts.Length != 2)
                {
                    throw new ArgumentException("Filter must be in the format 'Column:Value'.");
                }
                string column = filterParts[0];
                string value = filterParts[1];

                var validColumns = new HashSet<string> { "Status", "Type", "Season", "Language", "Rating", "Year" };
                if (!validColumns.Contains(column))
                {
                    throw new ArgumentException("Invalid filter column.");
                }

                string query = $@"
                SELECT * 
                FROM [Anime]
                WHERE [{column}] = @value";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@value", value);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            animes.Add(MapReaderToAnime(reader));
                        }
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
            return animes;
        }

        public async Task<bool> DoesAnimeExistAsync(int animeId)
        {
            try
            {
                await connection.OpenAsync();
                string query = "SELECT COUNT(1) FROM [Anime] WHERE Id = @animeId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@animeId", animeId);
                    int count = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return count > 0;
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
        }

        public async Task<List<AnimeWithViewCount>> GetMostWatchedAnimesAsync()
        {
            var animesWithViewCount = new List<AnimeWithViewCount>();

            try
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("[dbo].[GetMostWatchedAnimeDetails]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var anime = MapReaderToAnimeWithViewCount(reader);
                            animesWithViewCount.Add(anime);
                        }
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

            return animesWithViewCount;
        }


        public async Task<List<AnimeWithPopularity>> GetMostPopularAnimesAsync()
        {
            var animesWithPopularity = new List<AnimeWithPopularity>();

            try
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("[dbo].[GetMostPopularAnime]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var anime = new AnimeWithPopularity
                            {
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                WatchlistCount = reader.GetInt32(reader.GetOrdinal("WatchlistCount"))
                            };
                            animesWithPopularity.Add(anime);
                        }
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

            return animesWithPopularity;
        }

        public async Task<(List<Anime> Animes, int TotalCount)> GetAnimesWithPaginationAsync(int page, int pageSize)
        {
            List<Anime> animes = new List<Anime>();
            int totalCount = 0;

            try
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    // Get the total count of animes
                    using (SqlCommand countCommand = new SqlCommand("SELECT COUNT(*) FROM [Anime]", connection, transaction))
                    {
                        totalCount = (int)await countCommand.ExecuteScalarAsync();
                    }

                    using (SqlCommand command = new SqlCommand("[dbo].[GetAnimesWithPagination]", connection, transaction))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PageNumber", page);
                        command.Parameters.AddWithValue("@PageSize", pageSize);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Anime anime = MapReaderToAnime(reader);
                                animes.Add(anime);
                            }
                        }
                    }

                    transaction.Commit();
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

            return (animes, totalCount);
        }


        public async Task<List<AnimeWithRatings>> GetAnimesWithRatingsAsync()
        {
            var animesWithRatings = new List<AnimeWithRatings>();

            try
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM [AnimeWithRatings]", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            AnimeWithRatings anime = MapReaderToAnimeWithRatings(reader);
                            animesWithRatings.Add(anime);
                        }
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

            return animesWithRatings;
        }

        public async Task<List<Anime>> GetRecommendedAnimesAsync(int userId)
        {
            var recommendedAnimes = new List<Anime>();

            try
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("[dbo].[GetRecommendedAnimes]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Anime anime = MapReaderToAnime(reader);
                            recommendedAnimes.Add(anime);
                        }
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

            return recommendedAnimes;
        }

        public async Task<bool> RecordAnimeViewAsync(AnimeViews animeView)
        {
            try
            {
                await connection.OpenAsync();
                string query = @"
            INSERT INTO [dbo].[AnimeViews] (AnimeId, UserId, ViewDate)
            VALUES (@AnimeId, @UserId, @ViewDate);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AnimeId", animeView.AnimeId);
                    command.Parameters.AddWithValue("@UserId", animeView.UserId);
                    command.Parameters.AddWithValue("@ViewDate", animeView.ViewDate);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                await _exceptionHandlingService.HandleExceptionAsync(ex);
                return false;
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
        }

        public async Task<List<AnimeViews>> GetAnimeViewsAsync(int animeId)
        {
            var animeViewsList = new List<AnimeViews>();

            try
            {
                await connection.OpenAsync();
                string query = @"
                SELECT [Id], [AnimeId], [UserId], [ViewDate]
                FROM [dbo].[AnimeViews]
                WHERE [AnimeId] = @AnimeId;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AnimeId", animeId);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            AnimeViews animeView = MapReaderToAnimeView(reader);
                            animeViewsList.Add(animeView);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                await _exceptionHandlingService.HandleExceptionAsync(ex);
                return new List<AnimeViews>();
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

            return animeViewsList;
        }

        private void AddParametersForAnime(SqlCommand command, Anime anime)
        {
            command.Parameters.AddWithValue("@Name", anime.Name);
            command.Parameters.AddWithValue("@Description", anime.Description ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ReleaseDate", anime.ReleaseDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@TrailerLink", anime.TrailerLink ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Country", anime.Country ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Season", anime.Season ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Episodes", anime.Episodes ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Studio", anime.Studio ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Type", anime.Type ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Status", anime.Status ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Premiered", anime.Premiered ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Aired", anime.Aired ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@CoverImage", anime.CoverImage ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Thumbnail", anime.Thumbnail ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Language", anime.Language ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Rating", anime.Rating ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Year", anime.Year ?? (object)DBNull.Value);
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
                Year = reader.IsDBNull(reader.GetOrdinal("Year")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Year"))
            };
        }

        private AnimeWithViewCount MapReaderToAnimeWithViewCount(SqlDataReader reader)
        {
            return new AnimeWithViewCount
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
                ViewCount = reader.GetInt32(reader.GetOrdinal("ViewCount"))
            };
        }

        private AnimeWithRatings MapReaderToAnimeWithRatings(SqlDataReader reader)
        {
            return new AnimeWithRatings
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
                NumberOfReviews = reader.GetInt32(reader.GetOrdinal("NumberOfReviews")),
                AverageRating = reader.IsDBNull(reader.GetOrdinal("AverageRating")) ? (float?)null : reader.GetFloat(reader.GetOrdinal("AverageRating")),
            };
        }

        private AnimeViews MapReaderToAnimeView(SqlDataReader reader)
        {
            return new AnimeViews
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                AnimeId = reader.GetInt32(reader.GetOrdinal("AnimeId")),
                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                ViewDate = reader.GetDateTime(reader.GetOrdinal("ViewDate"))
            };
        }
    }
}
