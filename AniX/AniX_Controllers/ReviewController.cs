using AniX_Shared.DomainModels;
using AniX_Shared.Interfaces;
using AniX_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniX_Controllers
{
    public class ReviewController
    {
        private readonly IReviewManagement _reviewManagement;
        private readonly IExceptionHandlingService _exceptionHandlingService;
        private readonly IErrorLoggingService _errorLoggingService;

        public ReviewController(IReviewManagement reviewManagement, IExceptionHandlingService exceptionHandlingService, IErrorLoggingService errorLoggingService)
        {
            _reviewManagement = reviewManagement;
            _exceptionHandlingService = exceptionHandlingService;
            _errorLoggingService = errorLoggingService;
        }

        public async Task<OperationResult> ApproveReviewAsync(int reviewId)
        {
            return await ExecuteWithExceptionHandlingAsync(() => _reviewManagement.ApproveReviewAsync(reviewId));
        }

        public async Task<OperationResult> DeleteReviewAsync(int reviewId)
        {
            return await ExecuteWithExceptionHandlingAsync(() => _reviewManagement.DeleteReviewAsync(reviewId));
        }

        public async Task<List<Review>> FetchFilteredReviewsAsync(string filter)
        {
            return await ExecuteWithExceptionHandlingAsync(() => _reviewManagement.FetchFilteredReviewsAsync(filter));
        }

        private async Task<T> ExecuteWithExceptionHandlingAsync<T>(Func<Task<T>> action)
        {
            try
            {
                return await action();
            }
            catch (Exception e)
            {
                bool handled = await _exceptionHandlingService.HandleExceptionAsync(e);
                if (!handled)
                {
                    await _errorLoggingService.LogErrorAsync(e, LogSeverity.Critical);
                }
                throw;
            }
        }
    }
}
