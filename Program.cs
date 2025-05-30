using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Grupprojekt;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        if (!builder.Environment.IsEnvironment("Test"))
        {
            DotNetEnv.Env.Load();
            string connectionString =
                Environment.GetEnvironmentVariable("CONNECTION_STRING")
                ?? throw new Exception("Missing CONNECTION_STRING environment variable");

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
        }
        builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme); 
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("remove_user", policy =>
            {
                policy.RequireAuthenticatedUser();
            });
        });

        builder.Services.AddIdentityCore<User>() 
        .AddEntityFrameworkStores<AppDbContext>()
        .AddApiEndpoints();

        // Added services and repositories for dependency injection
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IProductService, ProductService>(); 
        builder.Services.AddScoped<IProductRepository, ProductRepository>(); 

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.MapIdentityApi<User>();
        app.MapControllers();
        app.UseAuthentication(); 
        app.UseAuthorization(); 

        app.Run();
    }
}
