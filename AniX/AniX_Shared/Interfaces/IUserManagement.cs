using Anix_Shared.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniX_Shared.Interfaces
{
    public interface IUserManagement
    {
        bool Create(User user);
        User Read(int id);
        bool Update(User user);
        bool Delete(int id);
        User GetUserFromUsername(string username);
        User GetUserFromId(int id);
        User AuthenticateUser(string username, string rawPassword);
    }
}
