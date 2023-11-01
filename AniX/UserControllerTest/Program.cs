using System;
using System.Threading.Tasks;
using AniX_Controllers;
using AniX_BusinessLogic;
using AniX_Shared.Interfaces;
using AniX_Utility;
// Additional using statements...

namespace UserControllerTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Create instances of the dependent services
            // Note: Replace these with the actual implementations you are using
            IAuthenticationService authenticationService = new AuthenticationService();
            IUserManagement userManagement = new UserManagement();
            UserValidationService userValidationService = new UserValidationService();
            AuditService auditService = new AuditService();

            // Create an instance of UserController with the dependent services
            UserController userController = new UserController(
                authenticationService,
                userManagement,
                userValidationService,
                auditService);

            // Assume userId 1 for testing. Make sure this userId exists in your database.
            int userId = 1;

            try
            {
                // Fetch the profile image path for the given userId
                string profileImagePath = await userController.GetProfileImagePathAsync(userId);

                // Display the fetched profile image path
                Console.WriteLine($"Fetched profile image path for userId {userId}: {profileImagePath}");
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}