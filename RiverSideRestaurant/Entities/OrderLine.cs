namespace RiverSideRestaurant.Entities;

public struct OrderLine(MenuItem menuItem, uint quantity)
{
    public MenuItem MenuItem { get; private set; } = menuItem;
    public uint Quantity { get; private set; } = quantity;
}