using Microsoft.EntityFrameworkCore;

namespace Riskvalve.Models;

public class UserContext : DbContext
{
    public DbSet<UserModel> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options
            .UseSqlServer(
                "Server=127.0.0.1,1433;Database=Riskvalve;User Id=SA;Password=DB_Password;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;"
            )
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
}

public class UserModel
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }

    public bool isLogin(HttpContext context)
    {
        try
        {
            if (context.Session.GetString("IsLogin") != null)
            {
                return Convert.ToBoolean(context.Session.GetString("IsLogin"));
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
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
            if(user.Password != null)
            {
                oldUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }
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