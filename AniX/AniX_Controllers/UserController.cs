﻿using Anix_Shared.DomainModels;
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
        private readonly UserValidationService _userValidationService;
        private readonly AuditService _auditService;

        private const int batchSize = 20;
        private static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        public UserController(
            IAuthenticationService authenticationService,
            IUserManagement userManagement,
            UserValidationService userValidationService,
            AuditService auditService)
        {
            _authenticationService = authenticationService;
            _userManagement = userManagement;
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


        public async Task<List<User>> GetUsersInBatchAsync(int startIndex, int batchSize)
        {
            try
            {
                return await _userManagement.GetUsersInBatchAsync(startIndex, batchSize);
            }
            catch (Exception e)
            {
                await ExceptionHandlingService.HandleExceptionAsync(e);
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
                await ExceptionHandlingService.HandleExceptionAsync(e);
                throw;
            }
            finally
            {
                semaphore.Release();
            }
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            try
            {
                return await _userManagement.DeleteAsync(userId);
            }
            catch (Exception e)
            {
                await ExceptionHandlingService.HandleExceptionAsync(e);
                return false;
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
