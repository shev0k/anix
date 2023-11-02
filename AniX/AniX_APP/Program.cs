using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
using AniX_APP.CustomElements;
using AniX_BusinessLogic;
using AniX_DAL;
using AniX_Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using AniX_Controllers;
using AniX_FormsLogic;

namespace AniX_APP
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddSingleton<ApplicationModel>(sp => new ApplicationModel(null,
                    sp.GetRequiredService<UserController>(),
                    sp.GetRequiredService<UserDAL>()))
                .AddTransient<UserDAL>()
                .AddTransient<IUserManagement, UserDAL>()
                .AddTransient<IAuthenticationService, AuthenticationService>()
                .AddTransient<UserController>()
                .AddTransient<UserValidationService>()
                .AddTransient<AuditService>()
                .AddTransient<Main>()
                .BuildServiceProvider();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            using (var scope = serviceProvider.CreateScope())
            {
                var form = scope.ServiceProvider.GetRequiredService<Main>(); // This should now work
                Application.Run(form);
            }
        }
    }
}
