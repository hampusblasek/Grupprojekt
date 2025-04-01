using Microsoft.EntityFrameworkCore;
public interface IUserRepository{
Task AddUser(User user); // Add new user
Task<User?> FindByMail(string mail); // Returns User with matching email
Task<User?> FindById(Guid id); // Returns user with matching id
Task DeleteUser(Guid id); // Delete user
}

public class UserRepository : IUserRepository{

    private readonly AppContext Context;

    public UserRepository(AppContext context){
        this.Context = context;
    }

    public async Task AddUser(User user){
        await Context.User.AddAsync(user);
        await Context.SaveChangesAsync();
    }

    public async Task<User?> FindByMail(string mail){

        return await Context.User.FirstOrDefaultAsync(find => find.Mail == mail);
    }

    public async Task<User?> FindById(Guid id){

        return await Context.User.FindAsync(id);
    }

    public async Task DeleteUser(Guid userId){

        await Context.User.Where(id => id.Id.Equals(userId)).ExecuteDeleteAsync();
        await Context.SaveChangesAsync();
    }
    
}