using Microsoft.Extensions.DependencyInjection;
using AniX_BusinessLogic;
using AniX_DAL;
using AniX_Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using AniX_Controllers;
using AniX_FormsLogic;
using AniX_Utility;

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
                .AddSingleton<IErrorLoggingService, ErrorLoggingService>()
                .AddSingleton<IExceptionHandlingService, ExceptionHandlingService>()
                .AddSingleton<IAzureBlobService, AzureBlobService>(sp =>
                {
                    var errorLoggingService = sp.GetRequiredService<IErrorLoggingService>();
                    return new AzureBlobService(configuration, errorLoggingService);
                })
                .AddTransient<UserDAL>(sp => new UserDAL(
                    sp.GetRequiredService<IAzureBlobService>(),
                    configuration,
                    sp.GetRequiredService<IExceptionHandlingService>(),
                    sp.GetRequiredService<IErrorLoggingService>()))
                .AddTransient<IUserManagement, UserDAL>()
                .AddTransient<IAuthenticationService, AuthenticationService>()
                .AddTransient<UserValidationService>()
                .AddTransient<AuditService>(sp => new AuditService(
                    sp.GetRequiredService<IErrorLoggingService>(),
                    sp.GetRequiredService<IExceptionHandlingService>()))
                .AddTransient<UserController>()
                .AddSingleton<ApplicationModel>(sp => new ApplicationModel(
                    null,
                    sp.GetRequiredService<UserController>(),
                    sp.GetRequiredService<UserDAL>()))
                .AddTransient<Main>()
                .BuildServiceProvider();

            ApplicationConfiguration.Initialize();

            using (var scope = serviceProvider.CreateScope())
            {
                var form = scope.ServiceProvider.GetRequiredService<Main>();
                Application.Run(form);
            }
        }
    }
}
