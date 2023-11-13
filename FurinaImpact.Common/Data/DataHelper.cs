using System.Collections.Immutable;
using FurinaImpact.Common.Data.Excel;

namespace FurinaImpact.Common.Data;
public class DataHelper
{
    private readonly ImmutableDictionary<string, uint> _avatarNameToIdTable;

    public DataHelper(ExcelTableCollection excelTables)
    {
        _avatarNameToIdTable = BuildAvatarNameToIdTable(excelTables);
    }

    public bool TryResolveAvatarIdByName(string avatarName, out uint id)
    {
        return _avatarNameToIdTable.TryGetValue(avatarName, out id);
    }

    private static ImmutableDictionary<string, uint> BuildAvatarNameToIdTable(ExcelTableCollection excelTables)
    {
        ImmutableDictionary<string, uint>.Builder builder = ImmutableDictionary.CreateBuilder<string, uint>();
        ExcelTable avatarTable = excelTables.GetTable(ExcelType.Avatar);

        for (int i = 0; i < avatarTable.Count; i++)
        {
            AvatarExcel excel = avatarTable.GetItemAt<AvatarExcel>(i);

            string avatarName = excel.IconName[(excel.IconName.LastIndexOf('_') + 1)..];
            builder.TryAdd(avatarName, excel.Id);
        }

        return builder.ToImmutable();
    }
}
