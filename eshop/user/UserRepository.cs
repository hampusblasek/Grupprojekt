public interface IUserRepository{
Task AddUser(User user);
Task<User?> FindByMail(string mail);
Task<User?> FindById(Guid id);
Task DeleteUser(Guid id);
}

/* public class UserRepository : IUserRepository{

    private readonly AppContext Context;

    public UserRepository(AppContext context){
        this.Context = context;
    }
    
} */