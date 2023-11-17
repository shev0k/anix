using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AniX_BusinessLogic;
using AniX_Shared.Interfaces;
using AniX_DAL;
using AniX_Shared.DomainModels;
using AniX_Utility;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add session services to the container
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<IErrorLoggingService, ErrorLoggingService>();
builder.Services.AddSingleton<IExceptionHandlingService, ExceptionHandlingService>();
builder.Services.AddTransient<IAzureBlobService, AzureBlobService>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var errorLoggingService = sp.GetRequiredService<IErrorLoggingService>();
    return new AzureBlobService(configuration, errorLoggingService);
});
builder.Services.AddTransient<IUserManagement, UserDAL>(sp => new UserDAL(
    sp.GetRequiredService<IAzureBlobService>(),
    sp.GetRequiredService<IConfiguration>(),
    sp.GetRequiredService<IExceptionHandlingService>(),
    sp.GetRequiredService<IErrorLoggingService>()
));

builder.Services.AddTransient<IAnimeManagement, AnimeDAL>(sp => new AnimeDAL(
    sp.GetRequiredService<IAzureBlobService>(),
    sp.GetRequiredService<IConfiguration>(),
    sp.GetRequiredService<IExceptionHandlingService>(),
    sp.GetRequiredService<IErrorLoggingService>()
));

builder.Services.AddTransient<IReviewManagement, ReviewDAL>(sp => new ReviewDAL(
    sp.GetRequiredService<IConfiguration>(),
    sp.GetRequiredService<IExceptionHandlingService>(),
    sp.GetRequiredService<IErrorLoggingService>()
));

builder.Services.AddTransient<IUserAnimeActionManagement, UserAnimeActionDAL>(sp => new UserAnimeActionDAL(
    sp.GetRequiredService<IConfiguration>(),
    sp.GetRequiredService<IExceptionHandlingService>(),
    sp.GetRequiredService<IErrorLoggingService>()
));

builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<IUserAnimeActionManagement, UserAnimeActionDAL>();

// Register IHttpContextAccessor and ISessionService
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ISessionService, SessionService>();

// Authentication configuration
builder.Services.AddAuthentication("CustomAuthScheme")
    .AddCookie("CustomAuthScheme", options =>
    {
        options.Cookie.HttpOnly = true;
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Enable session state
app.UseSession();

// Custom authentication middleware
app.UseMiddleware<CustomAuthenticationMiddleware>();

app.UseStatusCodePagesWithReExecute("/404", "?statusCode={0}");

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.Run();