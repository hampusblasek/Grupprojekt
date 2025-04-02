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

    public async Task DeleteUser(string userId){

        await Context.Users.Where(id => id.Id.Equals(userId)).ExecuteDeleteAsync();
        await Context.SaveChangesAsync();
    }
    
}