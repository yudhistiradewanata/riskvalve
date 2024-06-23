using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLayer;

public class UserClass
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }
    public bool? IsAdmin { get; set; }
    public bool? IsEngineer { get; set; }
    public bool? IsViewer { get; set; }
    public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public string? CreatedAt { get; set; }
    public int? DeletedBy { get; set; }
    public string? DeletedAt { get; set; }
}

public class UserData : UserClass
{
    public string? CreatedByUser { get; set; }
    public string? DeletedByUser { get; set; }
}