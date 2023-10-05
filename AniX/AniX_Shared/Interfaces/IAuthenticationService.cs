using Anix_Shared.DomainModels;
using System.Threading.Tasks;

namespace AniX_Shared.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User> AuthenticateUserAsync(string username, string password);
    }
}