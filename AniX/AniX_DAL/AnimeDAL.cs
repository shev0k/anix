using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anix_Shared.DomainModels;
using AniX_Shared.DomainModels;
using AniX_Shared.Extensions;
using AniX_Shared.Interfaces;
using AniX_Utility;
using Microsoft.Extensions.Configuration;

namespace AniX_DAL
{
    public class AnimeDAL : BaseDAL, IAnimeManagement
    {
        private readonly IAzureBlobService _blobService;
        private readonly IExceptionHandlingService _exceptionHandlingService;
        private readonly IErrorLoggingService _errorLoggingService;

        public AnimeDAL(
            IAzureBlobService blobService,
            IConfiguration configuration,
            IExceptionHandlingService exceptionHandlingService,
            IErrorLoggingService errorLoggingService
        ) : base(configuration)
        {
            _blobService = blobService;
            _exceptionHandlingService = exceptionHandlingService;
            _errorLoggingService = errorLoggingService;
        }

        public async Task<int> CreateAnimeAsync(Anime anime, List<int> genreIds)
        {
            int animeId = -1;
            SqlTransaction transaction = null;

            try
            {
                await connection.OpenAsync();
                transaction = connection.BeginTransaction();

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

                using (var command = new SqlCommand(query, connection, transaction))
                {
                    AddParametersForAnime(command, anime);
                    animeId = Convert.ToInt32(await command.ExecuteScalarAsync());
                }

                if (animeId > -1)
                {
                    await LinkAnimeWithGenresAsync(animeId, genreIds, transaction);
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                await _exceptionHandlingService.HandleExceptionAsync(ex);
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }

            return animeId;
        }

        public async Task<bool> LinkAnimeWithGenresAsync(int animeId, List<int> genreIds, SqlTransaction transaction)
        {
            try
            {
                foreach (var genreId in genreIds)
                {
                    var query = "INSERT INTO Anime_Genre (AnimeId, GenreId) VALUES (@AnimeId, @GenreId);";
                    using (var command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@AnimeId", animeId);
                        command.Parameters.AddWithValue("@GenreId", genreId);
                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                await _exceptionHandlingService.HandleExceptionAsync(ex);
                return false;
            }
        }

        public async Task<(string coverImageUrl, string thumbnailUrl)> GetAnimeImageUrls(int animeId)
        {
            string coverImageUrl = null;
            string thumbnailUrl = null;

            try
            {
                await connection.OpenAsync();
                var query = "SELECT CoverImage, Thumbnail FROM Anime WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", animeId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            coverImageUrl = reader["CoverImage"] as string;
                            thumbnailUrl = reader["Thumbnail"] as string;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await _exceptionHandlingService.HandleExceptionAsync(ex);
            }
            finally
            {
                await connection.CloseAsync();
            }

            return (coverImageUrl, thumbnailUrl);
        }

        public async Task<bool> UpdateAnimeImages(int animeId, string coverImageUrl, string thumbnailUrl)
        {
            SqlTransaction transaction = null;
            try
            {
                await connection.OpenAsync();
                transaction = connection.BeginTransaction();

                var query = @"
            UPDATE [Anime] SET
                CoverImage = @CoverImage,
                Thumbnail = @Thumbnail
            WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection, transaction))
                {
                    command.Parameters.AddWithValue("@Id", animeId);
                    command.Parameters.AddWithValue("@CoverImage", coverImageUrl ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Thumbnail", thumbnailUrl ?? (object)DBNull.Value);
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                await _exceptionHandlingService.HandleExceptionAsync(ex);
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
            }
        }

        public async Task<bool> UpdateAnimeAsync(Anime anime, List<int> newGenreIds)
        {
            SqlTransaction transaction = null;
            try
            {
                await connection.OpenAsync();
                transaction = connection.BeginTransaction();

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

                using (SqlCommand command = new SqlCommand(queryBuilder.ToString(), connection, transaction))
                {
                    command.Parameters.AddWithValue("@Id", anime.Id);
                    AddParametersForAnime(command, anime);
                    await command.ExecuteNonQueryAsync();
                }

                var deleteGenresQuery = "DELETE FROM Anime_Genre WHERE AnimeId = @AnimeId";
                using (var deleteGenresCommand = new SqlCommand(deleteGenresQuery, connection, transaction))
                {
                    deleteGenresCommand.Parameters.AddWithValue("@AnimeId", anime.Id);
                    await deleteGenresCommand.ExecuteNonQueryAsync();
                }

                await LinkAnimeWithGenresAsync(anime.Id, newGenreIds, transaction);

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
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
            SqlTransaction transaction = null;

            try
            {
                await connection.OpenAsync();
                transaction = connection.BeginTransaction();

                string coverImageUrl = null;
                string thumbnailUrl = null;
                var query = "SELECT CoverImage, Thumbnail FROM Anime WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection, transaction))
                {
                    command.Parameters.AddWithValue("@Id", animeId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            coverImageUrl = reader["CoverImage"] as string;
                            thumbnailUrl = reader["Thumbnail"] as string;
                        }
                    }
                }

                string deleteGenresQuery = "DELETE FROM [Anime_Genre] WHERE AnimeId = @AnimeId";
                using (SqlCommand deleteGenresCommand = new SqlCommand(deleteGenresQuery, connection, transaction))
                {
                    deleteGenresCommand.Parameters.AddWithValue("@AnimeId", animeId);
                    await deleteGenresCommand.ExecuteNonQueryAsync();
                }

                string deleteAnimeQuery = "DELETE FROM [Anime] WHERE Id = @Id";
                using (SqlCommand deleteAnimeCommand = new SqlCommand(deleteAnimeQuery, connection, transaction))
                {
                    deleteAnimeCommand.Parameters.AddWithValue("@Id", animeId);
                    int rowsAffected = await deleteAnimeCommand.ExecuteNonQueryAsync();

                    bool IsValidUri(string uri) => Uri.TryCreate(uri, UriKind.Absolute, out Uri _);

                    if (!string.IsNullOrEmpty(coverImageUrl) && IsValidUri(coverImageUrl))
                    {
                        await _blobService.DeleteImageAsync(coverImageUrl);
                    }
                    if (!string.IsNullOrEmpty(thumbnailUrl) && IsValidUri(thumbnailUrl))
                    {
                        await _blobService.DeleteImageAsync(thumbnailUrl);
                    }
                }

                transaction.Commit();
                return true;
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                transaction?.Rollback();
                await _exceptionHandlingService.HandleExceptionAsync(ex);
                return false;
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                await _exceptionHandlingService.HandleExceptionAsync(ex);
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        public async Task<List<string>> GetSearchSuggestionsAsync(string searchType, string searchTerm)
        {
            List<string> suggestions = new List<string>();

            if (searchType == "Year" && !int.TryParse(searchTerm, out int _))
            {
                return suggestions;
            }

            string columnName = searchType switch
            {
                "Name" => "Name",
                "Year" => "Year",
                "Studio" => "Studio",
                _ => throw new ArgumentException("Invalid search type.")
            };

            string query;
            if (searchType == "Year")
            {
                query = searchTerm.Length < 4 ?
                        $"SELECT DISTINCT {columnName} FROM [dbo].[Anime] WHERE {columnName} >= @SearchTerm" :
                        $"SELECT DISTINCT {columnName} FROM [dbo].[Anime] WHERE {columnName} = @SearchTerm";
            }
            else
            {
                query = $"SELECT DISTINCT TOP 10 {columnName} FROM [dbo].[Anime] WHERE {columnName} LIKE @SearchTerm + '%'";
            }

            try
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SearchTerm", searchType == "Year" ? int.Parse(searchTerm) : searchTerm);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            suggestions.Add(reader[0].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await _exceptionHandlingService.HandleExceptionAsync(ex);
            }
            finally
            {
                await connection.CloseAsync();
            }

            return suggestions;
        }

        public async Task<List<AnimeWithRatings>> GetRecommendedAnimesAsync(int? userId, int? currentAnimeId)
        {
            var recommendedAnimes = new List<AnimeWithRatings>();

            if (userId == null && currentAnimeId == null)
            {
                throw new ArgumentException("Either UserId or CurrentAnimeId must be provided.");
            }

            try
            {
                await connection.OpenAsync();
                var query = @"
                    WITH DistinctAnimes AS (
                        SELECT DISTINCT
                            ar.Id, ar.Name, ar.Description, ar.ReleaseDate, ar.TrailerLink,
                            ar.Country, ar.Season, ar.Episodes, ar.Studio, ar.Type, ar.Status,
                            ar.Premiered, ar.Aired, ar.CoverImage, ar.Thumbnail, ar.Language,
                            ar.Rating, ar.Year, ar.NumberOfReviews, ar.AverageRating,
                            Genres = (SELECT STRING_AGG(g.Name, ', ') 
                                      FROM [Anime_Genre] ag
                                      JOIN [Genre] g ON ag.GenreId = g.Id
                                      WHERE ag.AnimeId = ar.Id
                                      FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)')
                        FROM [dbo].[AnimeWithRatings] ar
                        INNER JOIN [dbo].[Anime_Genre] ag ON ar.Id = ag.AnimeId
                        WHERE 
                            (@UserId IS NOT NULL AND ag.GenreId IN (
                                SELECT TOP(3) GenreId
                                FROM [dbo].[AnimeViews] av
                                INNER JOIN [dbo].[Anime_Genre] ag ON av.AnimeId = ag.AnimeId
                                WHERE av.UserId = @UserId
                                GROUP BY ag.GenreId
                                ORDER BY COUNT(*) DESC
                            ) AND ar.Id NOT IN (
                                SELECT AnimeId FROM [dbo].[AnimeViews] WHERE UserId = @UserId
                            ))
                            OR
                            (@CurrentAnimeId IS NOT NULL AND ar.Id <> @CurrentAnimeId AND
                            (ag.GenreId IN (
                                SELECT GenreId FROM [dbo].[Anime_Genre] WHERE AnimeId = @CurrentAnimeId)
                                OR ar.Studio = (SELECT Studio FROM [dbo].[AnimeWithRatings] WHERE Id = @CurrentAnimeId)
                                OR ar.Type = (SELECT Type FROM [dbo].[AnimeWithRatings] WHERE Id = @CurrentAnimeId)
                            ))
                    )
                    SELECT *, NEWID() AS RandomOrder
                    FROM DistinctAnimes
                    ORDER BY RandomOrder;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CurrentAnimeId", currentAnimeId ?? (object)DBNull.Value);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var anime = MapReaderToAnimeRecommended(reader);
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

        public async Task<AnimeDetailModel> GetAnimeDetailAsync(int animeId, int? currentUserId = null)
        {
            AnimeDetailModel animeDetail = null;

            try
            {
                await connection.OpenAsync();
                // Query for fetching anime details
                string animeQuery = @"
            SELECT ar.*, 
            (SELECT STRING_AGG(g.Name, ', ') FROM [Anime_Genre] ag 
             JOIN [Genre] g ON ag.GenreId = g.Id WHERE ag.AnimeId = ar.Id 
             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') AS Genres
            FROM [dbo].[AnimeWithRatings] ar
            WHERE ar.Id = @AnimeId";

                // Execute the query for anime details
                using (SqlCommand command = new SqlCommand(animeQuery, connection))
                {
                    command.Parameters.AddWithValue("@AnimeId", animeId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            animeDetail = MapReaderToAnimeDetail(reader);
                        }
                    }
                }

                // If anime details were fetched successfully
                if (animeDetail != null)
                {
                    // Query for fetching approved reviews
                    string reviewQuery = @"
                SELECT r.*, u.Username, u.ProfileImagePath
                FROM [Review] r 
                JOIN [User] u ON r.UserId = u.Id 
                WHERE r.AnimeId = @AnimeId AND r.IsApproved = 1";

                    // Execute the query for approved reviews
                    using (SqlCommand command = new SqlCommand(reviewQuery, connection))
                    {
                        command.Parameters.AddWithValue("@AnimeId", animeId);
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var review = MapReaderToReview(reader);
                                animeDetail.Reviews.Add(review);
                            }
                        }
                    }

                    // Fetch pending reviews for the current user if authenticated
                    if (currentUserId.HasValue)
                    {
                        string pendingReviewsQuery = @"
                        SELECT r.*, u.Username, u.ProfileImagePath
                        FROM [Review] r 
                        JOIN [User] u ON r.UserId = u.Id 
                        WHERE r.AnimeId = @AnimeId AND r.UserId = @UserId AND r.IsApproved = 0";

                        using (SqlCommand command = new SqlCommand(pendingReviewsQuery, connection))
                        {
                            command.Parameters.AddWithValue("@AnimeId", animeId);
                            command.Parameters.AddWithValue("@UserId", currentUserId.Value);
                            using (SqlDataReader reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var review = MapReaderToReview(reader);
                                    animeDetail.PendingReviews.Add(review);
                                }
                            }
                        }
                    }

                    if (animeDetail != null && currentUserId.HasValue)
                    {
                        string userRatingQuery = @"
                        SELECT r.Rating
                        FROM [Review] r
                        WHERE r.AnimeId = @AnimeId AND r.UserId = @UserId AND r.IsApproved = 1";

                        using (SqlCommand command = new SqlCommand(userRatingQuery, connection))
                        {
                            command.Parameters.AddWithValue("@AnimeId", animeId);
                            command.Parameters.AddWithValue("@UserId", currentUserId.Value);
                            object result = await command.ExecuteScalarAsync();
                            if (result != DBNull.Value)
                            {
                                animeDetail.CurrentUserRating = Convert.ToDouble(result);
                            }
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

            return animeDetail;
        }

        public async Task<AnimeWithRatings> GetUpcomingAnimeAsync()
        {
            AnimeWithRatings upcomingAnime = null;
            try
            {
                await connection.OpenAsync();
                string query = @"
                SELECT TOP 1 
                    ar.*,
                    (SELECT STRING_AGG(g.Name, ', ') 
                     FROM [Anime_Genre] ag
                     JOIN [Genre] g ON ag.GenreId = g.Id
                     WHERE ag.AnimeId = ar.Id
                     FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') AS Genres
                FROM [dbo].[AnimeWithRatings] ar
                WHERE ar.Status = 'Not Yet Aired' AND ar.ReleaseDate > GETDATE()
                ORDER BY ar.ReleaseDate ASC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            upcomingAnime = MapReaderToAnimeWithGenres(reader);
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
            return upcomingAnime;
        }

        public async Task<List<AnimeWithRatings>> GetTopRatedAnimesAsync(int count)
        {
            var topRatedAnimes = new List<AnimeWithRatings>();

            try
            {
                await connection.OpenAsync();
                string query = @"
            SELECT TOP (@Count) *
            FROM [dbo].[AnimeWithRatings]
            ORDER BY AverageRating DESC, NumberOfReviews DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Count", count);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            topRatedAnimes.Add(MapReaderToAnimeWithRatings(reader));
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

            return topRatedAnimes;
        }

        public async Task<List<AnimeWithRatings>> GetNewlyReleasedAnimesAsync(int count)
        {
            var newlyReleasedAnimes = new List<AnimeWithRatings>();

            try
            {
                await connection.OpenAsync();
                string query = @"
            SELECT TOP (@Count) *
            FROM [dbo].[AnimeWithRatings]
            WHERE ReleaseDate <= GETDATE() 
            ORDER BY ReleaseDate DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Count", count);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            newlyReleasedAnimes.Add(MapReaderToAnimeWithRatings(reader));
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

            return newlyReleasedAnimes;
        }

        public async Task<List<AnimeWithRatings>> GetRecentlyUpdatedAnimesAsync(int count)
        {
            var animes = new List<AnimeWithRatings>();
            try
            {
                await connection.OpenAsync();
                string query = $"SELECT TOP {count} * FROM [dbo].[AnimeWithRatings] ORDER BY Id DESC";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            animes.Add(MapReaderToAnimeWithRatings(reader));
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

        public async Task<List<AnimeWithRatings>> GetRandomAnimesAsync(int count)
        {
            var animes = new List<AnimeWithRatings>();
            try
            {
                await connection.OpenAsync();
                string query = $"SELECT TOP {count} * FROM [dbo].[AnimeWithRatings] ORDER BY NEWID()";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            animes.Add(MapReaderToAnimeWithRatings(reader));
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

        public async Task<AnimeWithRatings> GetRandomAnimeAsync()
        {
            AnimeWithRatings anime = null;
            try
            {
                await connection.OpenAsync();
                string query = $"SELECT TOP 1 * FROM [dbo].[AnimeWithRatings] ORDER BY NEWID()";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            anime = MapReaderToAnimeWithRatings(reader);
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

        public async Task<(List<AnimeWithRatings> Animes, int TotalCount)> GetFilteredAnimesAsync(AnimeFilterModel filter, int pageNumber, int pageSize)
        {
            List<AnimeWithRatings> animes = new List<AnimeWithRatings>();
            int totalCount = 0;

            try
            {
                await connection.OpenAsync();

                string countQuery = BuildFilterCountQuery(filter);
                using (SqlCommand countCommand = new SqlCommand(countQuery, connection))
                {
                    AddFilterParameters(countCommand, filter);
                    totalCount = (int)await countCommand.ExecuteScalarAsync();
                }
                int offset = (pageNumber - 1) * pageSize;
                string paginatedQuery = BuildFilterPaginatedQuery(filter, offset, pageSize);
                using (SqlCommand command = new SqlCommand(paginatedQuery, connection))
                {
                    AddFilterParameters(command, filter);
                    command.Parameters.AddWithValue("@Offset", offset);
                    command.Parameters.AddWithValue("@PageSize", pageSize);


                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var animeDetail = MapReaderToAnimeWithGenres(reader);
                            animes.Add(animeDetail);
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

            return (Animes: animes, TotalCount: totalCount);
        }

        private string BuildFilterCountQuery(AnimeFilterModel filter)
        {
            var queryBuilder = new StringBuilder("SELECT COUNT(DISTINCT A.Id) FROM [dbo].[AnimeWithRatings] AS A ");

            if (filter.GenreIds != null && filter.GenreIds.Count > 0)
            {
                queryBuilder.Append("INNER JOIN [dbo].[Anime_Genre] AS AG ON A.Id = AG.AnimeId ");
            }

            List<string> whereClauses = new List<string>();


            if (filter.GenreIds != null && filter.GenreIds.Count > 0)
            {
                whereClauses.Add($"AG.GenreId IN ({string.Join(", ", filter.GenreIds.Select((_, i) => $"@GenreId{i}"))})");
            }

            if (filter.Countries != null && filter.Countries.Count > 0)
            {
                whereClauses.Add($"A.Country IN ({string.Join(", ", filter.Countries.Select((_, i) => $"@Country{i}"))})");
            }

            if (filter.Premiered != null && filter.Premiered.Count > 0)
            {
                whereClauses.Add($"A.Premiered IN ({string.Join(", ", filter.Premiered.Select((_, i) => $"@Premiered{i}"))})");
            }

            if (filter.Years != null && filter.Years.Count > 0)
            {
                whereClauses.Add($"A.Year IN ({string.Join(", ", filter.Years.Select((year, i) => $"@Year{i}"))})");
            }

            if (filter.Types != null && filter.Types.Count > 0)
            {
                whereClauses.Add($"A.Type IN ({string.Join(", ", filter.Types.Select((_, i) => $"@Type{i}"))})");
            }

            if (filter.Statuses != null && filter.Statuses.Count > 0)
            {
                whereClauses.Add($"A.Status IN ({string.Join(", ", filter.Statuses.Select((_, i) => $"@Status{i}"))})");
            }

            if (filter.Languages != null && filter.Languages.Count > 0)
            {
                whereClauses.Add($"A.Language IN ({string.Join(", ", filter.Languages.Select((_, i) => $"@Language{i}"))})");
            }

            if (filter.Ratings != null && filter.Ratings.Count > 0)
            {
                whereClauses.Add($"A.Rating IN ({string.Join(", ", filter.Ratings.Select((_, i) => $"@Rating{i}"))})");
            }

            if (!string.IsNullOrWhiteSpace(filter.SearchQuery))
            {
                whereClauses.Add("A.Name LIKE @SearchQuery");
            }

            if (whereClauses.Any())
            {
                queryBuilder.Append("WHERE " + string.Join(" AND ", whereClauses));
            }

            return queryBuilder.ToString();
        }

        private string BuildFilterPaginatedQuery(AnimeFilterModel filter, int offset, int pageSize)
        {
            var queryBuilder = new StringBuilder();

            queryBuilder.Append(@"SELECT A.*, STUFF((SELECT ',' + G.Name FROM [dbo].[Genre] G INNER JOIN [dbo].[Anime_Genre] AG ON G.Id = AG.GenreId WHERE AG.AnimeId = A.Id FOR XML PATH('')), 1, 1, '') AS Genres FROM [dbo].[AnimeWithRatings] A ");

            List<string> whereClauses = new List<string>();

            if (filter.SortBy == SortCriteria.MostWatched)
            {
                queryBuilder.Append("LEFT JOIN [dbo].[AnimeViews] AV ON A.Id = AV.AnimeId ");
            }
            else if (filter.SortBy == SortCriteria.MostPopular)
            {
                queryBuilder.Append("LEFT JOIN [dbo].[User_Anime] UA ON A.Id = UA.AnimeId ");
            }

            if (filter.GenreIds != null && filter.GenreIds.Count > 0)
            {
                queryBuilder.Append("INNER JOIN [dbo].[Anime_Genre] AS AG ON A.Id = AG.AnimeId ");
                whereClauses.Add($"AG.GenreId IN ({string.Join(", ", filter.GenreIds.Select((_, i) => $"@GenreId{i}"))})");
            }

            if (filter.Countries != null && filter.Countries.Count > 0)
            {
                whereClauses.Add($"A.Country IN ({string.Join(", ", filter.Countries.Select((_, i) => $"@Country{i}"))})");
            }

            if (filter.Premiered != null && filter.Premiered.Count > 0)
            {
                whereClauses.Add($"A.Premiered IN ({string.Join(", ", filter.Premiered.Select((_, i) => $"@Premiered{i}"))})");
            }

            if (filter.Years != null && filter.Years.Count > 0)
            {
                whereClauses.Add($"A.Year IN ({string.Join(", ", filter.Years.Select((year, i) => $"@Year{i}"))})");
            }

            if (filter.Types != null && filter.Types.Count > 0)
            {
                whereClauses.Add($"A.Type IN ({string.Join(", ", filter.Types.Select((_, i) => $"@Type{i}"))})");
            }

            if (filter.Statuses != null && filter.Statuses.Count > 0)
            {
                whereClauses.Add($"A.Status IN ({string.Join(", ", filter.Statuses.Select((_, i) => $"@Status{i}"))})");
            }

            if (filter.Languages != null && filter.Languages.Count > 0)
            {
                whereClauses.Add($"A.Language IN ({string.Join(", ", filter.Languages.Select((_, i) => $"@Language{i}"))})");
            }

            if (filter.Ratings != null && filter.Ratings.Count > 0)
            {
                whereClauses.Add($"A.Rating IN ({string.Join(", ", filter.Ratings.Select((_, i) => $"@Rating{i}"))})");
            }

            if (!string.IsNullOrWhiteSpace(filter.SearchQuery))
            {
                whereClauses.Add("A.Name LIKE @SearchQuery");
            }

            if (whereClauses.Any())
            {
                queryBuilder.Append("WHERE " + string.Join(" AND ", whereClauses) + " ");
            }

            if (filter.SortBy == SortCriteria.MostWatched || filter.SortBy == SortCriteria.MostPopular)
            {
                queryBuilder.Append(@"GROUP BY A.Id, A.Name, A.Description, A.ReleaseDate, A.TrailerLink, A.Country,
                             A.Season, A.Episodes, A.Studio, A.Type, A.Status, A.Premiered, A.Aired, A.CoverImage,
                             A.Thumbnail, A.Language, A.Rating, A.Year, A.NumberOfReviews, A.AverageRating ");
            }

            if (filter.SortBy == SortCriteria.MostWatched)
            {
                queryBuilder.Append("ORDER BY COUNT(AV.Id) DESC ");
            }
            else if (filter.SortBy == SortCriteria.MostPopular)
            {
                queryBuilder.Append("ORDER BY COUNT(UA.UserId) DESC ");
            }
            else
            {
                switch (filter.SortBy)
                {
                    case SortCriteria.RecentlyUpdated:
                        queryBuilder.Append("ORDER BY A.Id DESC ");
                        break;
                    case SortCriteria.ReleaseDate:
                        queryBuilder.Append("ORDER BY A.ReleaseDate ");
                        break;
                    case SortCriteria.Rating:
                        queryBuilder.Append("ORDER BY A.AverageRating DESC ");
                        break;
                    case SortCriteria.NumberOfEpisodes:
                        queryBuilder.Append("ORDER BY A.Episodes DESC ");
                        break;
                    default:
                        queryBuilder.Append("ORDER BY A.Id ");
                        break;
                }
            }

            queryBuilder.Append($" OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY");

            return queryBuilder.ToString();
        }

        private void AddFilterParameters(SqlCommand command, AnimeFilterModel filter)
        {
            if (filter.GenreIds != null && filter.GenreIds.Any())
            {
                var genreIdParameters = new List<string>();
                for (int i = 0; i < filter.GenreIds.Count; i++)
                {
                    var paramName = $"@GenreId{i}";
                    command.Parameters.AddWithValue(paramName, filter.GenreIds[i]);
                    genreIdParameters.Add(paramName);
                }
                command.CommandText = command.CommandText.Replace("@GenreIds", string.Join(",", genreIdParameters));
            }

            AddListParametersToCommand(command, filter.Countries, "Country");

            AddListParametersToCommand(command, filter.Premiered, "Premiered");

            AddListParametersToCommand(command, filter.Years, "Year");

            AddListParametersToCommand(command, filter.Types, "Type");

            AddListParametersToCommand(command, filter.Statuses, "Status");

            AddListParametersToCommand(command, filter.Languages, "Language");

            AddListParametersToCommand(command, filter.Ratings, "Rating");

            if (!string.IsNullOrWhiteSpace(filter.SearchQuery))
            {
                command.Parameters.AddWithValue("@SearchQuery", $"%{filter.SearchQuery}%");
            }

        }

        private void AddListParametersToCommand<T>(SqlCommand command, List<T> list, string parameterBaseName)
        {
            if (list != null && list.Any())
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string parameterName = $"@{parameterBaseName}{i}";
                    command.Parameters.AddWithValue(parameterName, list[i]);
                }
            }
        }

        public async Task<List<Genre>> GetAllGenresAsync()
        {
            List<Genre> genres = new List<Genre>();
            try
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM [Genre]";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            genres.Add(new Genre
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            });
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
            return genres;
        }

        public async Task<List<Anime>> FetchFilteredAndSearchedAnimesAsync(string filter, string searchTerm)
        {
            var animeList = new List<Anime>();
            var genreFilterApplied = !string.IsNullOrEmpty(filter) && filter == "Genre";
            var allAnimeFilterApplied = string.IsNullOrEmpty(filter) || filter == "All Anime";

            var subquery = new StringBuilder();
            if (genreFilterApplied)
            {
                subquery.AppendLine(@"
            SELECT DISTINCT a.Id
            FROM [dbo].[Anime] a
            JOIN [dbo].[Anime_Genre] ag ON a.Id = ag.AnimeId
            JOIN [dbo].[Genre] g ON ag.GenreId = g.Id
            WHERE g.Name LIKE @GenreName");
            }

            var queryBuilder = new StringBuilder();
            queryBuilder.AppendLine(@"
            SELECT 
                a.*,
                g.Id AS GenreId,
                g.Name AS GenreName
            FROM 
                [dbo].[Anime] a
            JOIN 
                [dbo].[Anime_Genre] ag ON a.Id = ag.AnimeId
            JOIN 
                [dbo].[Genre] g ON ag.GenreId = g.Id");

            if (genreFilterApplied)
            {
                queryBuilder.AppendLine($"WHERE a.Id IN ({subquery})");
            }
            else if (!string.IsNullOrEmpty(filter) && !allAnimeFilterApplied)
            {
                if (filter == "Rating")
                {
                    queryBuilder.AppendLine($"WHERE a.[{filter}] = @FilterValue");
                }
                else
                {
                    queryBuilder.AppendLine($"WHERE a.[{filter}] LIKE @FilterValue");
                }
            }

            queryBuilder.AppendLine("ORDER BY a.Name, g.Id");

            var parameters = new List<SqlParameter>();
            if (genreFilterApplied)
            {
                parameters.Add(new SqlParameter("@GenreName", $"%{searchTerm}%"));
            }
            else if (!string.IsNullOrEmpty(filter) && !allAnimeFilterApplied)
            {
                parameters.Add(new SqlParameter("@FilterValue", filter == "Rating" ? searchTerm : $"%{searchTerm}%"));
            }

            try
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(queryBuilder.ToString(), connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        var genreLookup = new Dictionary<int, List<Genre>>();

                        while (await reader.ReadAsync())
                        {
                            int animeId = reader.GetInt32(reader.GetOrdinal("Id"));
                            if (!animeList.Any(a => a.Id == animeId))
                            {
                                var anime = MapReaderToAnime(reader);
                                anime.Genres = new List<Genre>();
                                animeList.Add(anime);
                                genreLookup[animeId] = new List<Genre>();
                            }

                            int genreId = reader.GetInt32(reader.GetOrdinal("GenreId"));
                            string genreName = reader.GetString(reader.GetOrdinal("GenreName"));

                            var genres = genreLookup[animeId];
                            if (!genres.Any(g => g.Id == genreId))
                            {
                                genres.Add(new Genre { Id = genreId, Name = genreName });
                            }
                        }

                        foreach (var anime in animeList)
                        {
                            anime.Genres = genreLookup[anime.Id];
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
                if (connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
            }

            return animeList;
        }

        public async Task<bool> DoesAnimeNameExistAsync(string animeName)
        {
            try
            {
                await connection.OpenAsync();
                string query = "SELECT COUNT(1) FROM [Anime] WHERE Name = @animeName";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@animeName", animeName);
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
                var query = @"
                SELECT 
                    awr.Id,
                    awr.Name,
                    awr.Type,
                    awr.Thumbnail,
                    COUNT(ua.UserId) AS WatchlistCount,
                    awr.AverageRating
                FROM [dbo].[AnimeWithRatings] awr
                INNER JOIN [dbo].[User_Anime] ua ON awr.Id = ua.AnimeId
                GROUP BY awr.Id, awr.Name, awr.Type, awr.Thumbnail, awr.AverageRating
                ORDER BY WatchlistCount DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var anime = new AnimeWithPopularity
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Type = reader.GetString(reader.GetOrdinal("Type")),
                                Thumbnail = reader.GetString(reader.GetOrdinal("Thumbnail")),
                                WatchlistCount = reader.GetInt32(reader.GetOrdinal("WatchlistCount")),
                                AverageRating = reader.IsDBNull(reader.GetOrdinal("AverageRating"))
                                    ? (double?)null
                                    : reader.GetDouble(reader.GetOrdinal("AverageRating"))
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
                ViewCount = reader.GetInt32(reader.GetOrdinal("ViewCount")),
                AverageRating = reader.IsDBNull(reader.GetOrdinal("AverageRating")) ? (double?)null : reader.GetDouble(reader.GetOrdinal("AverageRating")),
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
                AverageRating = reader.IsDBNull(reader.GetOrdinal("AverageRating")) ? (double?)null : reader.GetDouble(reader.GetOrdinal("AverageRating")),
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

        private AnimeWithRatings MapReaderToAnimeWithGenres(SqlDataReader reader)
        {
            var anime = new AnimeWithRatings
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
                AverageRating = reader.IsDBNull(reader.GetOrdinal("AverageRating")) ? (double?)null : reader.GetDouble(reader.GetOrdinal("AverageRating")),
            };

            if (!reader.IsDBNull(reader.GetOrdinal("Genres")))
            {
                anime.Genres = reader.GetString(reader.GetOrdinal("Genres"))
                    .Split(',')
                    .Select(name => new Genre { Name = name.Trim() })
                    .ToList();
            }
            else
            {
                anime.Genres = new List<Genre>();
            }

            return anime;
        }

        private AnimeDetailModel MapReaderToAnimeDetail(SqlDataReader reader)
        {
            var animeDetail = new AnimeDetailModel
            {
                Anime = new Anime
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
                },
                NumberOfReviews = reader.GetInt32(reader.GetOrdinal("NumberOfReviews")),
                AverageRating = reader.IsDBNull(reader.GetOrdinal("AverageRating")) ? (double?)null : reader.GetDouble(reader.GetOrdinal("AverageRating")),

                Genres = reader.GetString(reader.GetOrdinal("Genres"))
                    .Split(',')
                    .Select(name => new Genre { Name = name.Trim() })
                    .ToList(),
                Reviews = new List<Review>()
            };

            return animeDetail;
        }

        private AnimeWithRatings MapReaderToAnimeRecommended(SqlDataReader reader)
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
                AverageRating = reader.IsDBNull(reader.GetOrdinal("AverageRating")) ? (double?)null : reader.GetDouble(reader.GetOrdinal("AverageRating")),

                Genres = reader.GetString(reader.GetOrdinal("Genres"))
                    .Split(',')
                    .Select(name => new Genre { Name = name.Trim() })
                    .ToList(),
            };
        }

        private Review MapReaderToReview(SqlDataReader reader)
        {
            return new Review
            {
                User = new User
                {
                    Username = reader.GetString(reader.GetOrdinal("Username")),
                    ProfileImagePath = reader.IsDBNull(reader.GetOrdinal("ProfileImagePath")) ? null : reader.GetString(reader.GetOrdinal("ProfileImagePath"))
                },
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                Text = reader.IsDBNull(reader.GetOrdinal("Text")) ? null : reader.GetString(reader.GetOrdinal("Text")),
                AnimeId = reader.GetInt32(reader.GetOrdinal("AnimeId"))


            };
        }
    }
}
