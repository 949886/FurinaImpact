using System.Collections.Immutable;
using System.Reflection;
using System.Text.Json;
using FurinaImpact.Common.Data.Excel.Attributes;
using FurinaImpact.Common.Data.Provider;
using Microsoft.Extensions.Logging;

namespace FurinaImpact.Common.Data.Excel;
public class ExcelTableCollection
{
    private readonly ImmutableDictionary<ExcelType, ExcelTable> _tables;

    public ExcelTableCollection(IAssetProvider assetProvider, ILogger<ExcelTableCollection> logger)
    {
        _tables = LoadTables(assetProvider);
        logger.LogInformation("Loaded {count} excel tables", _tables.Count);
    }

    public TExcel? GetExcel<TExcel>(ExcelType type, uint id) where TExcel : ExcelItem
        => _tables[type].GetItemById<TExcel>(id);

    public ExcelTable GetTable(ExcelType type) => _tables[type];

    private static ImmutableDictionary<ExcelType, ExcelTable> LoadTables(IAssetProvider assetProvider)
    {
        ImmutableDictionary<ExcelType, ExcelTable>.Builder tables = ImmutableDictionary.CreateBuilder<ExcelType, ExcelTable>();

        IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes()
                                  .Where(type => type.GetCustomAttribute<ExcelAttribute>() != null);

        foreach (Type type in types)
        {
            ExcelAttribute attribute = type.GetCustomAttribute<ExcelAttribute>()!;

            JsonDocument tableJson = assetProvider.GetExcelTableJson(attribute.AssetName);
            tables.Add(attribute.Type, new ExcelTable(tableJson, type));
        }

        return tables.ToImmutable();
    }
}
