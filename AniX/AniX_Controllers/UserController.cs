using Anix_Shared.DomainModels;
using AniX_BusinessLogic;
using System;
using AniX_Shared.Interfaces;
using AniX_Utility;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AniX_Controllers
{
    public class UserController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserManagement _userManagement;
        private readonly IExceptionHandlingService _exceptionHandlingService;
        private readonly IErrorLoggingService _errorLoggingService;
        private readonly UserValidationService _userValidationService;
        private readonly AuditService _auditService;
        private static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        private const int batchSize = 20;

        public UserController(
            IAuthenticationService authenticationService,
            IUserManagement userManagement,
            UserValidationService userValidationService,
            AuditService auditService,
            IExceptionHandlingService exceptionHandlingService,
            IErrorLoggingService errorLoggingService)
        {
            _authenticationService = authenticationService;
            _userManagement = userManagement;
            _userValidationService = userValidationService;
            _auditService = auditService;
            _exceptionHandlingService = exceptionHandlingService;
            _errorLoggingService = errorLoggingService;
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            try
            {
                if (!_userValidationService.ValidateCredentials(username, password, out string validationMessage))
                {
                    throw new ValidationException(validationMessage);
                }

                var user = await _authenticationService.AuthenticateUserAsync(username, password);

                await _auditService.LogLoginAttemptAsync(username, user != null);

                if (user == null)
                {
                    throw new AuthenticationException("Failed to authenticate.");
                }

                if (user.Banned)
                {
                    throw new AccountBannedException("Your account has been banned.");
                }

                if (!user.IsAdmin)
                {
                    throw new AuthorizationException("You are not authorized to access this application.");
                }

                return user;
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

        public async Task<List<User>> FetchFilteredAndSearchedUsersAsync(string filter, string searchTerm)
        {
            await semaphore.WaitAsync();
            try
            {
                return await _userManagement.FetchFilteredAndSearchedUsersAsync(filter, searchTerm);
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
            finally
            {
                semaphore.Release();
            }
        }

        public async Task<OperationResult> DeleteUserAsync(int userId)
        {
            try
            {
                OperationResult result = await _userManagement.DeleteAsync(userId);
                return result;
            }
            catch (Exception e)
            {
                bool handled = await _exceptionHandlingService.HandleExceptionAsync(e);
                if (!handled)
                {
                    await _errorLoggingService.LogErrorAsync(e, LogSeverity.Critical);
                }
                return new OperationResult
                {
                    Success = false,
                    Message = "An exception occurred while attempting to delete the user."
                };
            }
        }


        public async Task<bool> DoesUsernameExistAsync(string username)
        {
            return await _userManagement.DoesUsernameExistAsync(username);
        }

        public async Task<bool> DoesEmailExistAsync(string email)
        {
            return await _userManagement.DoesEmailExistAsync(email);
        }

        public class ValidationException : Exception
        {
            public ValidationException(string message) : base(message) { }
        }

        public class AuthenticationException : Exception
        {
            public AuthenticationException(string message) : base(message) { }
        }

        public class AccountBannedException : Exception
        {
            public AccountBannedException(string message) : base(message) { }
        }

        public class AuthorizationException : Exception
        {
            public AuthorizationException(string message) : base(message) { }
        }
    }
}
