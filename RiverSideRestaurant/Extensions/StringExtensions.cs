namespace RiverSideRestaurant.Extensions;

public static class StringExtensions
{
    public static string NormalizeId(this string id)
    {
        return id.ToUpperInvariant().Trim();
    }
}