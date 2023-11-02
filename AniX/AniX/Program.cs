using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AniX_BusinessLogic;
using AniX_Shared.Interfaces;
using AniX_DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add this line to add session services to the container
builder.Services.AddSession();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddTransient<IUserManagement, UserDAL>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddDbContext<AniXContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register IHttpContextAccessor and ISessionService
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ISessionService, SessionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Add this line to enable session state
app.UseSession();

app.UseMiddleware<CustomAuthenticationMiddleware>();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.Run();