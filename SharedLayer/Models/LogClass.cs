namespace SharedLayer;

public class LogClass
{
    public int Id { get; set; }
    public string? Module { get; set; }
    public int? CreatedBy { get; set; }
    public string? CreatedAt { get; set; }
    public string? Message { get; set; }
    public string? Data { get; set; }
}

public class LogData : LogClass
{
    public string? CreatedByUser { get; set; }
}