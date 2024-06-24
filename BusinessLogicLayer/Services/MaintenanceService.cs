using System.Globalization;
using DataAccessLayer;
using Newtonsoft.Json;
using SharedLayer;

namespace BusinessLogicLayer;

public interface IMaintenanceService
{
    MaintenanceData GetMaintenance(int id);
    List<MaintenanceData> GetMaintenanceList(bool IncludeDeleted = false, int AssetID = 0);
    MaintenanceData AddMaintenance(MaintenanceClass maintenance);
    MaintenanceData UpdateMaintenance(MaintenanceClass maintenance);
    MaintenanceData DeleteMaintenance(MaintenanceClass maintenance);
    MaintenanceData GetLastAssetMainenance(int AssetID);
    List<InspectionFileData> GetMaintenanceFiles(int maintenanceID);
    bool AddMaintenanceFile(InspectionFileClass inspectionFile);
    bool DeleteMaintenanceFiles(List<InspectionFileClass> inspectionFiles);
    Dictionary<string, string> ImportMaintenance(List<Dictionary<string, string>> data, int CreatedBy);
}

public class MaintenanceService(
    IMaintenanceRepository maintenanceRepository,
    IIsValveRepairedRepository isValveRepairedRepository,
    IAssetRepository assetRepository,
    IInspectionFileRepository inspectionFileRepository,
    ILogRepository logRepository
) : IMaintenanceService
{
    private readonly IMaintenanceRepository _maintenanceRepository = maintenanceRepository;
    private readonly IIsValveRepairedRepository _isValveRepairedRepository =
        isValveRepairedRepository;
    private readonly IAssetRepository _assetRepository = assetRepository;
    private readonly IInspectionFileRepository _inspectionFileRepository = inspectionFileRepository;
    private readonly ILogRepository _logRepository = logRepository;

    public MaintenanceData GetMaintenance(int id)
    {
        try
        {
            MaintenanceData? maintenanceData = _maintenanceRepository.GetMaintenance(id) ?? throw new Exception("Maintenance not found");
            maintenanceData.Asset = _assetRepository.GetAsset(maintenanceData.AssetID);
            maintenanceData.MaintenanceFiles = _inspectionFileRepository.GetMaintenanceFiles(id);
            if(maintenanceData.MaintenanceFiles != null)
            {
                foreach (var item in maintenanceData.MaintenanceFiles)
                {
                    item.FilePath = SharedEnvironment.app_path.Replace("/", "") + "/" + item.FilePath;
                }
            }
            return maintenanceData;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public List<MaintenanceData> GetMaintenanceList(bool IncludeDeleted = false, int AssetID = 0)
    {
        try
        {
            List<MaintenanceData> maintenanceList = _maintenanceRepository.GetMaintenanceList(IncludeDeleted, AssetID);
            foreach (var maintenance in maintenanceList)
            {
                maintenance.Asset = _assetRepository.GetAsset(maintenance.AssetID);
                maintenance.MaintenanceFiles = _inspectionFileRepository.GetMaintenanceFiles(maintenance.Id);
                if(maintenance.MaintenanceFiles != null)
                {
                    foreach (var item in maintenance.MaintenanceFiles)
                    {
                        item.FilePath = SharedEnvironment.app_path.Replace("/", "") + "/" + item.FilePath;
                    }
                }
            }
            return maintenanceList;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public MaintenanceData AddMaintenance(MaintenanceClass maintenance)
    {
        try
        {
            MaintenanceData maintenanceData =_maintenanceRepository.AddMaintenance(maintenance);
            maintenanceData.Asset = _assetRepository.GetAsset(maintenanceData.AssetID);
            maintenanceData.MaintenanceFiles = _inspectionFileRepository.GetMaintenanceFiles(maintenanceData.Id);
            if(maintenanceData.MaintenanceFiles != null)
            {
                foreach (var item in maintenanceData.MaintenanceFiles)
                {
                    item.FilePath = SharedEnvironment.app_path.Replace("/", "") + "/" + item.FilePath;
                }
            }
            return maintenanceData;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public MaintenanceData UpdateMaintenance(MaintenanceClass maintenance)
    {
        try
        {
            MaintenanceData maintenanceData =_maintenanceRepository.UpdateMaintenance(maintenance);
            maintenanceData.Asset = _assetRepository.GetAsset(maintenanceData.AssetID);
            maintenanceData.MaintenanceFiles = _inspectionFileRepository.GetMaintenanceFiles(maintenanceData.Id);
            if(maintenanceData.MaintenanceFiles != null)
            {
                foreach (var item in maintenanceData.MaintenanceFiles)
                {
                    item.FilePath = SharedEnvironment.app_path.Replace("/", "") + "/" + item.FilePath;
                }
            }
            return maintenanceData;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public MaintenanceData DeleteMaintenance(MaintenanceClass maintenance)
    {
        try
        {
            return _maintenanceRepository.DeleteMaintenance(maintenance);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private ToolImportClass MapMaintenanceRegister(List<Dictionary<string, string>> datas)
    {
        ToolImportClass toolImport = new();
        List<string> failedRecords = [];
        List<AssetData> assetList = _assetRepository.GetAssetList();
        List<IsValveRepairedData> isValveRepairedList =
            _isValveRepairedRepository.GetIsValveRepairedList();
        List<Dictionary<string, string>> finalresult = [];
        foreach (var records in datas)
        {
            Dictionary<string, string> result = [];
            foreach (var record in records)
            {
                string key = record.Key;
                string value = record.Value.Trim().ToLower();
                string valuereal = record.Value.Trim();
                string mappedKey = MapHeader(key);
                string mappedValue = "";
                if (mappedKey == "")
                {
                    continue;
                }
                if (
                    mappedKey.Equals("AssetID")
                    || mappedKey.Equals("MaintenanceDate")
                    || mappedKey.Equals("IsValveRepairedID")
                )
                {
                    if (mappedKey.Equals("AssetID"))
                    {
                        foreach (var asset in assetList)
                        {
                            if (asset.TagNo == null)
                            {
                                continue;
                            }
                            if (asset.TagNo.ToLower().Equals(value))
                            {
                                mappedValue = asset.Id.ToString();
                                break;
                            }
                        }
                    }
                    else if (mappedKey.Equals("MaintenanceDate"))
                    {
                        if (value.Contains("/"))
                        {
                            string date = value.Split(" ")[0];
                            List<string> dateParts = date.Split("/").ToList();
                            string newDate = "";
                            if (dateParts[0].Length == 1)
                            {
                                dateParts[0] = "0" + dateParts[0];
                            }
                            if (dateParts[1].Length == 1)
                            {
                                dateParts[1] = "0" + dateParts[1];
                            }
                            date = string.Join("/", dateParts);
                            string[] formats = ["dd/MM/yyyy", "MM/dd/yyyy"];
                            if (DateTime.TryParseExact(date, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                            {
                                if (parsedDate.Day == int.Parse(date.Split('/')[0]))
                                {
                                    newDate = dateParts[0] + "-" + dateParts[1] + "-" + dateParts[2];
                                }
                                else
                                {
                                    newDate = dateParts[1] + "-" + dateParts[0] + "-" + dateParts[2];
                                }
                            }
                            else
                            {
                                newDate = "01-01-1900";
                            }
                            Console.WriteLine("Mapped Value: " + string.Join(", ", dateParts));
                            mappedValue = newDate;
                        }
                        else
                        {
                            mappedValue = DateTime
                                .FromOADate(Convert.ToDouble(value))
                                .ToString(SharedEnvironment.GetDateFormatString(false));
                        }
                        Console.WriteLine("Mapped Value: " + value);
                        Console.WriteLine("Mapped Value: " + mappedValue);
                    }
                    else if (mappedKey.Equals("IsValveRepairedID"))
                    {
                        foreach (var isValveRepaired in isValveRepairedList)
                        {
                            if (isValveRepaired.IsValveRepaired == null)
                            {
                                continue;
                            }
                            if (isValveRepaired.IsValveRepaired.ToLower().Equals(value))
                            {
                                mappedValue = isValveRepaired.Id.ToString();
                                break;
                            }
                        }
                    }
                    if (mappedValue == "")
                    {
                        failedRecords.Add(
                            "Value "
                                + record.Value
                                + " on field "
                                + key
                                + " is not found on the list"
                        );
                    }
                    else
                    {
                        value = mappedValue;
                    }
                }
                else
                {
                    value = valuereal;
                }
                result.Add(mappedKey, value);
            }
            finalresult.Add(result);
        }
        toolImport.MappedRecords = finalresult;
        toolImport.FailedRecords = failedRecords;
        return toolImport;
    }

    private static string MapHeader(string name)
    {
        string result = name.ToLower() switch
        {
            "valve tag no." => "AssetID",
            "is valve repaired?\n(y/n)" => "IsValveRepairedID",
            "maintenance date\n(dd/mm/yyyy)" => "MaintenanceDate",
            "maintenance description" => "MaintenanceDescription",
            _ => "",
        };
        return result;
    }

    public MaintenanceData GetLastAssetMainenance(int AssetID)
    {
        try
        {
            MaintenanceData? maintenanceData = _maintenanceRepository
                .GetMaintenanceList(false, AssetID)
                .OrderByDescending(x => DateTime.ParseExact(x.MaintenanceDate ?? "01-01-0001", "dd-MM-yyyy", CultureInfo.InvariantCulture))
                .FirstOrDefault() ?? throw new Exception("Maintenance not found");
            return maintenanceData;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public List<InspectionFileData> GetMaintenanceFiles(int maintenanceID)
    {
        try {
            List<InspectionFileData> maintenanceFiles = _inspectionFileRepository.GetMaintenanceFiles(maintenanceID);
            foreach (var item in maintenanceFiles)
            {
                item.FilePath = SharedEnvironment.app_path.Replace("/", "") + "/" + item.FilePath;
            }
            return maintenanceFiles;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public bool AddMaintenanceFile(InspectionFileClass inspectionFile)
    {
        try{
            return _inspectionFileRepository.AddInspectionFile(inspectionFile);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public bool DeleteMaintenanceFiles(List<InspectionFileClass> inspectionFiles)
    {
        try{
            return _inspectionFileRepository.DeleteInspectionFiles(inspectionFiles);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Dictionary<string, string> ImportMaintenance(List<Dictionary<string, string>> data, int CreatedBy)
    {
        ToolImportClass toolImport = MapMaintenanceRegister(data);
        if(toolImport.MappedRecords == null || toolImport.MappedRecords.Count == 0){
            throw new Exception("Failed to import maintenance data");
        }
        List<Dictionary<string, string>> result = toolImport.MappedRecords;
        int total = 0;
        int success = 0;
        int failed = 0;
        List<string> failedDatas = [];

        foreach(var item in result){
            if(item == null) continue;
            total++;
            MaintenanceClass? maintenance = null;
            try{
                string json = JsonConvert.SerializeObject(item);
                maintenance = JsonConvert.DeserializeObject<MaintenanceClass>(json);
                if(maintenance == null) continue;
                maintenance.IsDeleted = false;
                maintenance.CreatedAt = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString());
                maintenance.CreatedBy = CreatedBy;
                MaintenanceData? maintenanceData = _maintenanceRepository.AddMaintenance(maintenance);
            }
            catch(Exception e){
                LogData log = new(){
                    Module = "Import Maintenance",
                    CreatedBy = CreatedBy,
                    CreatedAt = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString()),
                    Message = e.Message,
                    Data = JsonConvert.SerializeObject(item)
                };
                _logRepository.AddLog(log);
                failed++;
                failedDatas.Add(maintenance?.MaintenanceDate ?? "");
            }
        }
        success = total - failed;
        string message =
            "Success import "
            + success
            + " data(s) of "
            + total
            + " data(s). Failed "
            + failed
            + " data(s)";
        if (failed > 0)
        {
            if (failedDatas.Count > 0)
            {
                string failedData = "";
                foreach (var _failed in failedDatas)
                {
                    failedData += _failed + ", ";
                }
                failedData = failedData[..^2];
                message += " with Maintenance Date: " + failedData + ".";
            }
            else
            {
                message += ".";
            }
        }
        else
        {
            message += ".";
        }
        string messageFailed = "";
        if(toolImport.FailedRecords != null && toolImport.FailedRecords.Count > 0){
            foreach (var _failed in toolImport.FailedRecords)
            {
                total++;
                failed++;
                messageFailed += _failed + ", ";
                if (_failed.Equals(toolImport.FailedRecords.Last()))
                {
                    messageFailed = messageFailed.Substring(0, messageFailed.Length - 2);
                    message += " Exception Error: " + messageFailed + ".";
                }
            }
        }
        return 
            new Dictionary<string, string>
            {
                { "total", total.ToString() },
                { "success", success.ToString() },
                { "failed", failed.ToString() },
                { "failedDatas", JsonConvert.SerializeObject(failedDatas) },
                { "message", message }
            };
    }
}
