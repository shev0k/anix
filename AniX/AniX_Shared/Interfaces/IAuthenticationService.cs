using Anix_Shared.DomainModels;

namespace AniX_Shared.Interfaces
{
    public interface IAuthenticationService
    {
        User AuthenticateUser(string username, string password);
    }
}
