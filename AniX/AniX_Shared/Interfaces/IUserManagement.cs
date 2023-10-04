using Anix_Shared.DomainModels;

namespace AniX_Shared.Interfaces
{
    public interface IUserManagement
    {
        bool Create(User user);
        bool Update(User user);
        bool Delete(int id);
        User GetUserFromUsername(string username);
        User GetUserFromId(int id);
        User AuthenticateUser(string username, string rawPassword);
    }
}
