using Microsoft.EntityFrameworkCore;

namespace Riskvalve.Models;

public class UserContext : DbContext
{
    public DbSet<UserModel> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options
            .UseSqlServer(Environment.GetConnectionStringDB())
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
}

public class UserModel
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsEngineer { get; set; }
    public bool IsViewer { get; set; }

    public bool isLogin(HttpContext context)
    {
        try
        {
            if (context.Session.GetString("IsLogin") != null)
            {
                bool status = Convert.ToBoolean(context.Session.GetString("IsLogin"));
                return status;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public UserModel GetLogin(HttpContext context)
    {
        UserModel login = new()
        {
            Username = context.Session.GetString("Username"),
            Role = context.Session.GetString("Role"),
            IsAdmin = Convert.ToBoolean(context.Session.GetString("IsAdmin")),
            IsEngineer = Convert.ToBoolean(context.Session.GetString("IsEngineer")),
            IsViewer = Convert.ToBoolean(context.Session.GetString("IsViewer"))
        };
        return login;
    }

    public Dictionary<string, string> GetLoginSession(HttpContext context)
    {
        Dictionary<string, string> session = new()
        {
            { "IsLogin", context.Session.GetString("IsLogin") },
            { "Username", context.Session.GetString("Username") },
            { "Role", context.Session.GetString("Role") },
            { "Id", context.Session.GetString("Id") },
            { "IsAdmin", context.Session.GetString("IsAdmin") },
            { "IsEngineer", context.Session.GetString("IsEngineer") },
            { "IsViewer", context.Session.GetString("IsViewer") }
        };
        return session;
    }

    public bool doLogin(HttpContext httpcontext, string username, string password)
    {
        UserModel login = new();
        using (var context = new UserContext())
        {
            login = context.User.Where(u => u.Username == username).FirstOrDefault();
            if (login == null)
            {
                return false;
            }
            else if (BCrypt.Net.BCrypt.Verify(password, login.Password))
            {
                httpcontext.Session.SetString("IsLogin", "true");
                httpcontext.Session.SetString("Username", login.Username);
                httpcontext.Session.SetString("Role", login.Role);
                httpcontext.Session.SetString("Id", login.Id.ToString());
                httpcontext.Session.SetString("isAdmin", login.IsAdmin.ToString());
                httpcontext.Session.SetString("isEngineer", login.IsEngineer.ToString());
                httpcontext.Session.SetString("isViewer", login.IsViewer.ToString());
                return true;
            }
        }
        return false;
    }

    public bool doLogout(HttpContext context)
    {
        context.Session.Clear();
        return true;
    }

    public UserModel GetUserModel(int id)
    {
        UserModel user = new();
        using (var context = new UserContext())
        {
            user = context.User.Find(id);
        }
        return user;
    }

    public List<UserModel> GetUserList()
    {
        List<UserModel> userList = new();
        using (var context = new UserContext())
        {
            userList = context.User.ToList();
        }
        return userList;
    }

    public void AddUser(UserModel user)
    {
        using (var context = new UserContext())
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            context.User.Add(user);
            context.SaveChanges();
        }
    }

    public void UpdateUser(UserModel user)
    {
        using (var context = new UserContext())
        {
            UserModel oldUser = context.User.Find(user.Id);
            oldUser.Username = user.Username;
            oldUser.Role = user.Role;
            if (user.Password != null)
            {
                oldUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }
            oldUser.IsAdmin = user.IsAdmin;
            oldUser.IsEngineer = user.IsEngineer;
            oldUser.IsViewer = user.IsViewer;
            context.User.Update(oldUser);
            context.SaveChanges();
        }
    }

    public void DeleteUser(int id)
    {
        using (var context = new UserContext())
        {
            UserModel user = context.User.Find(id);
            context.User.Remove(user);
            context.SaveChanges();
        }
    }
}
