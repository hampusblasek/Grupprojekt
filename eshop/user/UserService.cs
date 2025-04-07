public interface IUserService{
Task<User> RemoveUser(string id); // Delete user

}
public class UserService : IUserService{

    private readonly UserRepository UserRepository;

    public UserService(UserRepository userRepository)
    {
        this.UserRepository = userRepository;
    }
    public async Task<User> RemoveUser(string id)
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