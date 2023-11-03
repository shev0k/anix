using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AniX_FormsLogic;
using AniX_BusinessLogic;
using AniX_Controllers;
using Anix_Shared.DomainModels;
using AniX_Utility;

namespace AniX_FormsLogic
{
    public class UsersFormLogic
    {

        private ApplicationModel _appModel;

        public UsersFormLogic(ApplicationModel appModel)
        {
            _appModel = appModel;
        }

        public async Task<List<User>> RefreshUsersAsync()
        {
            return await _appModel.UserController.FetchFilteredAndSearchedUsersAsync("All Users", null);
        }

        public async Task<List<User>> UpdateUserListAsync(string selectedFilter, string searchTerm)
        {
            return await _appModel.UserController.FetchFilteredAndSearchedUsersAsync(selectedFilter, searchTerm);
        }

        public async Task<OperationResult> DeleteUserAsync(int userId)
        {
            return await _appModel.UserController.DeleteUserAsync(userId);
        }

        public string ShowUserDetails(User user)
        {
            return $"Username: {user.Username}\nEmail: {user.Email}\nBanned: {(user.Banned ? "Yes" : "No")}\nAdmin: {(user.IsAdmin ? "Yes" : "No")}";
        }

        public User GetSelectedUserFromDataGridView(List<Tuple<User, object>> originalUsers, int rowIndex)
        {
            return originalUsers[rowIndex].Item1;
        }

        public List<Tuple<User, object>> TransformUsersForDataGridView(List<User> users)
        {
            return users.Select(u => new Tuple<User, object>(
                u,
                new
                {
                    u.Id,
                    u.Username,
                    u.Email,
                    RegistrationDate = u.RegistrationDate.ToString("dd MMM yyyy"),
                    Banned = u.Banned ? "Yes" : "No",
                    IsAdmin = u.IsAdmin ? "Yes" : "No"
                }
            )).ToList();
        }
    }
}
