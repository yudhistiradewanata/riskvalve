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
            join updateby in _context.User on user.UpdatedBy equals updateby.Id into uc
            from subupdateby in uc.DefaultIfEmpty()
            where user.Id == id
            select new UserData
            {
                Id = user.Id,
                Username = SharedEnvironment.HtmlEncode(user.Username),
                Role = SharedEnvironment.HtmlEncode(user.Role),
                IsAdmin = user.IsAdmin,
                IsEngineer = user.IsEngineer,
                IsViewer = user.IsViewer,
                CreatedBy = user.CreatedBy,
                CreatedAt = user.CreatedAt,
                DeletedBy = user.DeletedBy,
                DeletedAt = user.DeletedAt,
                UpdatedBy = user.UpdatedBy,
                UpdatedAt = user.UpdatedAt,
                CreatedByUser = SharedEnvironment.HtmlEncode(subcreateby.Username ?? ""),
                DeletedByUser = SharedEnvironment.HtmlEncode(subdeleteby.Username ?? ""),
                UpdatedByUser = SharedEnvironment.HtmlEncode(subupdateby.Username ?? "")
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
            join updateby in _context.User on user.UpdatedBy equals updateby.Id into uc
            from subupdateby in uc.DefaultIfEmpty()
            where user.Username == username
            select new UserData
            {
                Id = user.Id,
                Username = SharedEnvironment.HtmlEncode(user.Username),
                Password = SharedEnvironment.HtmlEncode(user.Password),
                Role = SharedEnvironment.HtmlEncode(user.Role),
                IsAdmin = user.IsAdmin,
                IsEngineer = user.IsEngineer,
                IsViewer = user.IsViewer,
                CreatedBy = user.CreatedBy,
                CreatedAt = user.CreatedAt,
                DeletedBy = user.DeletedBy,
                DeletedAt = user.DeletedAt,
                UpdatedBy = user.UpdatedBy,
                UpdatedAt = user.UpdatedAt,
                CreatedByUser = SharedEnvironment.HtmlEncode(subcreateby.Username ?? ""),
                DeletedByUser = SharedEnvironment.HtmlEncode(subdeleteby.Username ?? ""),
                UpdatedByUser = SharedEnvironment.HtmlEncode(subupdateby.Username ?? "")
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
                Username = SharedEnvironment.HtmlEncode(user.Username),
                Role = SharedEnvironment.HtmlEncode(user.Role),
                IsAdmin = user.IsAdmin,
                IsEngineer = user.IsEngineer,
                IsViewer = user.IsViewer,
                CreatedBy = user.CreatedBy,
                CreatedAt = user.CreatedAt,
                DeletedBy = user.DeletedBy,
                DeletedAt = user.DeletedAt,
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
        user.UpdatedBy = user.CreatedBy;
        user.UpdatedAt = user.CreatedAt;
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
        oldUser.UpdatedBy = user.UpdatedBy;
        oldUser.UpdatedAt = user.UpdatedAt;
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
        oldUser.UpdatedBy = user.DeletedBy;
        oldUser.UpdatedAt = user.DeletedAt;
        _context.User.Update(oldUser);
        _context.SaveChanges();
        return GetUser(user.Id);
    }
}
