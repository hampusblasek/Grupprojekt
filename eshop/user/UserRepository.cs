using Microsoft.EntityFrameworkCore;
public interface IUserRepository{
Task<User?> FindById(Guid id); // Returns user with matching id
Task DeleteUser(Guid id); // Delete user
}

public class UserRepository : IUserRepository{

    private readonly AppContext Context;

    public UserRepository(AppContext context){
        this.Context = context;
    }
    public async Task<User?> FindById(Guid id){

        return await Context.User.FindAsync(id);
    }

    public async Task DeleteUser(Guid userId){

        await Context.User.Where(id => id.Id.Equals(userId)).ExecuteDeleteAsync();
        await Context.SaveChangesAsync();
    }
    
}