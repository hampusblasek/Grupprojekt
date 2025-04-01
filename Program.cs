using Microsoft.EntityFrameworkCore;

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

        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<UserRepository>();
        builder.Services.AddScoped<ProductService>();
        builder.Services.AddScoped<ProductRepository>();

        builder.Services.AddAuthorization();

        builder.Services.AddDbContext<AppContext>(options =>
        {
            options.UseNpgsql(connectionString);
        }); 

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
