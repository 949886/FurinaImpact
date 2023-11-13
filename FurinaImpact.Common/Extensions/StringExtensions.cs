namespace FurinaImpact.Common.Extensions;
public static class StringExtensions
{
    public static uint GetStableHash(this string str)
    {
        uint hash = 0;

        for (int i = 0; i < str.Length; i++)
        {
            hash = ((str[i] + 131 * hash) & 0xFFFFFFFF) >> 0;
        }
        return hash;
    }
}
