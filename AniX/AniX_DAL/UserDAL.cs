using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using AniX_Shared.DomainModels;
using AniX_Shared.Interfaces;
using AniX_Utility;
using AniX_Shared.DomainModels;
using Microsoft.EntityFrameworkCore;
using Anix_Shared.DomainModels;

namespace AniX_DAL
{
    public class UserDAL : IUserManagement
    {
        private readonly AniXContext _context;

        public UserDAL(AniXContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateUserAsync(string username, string rawPassword)
        {
            try
            {
                User user = await _context.User.FirstOrDefaultAsync(u => u.Username == username);

                if (user != null)
                {
                    (string storedPassword, string storedSalt) = user.RetrieveCredentials();
                    string hashedPassword = HashPassword.GenerateHashedPassword(rawPassword, storedSalt);

                    if (hashedPassword == storedPassword)
                    {
                        return user;
                    }
                }
            }
            catch (Exception ex)
            {
                await ExceptionHandlingService.HandleExceptionAsync(ex);
                throw;
            }

            return null;
        }

        public async Task<bool> CreateAsync(User user)
        {
            try
            {
                _context.User.Add(user);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                await ExceptionHandlingService.HandleExceptionAsync(ex);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(User user)
        {
            try
            {
                var existingUser = await _context.User.SingleOrDefaultAsync(u => u.Id == user.Id);

                if (existingUser != null)
                {
                    existingUser.Username = user.Username;
                    (string Password, string Salt) = user.RetrieveCredentials();
                    existingUser.UpdatePassword(Password, Salt);
                    existingUser.Email = user.Email;
                    existingUser.Banned = user.Banned;
                    existingUser.IsAdmin = user.IsAdmin;

                    return (await _context.SaveChangesAsync()) > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                await ExceptionHandlingService.HandleExceptionAsync(ex);
                throw;
            }
        }



        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var userToDelete = await _context.User.SingleOrDefaultAsync(u => u.Id == id);

                if (userToDelete != null)
                {
                    _context.User.Remove(userToDelete);

                    return (await _context.SaveChangesAsync()) > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                await ExceptionHandlingService.HandleExceptionAsync(ex);
                throw;
            }
        }


        public async Task<User> GetUserFromIdAsync(int id)
        {
            return await GetUserByIdAsync(id);
        }

        public async Task<User> GetUserFromUsernameAsync(string username)
        {
            try
            {
                var user = await _context.User.SingleOrDefaultAsync(u => u.Username == username);
                return user;
            }
            catch (Exception ex)
            {
                await ExceptionHandlingService.HandleExceptionAsync(ex);
                throw;
            }
        }


        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _context.User.FindAsync(id);
                return user;
            }
            catch (Exception ex)
            {
                await ExceptionHandlingService.HandleExceptionAsync(ex);
                throw;
            }
        }

        public async Task<List<User>> GetUsersInBatchAsync(int startIndex, int batchSize)
        {
            try
            {
                var users = await _context.User
                    .OrderBy(u => u.Id)
                    .Skip(startIndex)
                    .Take(batchSize)
                    .ToListAsync();
                return users;
            }
            catch (Exception ex)
            {
                await ExceptionHandlingService.HandleExceptionAsync(ex);
                throw;
            }
        }


        public async Task<List<User>> FetchFilteredAndSearchedUsersAsync(string filter, string searchTerm)
        {
            try
            {
                IQueryable<User> query = _context.User;

                if (!string.IsNullOrEmpty(filter))
                {
                    switch (filter)
                    {
                        case "Admin":
                            query = query.Where(u => u.IsAdmin);
                            break;
                        case "User":
                            query = query.Where(u => !u.IsAdmin);
                            break;
                        case "Banned":
                            query = query.Where(u => u.Banned);
                            break;
                        case "Not Banned":
                            query = query.Where(u => !u.Banned);
                            break;
                    }
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(u => u.Username.Contains(searchTerm) || u.Email.Contains(searchTerm));
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                await ExceptionHandlingService.HandleExceptionAsync(ex);
                throw;
            }
        }


        public async Task<bool> DoesUsernameExistAsync(string username)
        {
            try
            {
                return await _context.User.AnyAsync(u => u.Username == username);
            }
            catch (Exception ex)
            {
                await ExceptionHandlingService.HandleExceptionAsync(ex);
                throw;
            }
        }


        public async Task<bool> DoesEmailExistAsync(string email)
        {
            try
            {
                return await _context.User.AnyAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                await ExceptionHandlingService.HandleExceptionAsync(ex);
                throw;
            }
        }
    }
}