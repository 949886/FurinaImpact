using System.Collections.Immutable;
using System.Text.Json;

namespace FurinaImpact.Common.Data.Excel;
public class ExcelTable
{
    public int Count => _items.Length;
    private readonly ImmutableArray<ExcelItem> _items;

    public ExcelTable(JsonDocument document, Type type)
    {
        _items = LoadData(document, type);
    }

    private static ImmutableArray<ExcelItem> LoadData(JsonDocument document, Type type)
    {
        ImmutableArray<ExcelItem>.Builder items = ImmutableArray.CreateBuilder<ExcelItem>();

        foreach (JsonElement element in document.RootElement.EnumerateArray())
        {
            if (element.ValueKind != JsonValueKind.Object)
                throw new ArgumentException($"ExcelTable::LoadData - expected an object, got {element.ValueKind}");

            ExcelItem deserialized = (element.Deserialize(type) as ExcelItem)!;
            items.Add(deserialized);
        }

        return items.ToImmutable();
    }

    public TExcel GetItemAt<TExcel>(int index) where TExcel : ExcelItem
    {
        return (_items[index] as TExcel)!;
    }

    public TExcel? GetItemById<TExcel>(uint id) where TExcel : ExcelItem
    {
        foreach (ExcelItem item in _items)
        {
            if (item.ExcelId == id)
                return item as TExcel;
        }

        return null;
    }
}
