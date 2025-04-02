using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
public class AppContext : IdentityDbContext<User>
{
    public AppContext(DbContextOptions<AppContext> options) : base(options) { }
    public DbSet<Product> Product { get; set; }
}