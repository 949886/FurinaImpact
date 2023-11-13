using System.Text.Json;

namespace FurinaImpact.Common.Data.Provider;
public interface IAssetProvider
{
    JsonDocument GetExcelTableJson(string assetName);
    IEnumerable<string> EnumerateAvatarConfigFiles();
    JsonDocument GetFileAsJsonDocument(string fullPath);
}
