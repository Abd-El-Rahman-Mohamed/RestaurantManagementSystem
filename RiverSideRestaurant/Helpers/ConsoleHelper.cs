using ConsoleTheme;
using RiverSideRestaurant.Entities;

namespace RiverSideRestaurant.Helpers;

public static class ConsoleHelper
{
    public static void ViewLoginMenu(Restaurant restaurant)
    {
        ThemeHelper.PrintHeader("         LOGIN         ");
        for (int i = 0; i < restaurant.Staffs.Count; i++)
            ThemeHelper.PrintOption($"{i+1}. {restaurant.Staffs[i].DisplayDetails()}");
        Console.WriteLine(new string('-', 22));
        ThemeHelper.PrintOption("5. Exit");
        Console.WriteLine(new string('=', 22));
        Console.WriteLine("Enter your choice: ");
    }
    
    public static void ViewServerMenu()
    {
        ThemeHelper.PrintHeader("         SERVER MENU         ");
        ThemeHelper.PrintOption("1. View Menu");
        ThemeHelper.PrintOption("2. View Menu By Category");
        ThemeHelper.PrintOption("3. Place New Order");
        ThemeHelper.PrintOption("4. View My Active Orders");
        ThemeHelper.PrintOption("5. View Ready Orders");
        ThemeHelper.PrintOption("6. Mark Order as Served");
        ThemeHelper.PrintOption("7. Cancel Order");
        Console.WriteLine(new string('-', 22));
        ThemeHelper.PrintOption("0. Logout");
        Console.WriteLine(new string('=', 22));
        Console.WriteLine("Enter your choice: ");
    }
    
    public static void ViewChefMenu()
    {
        ThemeHelper.PrintHeader("         CHEF MENU         ");
        ThemeHelper.PrintOption("1. View Kitchen Queue");
        ThemeHelper.PrintOption("2. View Orders in Progress");
        ThemeHelper.PrintOption("3. Start Preparing an Order");
        ThemeHelper.PrintOption("4. Mark Order as Ready");
        ThemeHelper.PrintOption("5. View Menu Stock");
        Console.WriteLine(new string('-', 22));
        ThemeHelper.PrintOption("0. Logout");
        Console.WriteLine(new string('=', 22));
        Console.WriteLine("Enter your choice: ");
    }
    
    public static void ViewManagerMenu()
    {
        ThemeHelper.PrintHeader("         MANAGER MENU         ");
        ThemeHelper.PrintOption("1. Restaurant Information");
        ThemeHelper.PrintOption("2. View All Staff");
        ThemeHelper.PrintOption("3. View Full Menu and Stock");
        ThemeHelper.PrintOption("4. View All Orders");
        ThemeHelper.PrintOption("5. Restock Menu Item");
        ThemeHelper.PrintOption("6. Cancel Order");
        ThemeHelper.PrintOption("7. Daily Sales Summary");
        ThemeHelper.PrintOption("8. Place Order");
        Console.WriteLine(new string('-', 22));
        ThemeHelper.PrintOption("0. Logout");
        Console.WriteLine(new string('=', 22));
        Console.WriteLine("Enter your choice: ");
    }
}