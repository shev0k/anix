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
    public class ReviewDAL : BaseDAL, IReviewManagement
    {
        private readonly IExceptionHandlingService _exceptionHandlingService;
        private readonly IErrorLoggingService _errorLoggingService;

        public ReviewDAL(
            IConfiguration configuration,
            IExceptionHandlingService exceptionHandlingService,
            IErrorLoggingService errorLoggingService
        ) : base(configuration)
        {
            _exceptionHandlingService = exceptionHandlingService;
            _errorLoggingService = errorLoggingService;
        }

        public async Task<OperationResult> CreateReviewAsync(Review review)
        {
            OperationResult result = new OperationResult();
            try
            {
                await connection.OpenAsync();
                string query = @"INSERT INTO [Review] (UserId, AnimeId, Text, Rating, IsApproved)
                         VALUES (@userId, @animeId, @text, @rating, 0);";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", review.UserId);
                command.Parameters.AddWithValue("@animeId", review.AnimeId);
                command.Parameters.AddWithValue("@text", review.Text);
                command.Parameters.AddWithValue("@rating", review.Rating);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                result.Success = rowsAffected > 0;
                result.Message = result.Success ? "Review created successfully." : "Failed to create the review.";
            }
            catch (Exception ex)
            {
                await _exceptionHandlingService.HandleExceptionAsync(ex);
            }
            finally
            {
                await connection.CloseAsync();
            }
            return result;
        }

        public async Task<OperationResult> UpdateReviewRatingAsync(int userId, int animeId, double newRating)
        {
            OperationResult result = new OperationResult();
            try
            {
                await connection.OpenAsync();
                string query = @"UPDATE [Review] SET Rating = @newRating 
                         WHERE UserId = @userId AND AnimeId = @animeId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@newRating", newRating);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@animeId", animeId);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                result.Success = rowsAffected > 0;
                result.Message = result.Success ? "Review rating updated successfully." : "No reviews found to update.";
            }
            catch (Exception ex)
            {
                await _exceptionHandlingService.HandleExceptionAsync(ex);
            }
            finally
            {
                await connection.CloseAsync();
            }
            return result;
        }

        public async Task<OperationResult> DeleteReviewAsync(int reviewId)
        {
            OperationResult result = new OperationResult();
            try
            {
                await connection.OpenAsync();
                string query = "DELETE FROM [Review] WHERE Id = @reviewId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@reviewId", reviewId);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                result.Success = rowsAffected > 0;
                result.Message = result.Success ? "Review deleted successfully." : "Review not found or could not be deleted.";
            }
            catch (SqlException ex)
            {
                result.Success = false;
                result.Message = "SQL Error occurred while deleting the review.";
                await _exceptionHandlingService.HandleExceptionAsync(ex);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while deleting the review.";
                await _exceptionHandlingService.HandleExceptionAsync(ex);
            }
            finally
            {
                await connection.CloseAsync();
            }
            return result;
        }

        public async Task<OperationResult> ApproveReviewAsync(int reviewId)
        {
            OperationResult result = new OperationResult();
            try
            {
                await connection.OpenAsync();
                string query = "UPDATE [Review] SET IsApproved = 1 WHERE Id = @reviewId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@reviewId", reviewId);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                result.Success = rowsAffected > 0;
                result.Message = result.Success ? "Review approved successfully." : "Review not found or could not be approved.";

            }
            catch (SqlException ex)
            {
                result.Success = false;
                result.Message = "SQL Error occurred while approving the review.";
                await _exceptionHandlingService.HandleExceptionAsync(ex);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while approving the review.";
                await _exceptionHandlingService.HandleExceptionAsync(ex);
            }
            finally
            {
                await connection.CloseAsync();
            }
            return result;
        }

        public async Task<List<Review>> GetReviewsByUserIdAndAnimeIdAsync(int userId, int animeId)
        {
            List<Review> reviews = new List<Review>();
            try
            {
                await connection.OpenAsync();
                string query = @"SELECT * FROM [Review] 
                         WHERE UserId = @userId AND AnimeId = @animeId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@animeId", animeId);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var review = new Review
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            AnimeId = reader.GetInt32(reader.GetOrdinal("AnimeId")),
                            Text = reader.IsDBNull(reader.GetOrdinal("Text")) ? null : reader.GetString(reader.GetOrdinal("Text")),
                            Rating = reader.GetDouble(reader.GetOrdinal("Rating")),
                            IsApproved = reader.GetBoolean(reader.GetOrdinal("IsApproved"))
                        };
                        reviews.Add(review);
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
            return reviews;
        }

        public async Task<Review> GetReviewByIdAsync(int reviewId)
        {
            Review review = null;

            try
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM [Review] WHERE Id = @reviewId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@reviewId", reviewId);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        review = new Review
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            AnimeId = reader.GetInt32(reader.GetOrdinal("AnimeId")),
                            Text = reader.GetString(reader.GetOrdinal("Text")),
                            Rating = reader.GetDouble(reader.GetOrdinal("Rating")),
                            IsApproved = reader.GetBoolean(reader.GetOrdinal("IsApproved")),
                        };
                    }
                }
            }
            catch (SqlException ex)
            {
                await _exceptionHandlingService.HandleExceptionAsync(ex);
            }
            catch (Exception ex)
            {
                await _exceptionHandlingService.HandleExceptionAsync(ex);
            }
            finally
            {
                await connection.CloseAsync();
            }

            return review;
        }

        public async Task<List<Review>> GetPendingReviewsAsync()
        {
            List<Review> pendingReviews = new List<Review>();

            try
            {
                await connection.OpenAsync();
                string query = @"
                SELECT r.*, u.Username AS UserName, a.Name AS AnimeName
                FROM [Review] r
                INNER JOIN [User] u ON r.UserId = u.Id
                INNER JOIN [Anime] a ON r.AnimeId = a.Id
                WHERE r.IsApproved = 0";
                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        pendingReviews.Add(MapReaderToReview(reader));
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

            return pendingReviews;
        }

        public async Task<List<Review>> GetApprovedReviewsByAnimeIdAsync(int animeId)
        {
            List<Review> approvedReviews = new List<Review>();

            try
            {
                await connection.OpenAsync();
                string query = @"
                SELECT r.*, u.Username AS UserName, a.Name AS AnimeName
                FROM [Review] r
                INNER JOIN [User] u ON r.UserId = u.Id
                INNER JOIN [Anime] a ON r.AnimeId = a.Id
                WHERE r.AnimeId = @animeId AND r.IsApproved = 1";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@animeId", animeId);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        approvedReviews.Add(MapReaderToReview(reader));
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

            return approvedReviews;
        }

        public async Task<List<Review>> FetchFilteredReviewsAsync(string filter)
        {
            List<Review> reviews = new List<Review>();
            var queryBuilder = new StringBuilder(@"
            SELECT r.*, u.Username AS UserName, a.Name AS AnimeName
            FROM [Review] r
            INNER JOIN [User] u ON r.UserId = u.Id
            INNER JOIN [Anime] a ON r.AnimeId = a.Id
            WHERE 1 = 1");

            var filters = new Dictionary<string, string>
            {
                { "Approved", "IsApproved = 1" },
                { "Pending", "IsApproved = 0" }
            };

            if (!string.IsNullOrEmpty(filter) && filters.ContainsKey(filter))
            {
                queryBuilder.Append($" AND {filters[filter]}");
            }

            try
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(queryBuilder.ToString(), connection))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        reviews.Add(MapReaderToReview(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                bool handled = await _exceptionHandlingService.HandleExceptionAsync(ex);
                if (!handled)
                {
                    await _errorLoggingService.LogErrorAsync(ex, LogSeverity.Critical);
                }

                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }

            return reviews;
        }

        private Review MapReaderToReview(SqlDataReader reader)
        {
            return new Review
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                AnimeId = reader.GetInt32(reader.GetOrdinal("AnimeId")),
                Text = reader.IsDBNull(reader.GetOrdinal("Text")) ? null : reader.GetString(reader.GetOrdinal("Text")),
                Rating = reader.GetDouble(reader.GetOrdinal("Rating")),
                IsApproved = reader.GetBoolean(reader.GetOrdinal("IsApproved")),
                UserName = reader.GetString(reader.GetOrdinal("UserName")),
                AnimeName = reader.GetString(reader.GetOrdinal("AnimeName"))
            };
        }
    }
}
