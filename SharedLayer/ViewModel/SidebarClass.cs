namespace SharedLayer;

public class SidebarClass
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

public class SidebarData : SidebarClass{
    public List<SidebarData>? Child { get; set; }
}