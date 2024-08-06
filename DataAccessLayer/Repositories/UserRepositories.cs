using System.Web;
using Newtonsoft.Json;
using SharedLayer;

namespace DataAccessLayer;

public interface IUserRepository
{
    UserData GetUser(int id);
    UserData GetUser(string username);
    List<UserData> GetUserList();
    UserData AddUser(UserClass user);
    UserData UpdateUser(UserClass user);
    UserData DeleteUser(UserClass user);
}

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    private readonly ApplicationDbContext _context = context;

    public UserData GetUser(int id)
    {
        UserData? userdata = null;
        var result =
            from user in _context.User
            join createby in _context.User on user.CreatedBy equals createby.Id into sc
            from subcreateby in sc.DefaultIfEmpty()
            join deleteby in _context.User on user.DeletedBy equals deleteby.Id into dc
            from subdeleteby in dc.DefaultIfEmpty()
            where user.Id == id
            select new UserData
            {
                Id = user.Id,
                Username = HttpUtility.HtmlEncode(user.Username),
                Role = HttpUtility.HtmlEncode(user.Role),
                IsAdmin = user.IsAdmin,
                IsEngineer = user.IsEngineer,
                IsViewer = user.IsViewer,
                CreatedBy = user.CreatedBy,
                CreatedAt = user.CreatedAt,
                DeletedBy = user.DeletedBy,
                DeletedAt = user.DeletedAt,
                CreatedByUser = HttpUtility.HtmlEncode(subcreateby.Username ?? ""),
                DeletedByUser = HttpUtility.HtmlEncode(subdeleteby.Username ?? "")
            };
        userdata = result.FirstOrDefault();
        if (userdata == null)
        {
            throw new Exception("User not found");
        }
        return userdata;
    }
    public UserData GetUser(string username)
    {
        UserData? userdata = null;
        var result =
            from user in _context.User
            join createby in _context.User on user.CreatedBy equals createby.Id into sc
            from subcreateby in sc.DefaultIfEmpty()
            join deleteby in _context.User on user.DeletedBy equals deleteby.Id into dc
            from subdeleteby in dc.DefaultIfEmpty()
            where user.Username == username
            select new UserData
            {
                Id = user.Id,
                Username = HttpUtility.HtmlEncode(user.Username),
                Password = HttpUtility.HtmlEncode(user.Password),
                Role = HttpUtility.HtmlEncode(user.Role),
                IsAdmin = user.IsAdmin,
                IsEngineer = user.IsEngineer,
                IsViewer = user.IsViewer,
                CreatedBy = user.CreatedBy,
                CreatedAt = user.CreatedAt,
                DeletedBy = user.DeletedBy,
                DeletedAt = user.DeletedAt,
                CreatedByUser = HttpUtility.HtmlEncode(subcreateby.Username ?? ""),
                DeletedByUser = HttpUtility.HtmlEncode(subdeleteby.Username ?? "")
            };
        userdata = result.FirstOrDefault();
        if (userdata == null)
        {
            throw new Exception("User not found");
        }
        return userdata;
    }

    public List<UserData> GetUserList()
    {
        List<UserData>? userdataList = null;
        var result =
            from user in _context.User
            where user.IsDeleted == false
            select new UserData
            {
                Id = user.Id,
                Username = HttpUtility.HtmlEncode(user.Username),
                Role = HttpUtility.HtmlEncode(user.Role),
                IsAdmin = user.IsAdmin,
                IsEngineer = user.IsEngineer,
                IsViewer = user.IsViewer,
                CreatedBy = user.CreatedBy,
                CreatedAt = user.CreatedAt
            };
        userdataList = [.. result];
        return userdataList;
    }

    public UserData AddUser(UserClass user)
    {
        UserClass? searchuser = _context
            .User.Where(u => u.Username == user.Username && u.IsDeleted == false)
            .FirstOrDefault();
        if (searchuser != null)
        {
            throw new Exception("User with email " + user.Username + " already exists.");
        }
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        _context.User.Add(user);
        _context.SaveChanges();
        return GetUser(user.Id);
    }

    public UserData UpdateUser(UserClass user)
    {
        UserClass? oldUser = _context
            .User.Where(u => u.Id == user.Id && u.IsDeleted == false)
            .FirstOrDefault() ?? throw new Exception("User not found");
        UserClass? searchuser = _context
            .User.Where(u => u.Username == user.Username && u.IsDeleted == false && u.Id != user.Id)
            .FirstOrDefault();
        if (searchuser != null)
        {
            throw new Exception("User with email " + user.Username + " already exists.");
        }
        oldUser.Username = user.Username;
        oldUser.Role = user.Role;
        if (user.Password != null && user.Password != "")
        {
            oldUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        }
        oldUser.IsAdmin = user.IsAdmin;
        oldUser.IsEngineer = user.IsEngineer;
        oldUser.IsViewer = user.IsViewer;
        _context.User.Update(oldUser);
        _context.SaveChanges();
        return GetUser(user.Id);
    }

    public UserData DeleteUser(UserClass user)
    {
        UserClass? oldUser = _context
            .User.Where(u => u.Id == user.Id && u.IsDeleted == false)
            .FirstOrDefault() ?? throw new Exception("User not found");
        oldUser.IsDeleted = user.IsDeleted;
        oldUser.DeletedBy = user.DeletedBy;
        oldUser.DeletedAt = user.DeletedAt;
        _context.User.Update(oldUser);
        _context.SaveChanges();
        return GetUser(user.Id);
    }
}
