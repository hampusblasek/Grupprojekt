using Microsoft.EntityFrameworkCore;
public interface IUserRepository{
Task<User?> FindById(string id); // Returns user with matching id
Task DeleteUser(string id); // Delete user
}

public class UserRepository : IUserRepository{

    private readonly AppContext Context;

    public UserRepository(AppContext context){
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

        await DeleteAllUsersProducts(user.Products); // Delete all products before deleting user
        
        await Context.Users.Where(id => id.Id.Equals(userId)).ExecuteDeleteAsync();
        await Context.SaveChangesAsync();

    }

    public async Task DeleteAllUsersProducts(List<Product> products) // Delete all Users product
    {
        foreach (var product in products)
        {
            Product? removeProduct = await Context.Product.FirstOrDefaultAsync(pr => pr.Id.Equals(product.Id));
            if (removeProduct != null)
            {
                Context.Product.Remove(removeProduct);
            }
        }
        await Context.SaveChangesAsync();
    }
}