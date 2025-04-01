using Microsoft.EntityFrameworkCore;
public class AppContext : DbContext
{
    public AppContext(DbContextOptions<AppContext> options) : base(options) { }
    public DbSet<User> User { get; set; }
    public DbSet<Product> Product { get; set; }
}