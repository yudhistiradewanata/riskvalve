using System.Globalization;
using DataAccessLayer;
using Newtonsoft.Json;
using SharedLayer;

namespace BusinessLogicLayer;

public interface IInspectionService
{
    InspectionData GetInspection(int id);
    List<InspectionData> GetInspectionList(bool IncludeDeleted = false, int AssetID = 0);
    InspectionData AddInspection(InspectionClass inspection);
    InspectionData UpdateInspection(InspectionClass inspection);
    InspectionData DeleteInspection(InspectionClass inspection);
    InspectionData GetLastAssetInspection(int assetId);
    List<InspectionFileData> GetInspectionFiles(int inspectionId);
    bool AddInspectionFile(InspectionFileClass inspectionFile);
    bool DeleteInspectionFiles(List<InspectionFileClass> inspectionFiles);
    Dictionary<string, string> ImportInspection(List<Dictionary<string, string>> data, int CreatedBy);
}

public class InspectionService(
    IInspectionRepository inspectionRepository,
    IAssetRepository assetRepository,
    IInspectionFileRepository inspectionFileRepository,
    IInspectionEffectivenessRepository inspectionEffectivenessRepository,
    IInspectionMethodRepository inspectionMethodRepository,
    ICurrentConditionLimitStateRepository currentConditionLimitStateRepository,
    ILogRepository logRepository
) : IInspectionService
{
    private readonly IInspectionRepository _inspectionRepository = inspectionRepository;
    private readonly IAssetRepository _assetRepository = assetRepository;
    private readonly IInspectionFileRepository _inspectionFileRepository = inspectionFileRepository;
    private readonly IInspectionEffectivenessRepository _inspectionEffectivenessRepository = inspectionEffectivenessRepository;
    private readonly IInspectionMethodRepository _inspectionMethodRepository = inspectionMethodRepository;
    private readonly ICurrentConditionLimitStateRepository _currentConditionLimitStateRepository = currentConditionLimitStateRepository;
    private readonly ILogRepository _logRepository = logRepository;

    public InspectionData GetInspection(int id)
    {
        try{
            InspectionData? inspectionData = _inspectionRepository.GetInspection(id) ?? throw new Exception("Inspection not found");
            inspectionData.Asset = _assetRepository.GetAsset(inspectionData.AssetID);
            inspectionData.InspectionFiles = _inspectionFileRepository.GetInspectionFiles(inspectionData.Id);
            if(inspectionData.InspectionFiles != null)
            {
                foreach (var item in inspectionData.InspectionFiles)
                {
                    item.FilePath = SharedEnvironment.app_path.Replace("/", "") + "/" + item.FilePath;
                }
            }
            return inspectionData;
        } catch (Exception e) {
            throw new Exception(e.Message);
        }
    }
    public List<InspectionData> GetInspectionList(bool IncludeDeleted = false, int AssetID = 0)
    {
        try{
            List<InspectionData> inspectionList = _inspectionRepository.GetInspectionList(IncludeDeleted, AssetID);
            foreach (var inspection in inspectionList)
            {
                inspection.Asset = _assetRepository.GetAsset(inspection.AssetID);
                inspection.InspectionFiles = _inspectionFileRepository.GetInspectionFiles(inspection.Id);
                if(inspection.InspectionFiles != null)
                {
                    foreach (var item in inspection.InspectionFiles)
                    {
                        item.FilePath = SharedEnvironment.app_path.Replace("/", "") + "/" + item.FilePath;
                    }
                }
            }
            return inspectionList;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public InspectionData AddInspection(InspectionClass inspection)
    {
        try{
            InspectionData inspectionData = _inspectionRepository.AddInspection(inspection);
            inspectionData.Asset = _assetRepository.GetAsset(inspectionData.AssetID);
            inspectionData.InspectionFiles = _inspectionFileRepository.GetInspectionFiles(inspectionData.Id);
            if(inspectionData.InspectionFiles != null)
            {
                foreach (var item in inspectionData.InspectionFiles)
                {
                    item.FilePath = SharedEnvironment.app_path.Replace("/", "") + "/" + item.FilePath;
                }
            }
            return inspectionData;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public InspectionData UpdateInspection(InspectionClass inspection)
    {
        try{
            InspectionData inspectionData = _inspectionRepository.UpdateInspection(inspection);
            inspectionData.Asset = _assetRepository.GetAsset(inspectionData.AssetID);
            inspectionData.InspectionFiles = _inspectionFileRepository.GetInspectionFiles(inspectionData.Id);
            if(inspectionData.InspectionFiles != null)
            {
                foreach (var item in inspectionData.InspectionFiles)
                {
                    item.FilePath = SharedEnvironment.app_path.Replace("/", "") + "/" + item.FilePath;
                }
            }
            return inspectionData;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public InspectionData DeleteInspection(InspectionClass inspection)
    {
        try{
            return _inspectionRepository.DeleteInspection(inspection);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    private ToolImportClass MapInspectionRegister(List<Dictionary<string, string>> datas)
    {
        ToolImportClass toolImport = new();
        List<string> failedRecords = [];
        List<AssetData> assetList = _assetRepository.GetAssetList();
        List<InspectionMethodData> inspectionMethodList = _inspectionMethodRepository.GetInspectionMethodList();
        List<InspectionEffectivenessData> inspectionEffectivenessList = _inspectionEffectivenessRepository.GetInspectionEffectivenessList();
        List<CurrentConditionLimitStateData> currentConditionLimitStateList = _currentConditionLimitStateRepository.GetCurrentConditionLimitStateList();
        List<Dictionary<string, string>> finalresult = [];
        foreach (var records in datas)
        {
            Dictionary<string, string> result = [];
            foreach (var record in records){
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
                    || mappedKey.Equals("InspectionMethodID")
                    || mappedKey.Equals("InspectionDate")
                    || mappedKey.Equals("InspectionEffectivenessID")
                    || mappedKey.Equals("CurrentConditionLeakeageToAtmosphereID")
                    || mappedKey.Equals("CurrentConditionFailureOfFunctionID")
                    || mappedKey.Equals("CurrentConditionPassingAcrossValveID")
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
                    else if (mappedKey.Equals("InspectionMethodID"))
                    {
                        foreach (var inspectionMethod in inspectionMethodList)
                        {
                            if (inspectionMethod.InspectionMethod == null)
                            {
                                continue;
                            }
                            if (inspectionMethod.InspectionMethod.ToLower().Equals(value))
                            {
                                mappedValue = inspectionMethod.Id.ToString();
                                break;
                            }
                        }
                    }
                    else if (mappedKey.Equals("InspectionDate"))
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
                    else if (mappedKey.Equals("InspectionEffectivenessID"))
                    {
                        foreach (var inspectionEffectiveness in inspectionEffectivenessList)
                        {
                            if (inspectionEffectiveness.Effectiveness == null)
                            {
                                continue;
                            }
                            if (inspectionEffectiveness.Effectiveness.ToLower().Equals(value))
                            {
                                mappedValue = inspectionEffectiveness.Id.ToString();
                                break;
                            }
                        }
                    }
                    else if (mappedKey.Equals("CurrentConditionLeakeageToAtmosphereID"))
                    {
                        foreach (var currentConditionLimitState in currentConditionLimitStateList)
                        {
                            if (currentConditionLimitState.CurrentConditionLimitState == null)
                            {
                                continue;
                            }
                            if (currentConditionLimitState.CurrentConditionLimitState.ToLower().Equals(value))
                            {
                                mappedValue = currentConditionLimitState.Id.ToString();
                                break;
                            }
                        }
                    }
                    else if (mappedKey.Equals("CurrentConditionFailureOfFunctionID"))
                    {
                        foreach (var currentConditionLimitState in currentConditionLimitStateList)
                        {
                            if (currentConditionLimitState.CurrentConditionLimitState == null)
                            {
                                continue;
                            }
                            if (currentConditionLimitState.CurrentConditionLimitState.ToLower().Equals(value))
                            {
                                mappedValue = currentConditionLimitState.Id.ToString();
                                break;
                            }
                        }
                    }
                    else if (mappedKey.Equals("CurrentConditionPassingAcrossValveID"))
                    {
                        foreach (var currentConditionLimitState in currentConditionLimitStateList)
                        {
                            if (currentConditionLimitState.CurrentConditionLimitState == null)
                            {
                                continue;
                            }
                            if (currentConditionLimitState.CurrentConditionLimitState.ToLower().Equals(value))
                            {
                                mappedValue = currentConditionLimitState.Id.ToString();
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
            "inspection date\n(dd/mm/yyyy)" => "InspectionDate",
            "inspection method" => "InspectionMethodID",
            "inspection effectiveness" => "InspectionEffectivenessID",
            "inspection description" => "InspectionDescription",
            "current condition leakage to atmosphere" => "CurrentConditionLeakeageToAtmosphereID",
            "current condition failure of function" => "CurrentConditionFailureOfFunctionID",
            "current condition passing across valve" => "CurrentConditionPassingAcrossValveID",
            "function condition" => "FunctionCondition",
            "test pressure if any" => "TestPressureIfAny",
            _ => "",
        };
        return result;
    }
    public InspectionData GetLastAssetInspection(int AssetID)
    {
        try{
            InspectionData? inspectionData = _inspectionRepository
                .GetInspectionList(false, AssetID)
                .OrderByDescending(x => DateTime.ParseExact(x.InspectionDate ?? "01-01-0001", "dd-MM-yyyy", CultureInfo.InvariantCulture))
                .FirstOrDefault() ?? throw new Exception("Inspection not found");
            return inspectionData;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public List<InspectionFileData> GetInspectionFiles(int inspectionId)
    {
        try{
            List<InspectionFileData> inspectionFiles = _inspectionFileRepository.GetInspectionFiles(inspectionId);
            foreach (var item in inspectionFiles)
            {
                item.FilePath = SharedEnvironment.app_path.Replace("/", "") + "/" + item.FilePath;
            }
            return inspectionFiles;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public bool AddInspectionFile(InspectionFileClass inspectionFile)
    {
        try{
            return _inspectionFileRepository.AddInspectionFile(inspectionFile);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public bool DeleteInspectionFiles(List<InspectionFileClass> inspectionFiles)
    {
        try{
            return _inspectionFileRepository.DeleteInspectionFiles(inspectionFiles);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public Dictionary<string, string> ImportInspection(List<Dictionary<string, string>> data, int CreatedBy)
    {
        ToolImportClass toolImport = MapInspectionRegister(data);
        if(toolImport.MappedRecords == null || toolImport.MappedRecords.Count == 0)
        {
            throw new Exception("Failed to import inspection data");
        }
        List<Dictionary<string, string>> result = toolImport.MappedRecords;
        int total = 0;
        int success = 0;
        int failed = 0;
        List<string> failedDatas = [];

        foreach(var item in result){
            if(item == null) continue;
            total++;
            InspectionClass? inspection = null;
            try{
                string json = JsonConvert.SerializeObject(item);
                inspection = JsonConvert.DeserializeObject<InspectionClass>(json);
                if(inspection == null) continue;
                inspection.IsDeleted = false;
                inspection.CreatedBy = CreatedBy;
                inspection.CreatedAt = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString());
                InspectionData? inspectionData = _inspectionRepository.AddInspection(inspection);
            }
            catch(Exception e){
                LogData log = new(){
                    Module = "Import Inspection",
                    CreatedBy = CreatedBy,
                    CreatedAt = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString()),
                    Message = e.Message,
                    Data = JsonConvert.SerializeObject(item)
                };
                _logRepository.AddLog(log);
                failed++;
                failedDatas.Add(inspection?.InspectionDate ?? "");
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
                message += " with Inspection Date: " + failedData + ".";
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