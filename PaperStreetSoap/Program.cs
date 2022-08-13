using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectBase.Infrastructure.Repositories;
using ProjectBase.Infrastructure.Contexts;
using ProjectBase.Core.Interfaces.Repositories;
using ProjectBase.Core.Models;
using PaperStreetSoap.Data;
using PaperStreetSoap.Core.Interfaces.Repositories;
using PaperStreetSoap.Infrastructure.Repositories;
using PaperStreetSoap.Core.Interfaces.Clients;
using PaperStreetSoap.Infrastructure.Clients;
using PaperStreetSoap.Infrastructure.Services;
using PaperStreetSoap.Core.Interfaces.Services;
using ProjectBase.Core.Interfaces.Services;
using ProjectBase.Infrastructure.Services;
using UoN.ExpressiveAnnotations.NetCore.DependencyInjection;
using Rollbar;
using Rollbar.NetCore.AspNet;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>().AddRoles<IdentityRole>().AddDefaultUI().AddDefaultTokenProviders();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<IDapperContext, DapperContext>();
builder.Services.AddScoped<IHomeRepository, HomeRepository>();
builder.Services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IVideosRepository, VideosRepository>();
builder.Services.AddScoped<IPackagesRepository, PackagesRepository>();
builder.Services.AddScoped<IChartsRepository, ChartsRepository>();
builder.Services.AddScoped<IAzureStorageRepository, AzureStorageRepository>();
builder.Services.AddScoped<IPageContentRepository, PageContentRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IOpenNodeClient, OpenNodeClient>();
builder.Services.AddScoped<IErrorLoggerService, ErrorLoggerService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/register";
});

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

builder.Services.AddExpressiveAnnotations();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

RollbarInfrastructureConfig config = new(builder.Configuration["RollbarConfig:AccessToken"], builder.Configuration["RollbarConfig:Environment"]);
config.RollbarInfrastructureOptions.CaptureUncaughtExceptions = true;
RollbarDataSecurityOptions dataSecurityOptions = new();
dataSecurityOptions.ScrubFields = new string[] { "url", "method", };
config.RollbarLoggerConfig.RollbarDataSecurityOptions.Reconfigure(dataSecurityOptions);

RollbarInfrastructure.Instance.Init(config);

builder.Services.AddRollbarLogger(loggerOptions =>
{
    loggerOptions.Filter = (loggerName, loglevel) => loglevel >= LogLevel.Critical;

});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseRollbarMiddleware();
app.UseStatusCodePagesWithRedirects("/error/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

