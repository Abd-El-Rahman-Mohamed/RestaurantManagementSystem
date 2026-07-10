using RiverSideRestaurant.Enums;
using RiverSideRestaurant.Contracts;

namespace RiverSideRestaurant.Entities;

public class MenuItem : IDisplayable
{
    private static int _counter = 0;
    
    public string Id { get; private set; } = null!;

    public string Name { get; private set; } = null!;

    public ItemCategory Category { get; private set; }

    public string? Description { get; private set; } = null!;

    public decimal Price { get; private set; }
    
    public int Quantity { get; private set; }

    public MenuItem(string name, ItemCategory category, decimal price, int quantity, string? description = null)
    {
        Id = $"ITEM-{++_counter:D3}";
        Name = name;
        Category = category;
        Price = price;
        Quantity = quantity;
        Description = description ?? "No Description";
    }

    public void AddQuantity(uint quantityToAdd)
    {
        Quantity += (int)quantityToAdd;
    }
    
    public void DeductQuantity(uint quantityToDeduct)
    {
        Quantity -= (int)quantityToDeduct;
    }

    public string DisplayDetails()
        => $"[{Id}] {Name} | {Category} | {Price} EGP | {(Quantity > 0 ? $"Remaining: {Quantity}" : "OUT OF STOCK")}";
}