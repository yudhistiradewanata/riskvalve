using Microsoft.EntityFrameworkCore;

namespace Riskvalve.Models;

public class ToolImportModel {
    public List<string> failedRecords { get; set; }
    public List<Dictionary<string, string>> mappedRecords { get; set; }
}