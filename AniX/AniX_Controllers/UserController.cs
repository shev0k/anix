using Anix_Shared.DomainModels;
using AniX_BusinessLogic;
using System;
using AniX_Shared.Interfaces;
using AniX_Utility;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;

namespace AniX_BusinessLogic.Controllers
{
    public class UserController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly UserValidationService _userValidationService;
        private readonly AuditService _auditService;

        public UserController(
            IAuthenticationService authenticationService,
            UserValidationService userValidationService,
            AuditService auditService)
        {
            _authenticationService = authenticationService;
            _userValidationService = userValidationService;
            _auditService = auditService;
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
                await ExceptionHandlingService.HandleExceptionAsync(e);
                throw;
            }
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
