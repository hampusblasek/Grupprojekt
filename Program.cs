using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Grupprojekt;

public class Program
{
    public static void Main(string[] args)
    {
        DotNetEnv.Env.Load();
        string connectionString =
            Environment.GetEnvironmentVariable("CONNECTION_STRING")
            ?? throw new Exception("Missing CONNECTION_STRING environment variable");
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppContext>(options =>
        {
            options.UseNpgsql(connectionString);
        }); 
        builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme); 
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("remove_user", policy =>
            {
                policy.RequireAuthenticatedUser();
            });
        });

        builder.Services.AddIdentityCore<User>() 
        .AddEntityFrameworkStores<AppContext>()
        .AddApiEndpoints();

        // Added services and repositories for dependency injection
        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<UserRepository>();
        builder.Services.AddScoped<ProductService>();
        builder.Services.AddScoped<ProductRepository>();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.MapIdentityApi<User>();
        app.MapControllers();
        app.UseAuthentication(); //.7
        app.UseAuthorization(); // 6.

        app.Run();
    }
}
