namespace SharedLayer;

public class ResultClass
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }
    public object? DataTemp { get; set; }
    public string? DataTempString { get; set; }
}