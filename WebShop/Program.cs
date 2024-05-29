using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using WebShop.Data;
using WebShop.Helpers;
using WebShop.Logger;
using WebShop.Models;
using WebShop.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
            .UseLazyLoadingProxies()
    );
builder.Services.AddIdentity<User, IdentityRole>().
    AddEntityFrameworkStores<ApplicationContext>().
    AddDefaultTokenProviders(); ;

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = false;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

DependencyInjection.ConfigureDI(builder.Services);

var serviceProvider = builder.Services.BuildServiceProvider();
ILogService logService = serviceProvider.GetService<ILogService>();
IHttpContextAccessor contextAccessor = serviceProvider.GetService<IHttpContextAccessor>();

builder.Logging.AddDbLogging(logService, contextAccessor);
builder.Logging.AddFilter<DbLoggerProvider>("Microsoft", LogLevel.None);


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{

    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();


    SeedData.Init(dbContext);

    try
    {
        await SeedData.InitUserRoles(userManager, rolesManager);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
