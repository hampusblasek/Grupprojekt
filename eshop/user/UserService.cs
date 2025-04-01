public interface IUserService{

Task<User> RegisterUser(string mail, string password); 

Task<User> Login(string mail, string password);

Task<User> RemoveUser(Guid id);

}
public class UserService : IUserService{

    private readonly UserRepository UserRepository;

    public UserService(UserRepository userRepository)
    {
        this.UserRepository = userRepository;
    }

    public async Task<User> RegisterUser(string mail, string password)
    {

        if (string.IsNullOrWhiteSpace(mail))
        {
            throw new ArgumentNullException("This field can not be empty");
        }
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentNullException("This field can not be empty");
        }
        User? isTaken = await UserRepository.FindByMail(mail);
        if (isTaken != null)
        {

            throw new ArgumentException("That mail is allready in use");
        }

        User user = new User(mail, password);
        await UserRepository.AddUser(user);
        return user;
    }

    public async Task<User> Login(string mail, string password)
    {

        if (string.IsNullOrWhiteSpace(mail))
        {
            throw new ArgumentNullException("This field can not be empty");
        }
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentNullException("This field can not be empty");
        }
        User? user = await UserRepository.FindByMail(mail);
        if (user == null)
        {

            throw new ArgumentException("Wrong email or password");
        }

        if (user.Password != password)
        {
            throw new ArgumentException("Wrong email or password");
        }

        return user;

    }
    public async Task<User> RemoveUser(Guid id)
    {

        User? user = await UserRepository.FindById(id);
        if (user == null)
        {
            throw new ArgumentException("This user does not exist");
        }
        
        await UserRepository.DeleteUser(id);

        return user;

    }

}