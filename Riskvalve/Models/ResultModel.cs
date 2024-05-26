using Microsoft.EntityFrameworkCore;

namespace Riskvalve.Models;

public class ResultModel
{
    public int? Result { get; set; }
    public string? Message { get; set; }
}