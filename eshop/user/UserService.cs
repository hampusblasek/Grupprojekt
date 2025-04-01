public interface IUserService{

Task<User> RegisterUser(string mail, string password); 

Task<User> Login(string mail, string password);

Task<User> RemoveUser(Guid id);

}
/* public class UserService : IUserService{

    private readonly UserRepository UserRepository;

    public UserService(UserRepository userRepository)
    {
        this.UserRepository = userRepository;
    }

} */