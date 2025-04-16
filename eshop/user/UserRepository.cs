using Microsoft.EntityFrameworkCore;
namespace Grupprojekt;
public interface IUserRepository{
Task<User?> FindById(string id); // Returns user with matching id
Task DeleteUser(string id); // Delete user
Task DeleteUserProducts(List<Product> products);
}

public class UserRepository : IUserRepository{

    private readonly AppDbContext Context;

    public UserRepository(AppDbContext context){
        this.Context = context;
    }
    public async Task<User?> FindById(string id){

        return await Context.Users.FindAsync(id);
    }

    public async Task DeleteUser(string userId)
    {
        var user = await Context.Users.Include(pr => pr.Products)
        .FirstOrDefaultAsync(id => id.Id.Equals(userId));

        if (user == null) throw new ArgumentNullException("User can not be found");

        await DeleteUserProducts(user.Products); // Delete all products before deleting user
        
        await Context.Users.Where(id => id.Id.Equals(userId)).ExecuteDeleteAsync();
        await Context.SaveChangesAsync();

    }

    public async Task DeleteUserProducts(List<Product> products) // Delete all Users product
    {
        if (products.Count == 0) return;

        // Collect all product IDs
        var productIds = products.Select(p => p.Id).ToList();

        // Get all products to delete in a list
        var productsToDelete = await Context.Product
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync();

        if (productsToDelete.Any())
        {   //Delete all users products
            Context.Product.RemoveRange(productsToDelete);
            await Context.SaveChangesAsync();
        }
    }
}