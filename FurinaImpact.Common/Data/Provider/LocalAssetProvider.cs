using System.Text.Json;

namespace FurinaImpact.Common.Data.Provider;
internal sealed class LocalAssetProvider : IAssetProvider
{
    private const string ExcelDirectory = "assets/excel/";

    public JsonDocument GetExcelTableJson(string assetName)
    {
        string filePath = string.Concat(ExcelDirectory, assetName);
        using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read);

        return JsonDocument.Parse(fileStream);
    }
}
