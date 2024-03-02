namespace Riskvalve.Models;

public class PlatformModel
{
    public int Id { get; set; }
    public AreaModel? BusinessArea { get; set; }
    public string? Platform { get; set; }
    public string? Code { get; set; }
}
