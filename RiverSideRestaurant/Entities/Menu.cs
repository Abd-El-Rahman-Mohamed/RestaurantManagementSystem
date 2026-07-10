using System.Text;
using RiverSideRestaurant.Extensions;
using RiverSideRestaurant.Contracts;
using RiverSideRestaurant.Enums;

namespace RiverSideRestaurant.Entities;

public class Menu : IDisplayable
{
    private static readonly List<MenuItem> _items = [];
    public IReadOnlyList<MenuItem> Items => _items;

    public Menu() { }
    
    public MenuItem AddMenuItem(string name, ItemCategory category, decimal price, int quantity, string? description = null)
    {
        var menuItem = new MenuItem(name, category, price, quantity, description);
        
        _items.Add(menuItem);
        
        return menuItem;
    }
    
    public MenuItem AddMenuItem(MenuItem menuItem)
    {
        _items.Add(menuItem);
        
        return menuItem;
    }
    
    public List<MenuItem> FindItemsByCategory(ItemCategory category)
    { 
        List<MenuItem> menuItemsByCategory = [];

        foreach (var item in Items)
            if (item.Category == category)
                menuItemsByCategory.Add(item);

        return menuItemsByCategory;
    }
    
    public List<MenuItem> FindAvailableItems()
    { 
        List<MenuItem> availableMenuItems = [];

        foreach (var item in Items)
            if (item.Quantity > 0)
                availableMenuItems.Add(item);

        return availableMenuItems;
    }
    
    public MenuItem? FindItemById(string id)
    {
        id = id.NormalizeId();
        MenuItem? returnedItem = null;
        
        foreach (var item in Items)
            if (item.Id == id)
                 returnedItem = item;
        
        return returnedItem;
    }

    public string DisplayDetails()
    {
        StringBuilder menuItemsStringBuilder = new StringBuilder();
        
        foreach (var item in Items)
        {
            menuItemsStringBuilder.Append(item.DisplayDetails());
            menuItemsStringBuilder.Append("\n");
        }

        return menuItemsStringBuilder.ToString();
    }
}