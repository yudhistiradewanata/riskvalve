using DataAccessLayer;
using SharedLayer;

namespace BusinessLogicLayer;

public interface IUserService
{
    UserData GetUser(int id);
    UserData GetUser(string username);
    List<UserData> GetUserList();
    UserData AddUser(UserClass user);
    UserData UpdateUser(UserClass user);
    UserData DeleteUser(UserClass user);
}

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public UserData GetUser(int id)
    {
        try
        {
            return _userRepository.GetUser(id);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public UserData GetUser(string username)
    {
        try
        {
            return _userRepository.GetUser(username);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public List<UserData> GetUserList()
    {
        return _userRepository.GetUserList();
    }

    public UserData AddUser(UserClass user)
    {
        try
        {
            UserData userData = _userRepository.AddUser(user);
            return userData;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public UserData UpdateUser(UserClass user)
    {
        try
        {
            UserData userData = _userRepository.UpdateUser(user);
            return userData;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public UserData DeleteUser(UserClass user)
    {
        try
        {
            UserData userData = _userRepository.DeleteUser(user);
            return userData;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
