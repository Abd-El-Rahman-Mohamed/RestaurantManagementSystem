using System.Numerics;
using ConsoleTheme;
using RiverSideRestaurant.Entities;
using RiverSideRestaurant.Enums;

namespace RiverSideRestaurant.Services;

public class DisplayService
{
    public void ViewRestaurantInfo(Restaurant restaurant)
    {
        ThemeHelper.PrintHeader("Restaurant INFO");
        Console.WriteLine(restaurant.DisplayDetails());
    }
    
    public void ViewAllStaff(Restaurant restaurant)
    {
        ThemeHelper.PrintHeader("ALL Staff");

        IReadOnlyList<Staff> staffs = restaurant.Staffs;

        for (int i = 0; i < staffs.Count; i++)
        {
            string header = staffs[i] switch
            {
                Manager => "MANAGER PROFILE",
                Chef => "CHEF PROFILE",
                Server => "SERVER PROFILE"
            };

            ThemeHelper.PrintSectionTitle(header);
            Console.WriteLine(staffs[i].DisplayDetails());
        }
    }
    
    public void ViewAvailableMenuItems(Menu menu)
    {
        ThemeHelper.PrintHeader("AVAILABLE MENU ITEMS");

        List<MenuItem> availableMenuItems = menu.FindAvailableItems();

        if (availableMenuItems.Count == 0)
        {
            ThemeHelper.PrintWarning("No available menu items found.");
            return;
        }

        foreach (var availableMenuItem in availableMenuItems)
            Console.WriteLine(availableMenuItem.DisplayDetails());
    }
    
    public void ViewMenuStock(Menu menu)
    {
        ThemeHelper.PrintHeader("MENU STOCK");

        var menuItems = menu.Items;

        foreach (var menuItem in menuItems)
            Console.WriteLine(menuItem.DisplayDetails());
    }
    
    public void ViewAllOrders(Restaurant restaurant)
    {
        ThemeHelper.PrintHeader("ALL ORDERS");
    
        if (restaurant.Orders.Count == 0)
        {
            ThemeHelper.PrintWarning("No orders found.");
            return;
        }
    
        foreach (var order in restaurant.Orders)
            Console.WriteLine(order.DisplayDetails() + "\n");
    }
    
    public void ViewOrderPlacementSuccess(Order order)
    {
        ThemeHelper.PrintSuccess(
            $"Order [{order.Id}] created for Table {order.TableNumber}, Total {order.Total} EGP, Status: [{order.Status}]."
        );
    }
    
    // Active orders are orders that are not Served nor Cancelled
    public void ViewActiveOrdersForLoggedInServer(Restaurant restaurant, Server server)
    {
        List<Order> activeOrders = restaurant.FindOrdersByStatus(OrderStatus.Pending);
        activeOrders.AddRange(restaurant.FindOrdersByStatus(OrderStatus.Preparing));
        activeOrders.AddRange(restaurant.FindOrdersByStatus(OrderStatus.Ready));

        List<Order> activeOrdersForLoggedInServer = [];
        
        ThemeHelper.PrintHeader("ACTIVE ORDERS");
            
        foreach (var order in activeOrders)
            if (order.Server.Equals(server))
                activeOrdersForLoggedInServer.Add(order);
        
        foreach (var order in activeOrdersForLoggedInServer)
            Console.WriteLine(order.DisplayDetails() + "\n");
    }
    
    public void ViewPendingOrdersForLoggedInServer(Restaurant restaurant, Server server)
    {
        List<Order> pendingOrders = restaurant.FindOrdersByStatus(OrderStatus.Pending);

        List<Order> activeOrdersForLoggedInServer = [];
        
        ThemeHelper.PrintHeader("Active Orders");
            
        foreach (var order in pendingOrders)
            if (order.Server.Equals(server))
                activeOrdersForLoggedInServer.Add(order);
        
        foreach (var order in activeOrdersForLoggedInServer)
            Console.WriteLine(order.DisplayDetails() + "\n");
    }
    
    public void ViewReadyOrders(Restaurant restaurant)
    {
        List<Order> readyOrders = restaurant.FindOrdersByStatus(OrderStatus.Ready);
        
        ThemeHelper.PrintHeader("READY ORDERS");
            
        foreach (var order in readyOrders)
            Console.WriteLine(order.DisplayDetails() + "\n");
    }
    
    public void ViewKitchenQueue(Restaurant restaurant)
    {
        List<Order> pendingOrders = restaurant.FindOrdersByStatus(OrderStatus.Pending);
        
        ThemeHelper.PrintHeader("CHEF QUEUE");
            
        foreach (var order in pendingOrders)
            Console.WriteLine(order.DisplayDetails() + "\n");
    }
    
    public void ViewOrdersInProgress(Restaurant restaurant)
    {
        List<Order> preparingOrders = restaurant.FindOrdersByStatus(OrderStatus.Preparing);
        
        ThemeHelper.PrintHeader("ORDERS IN PROGRESS");
            
        foreach (var order in preparingOrders)
                Console.WriteLine(order.DisplayDetails() + "\n");
    }
    
    public void ViewPendingOrders(Restaurant restaurant)
    {
        List<Order> pendingOrders = restaurant.FindOrdersByStatus(OrderStatus.Pending);
        
        ThemeHelper.PrintHeader("PENDING ORDERS");
            
        foreach (var order in pendingOrders)
                Console.WriteLine(order.DisplayDetails() + "\n");
    }
    
    public void ViewPreparingOrders(Restaurant restaurant)
    {
        List<Order> preparingOrders = restaurant.FindOrdersByStatus(OrderStatus.Preparing);
        
        ThemeHelper.PrintHeader("PREPARING ORDERS");
            
        foreach (var order in preparingOrders)
            Console.WriteLine(order.DisplayDetails() + "\n");
    }
    
    public void ViewOrderServedSuccess(Order order)
    {
        ThemeHelper.PrintSuccess(
            $"Order [{order.Id}] served. Bill Total: {order.Total:F2} EGP."
        );
    }
    
    public void ViewDailySalesSummary(Restaurant restaurant)
    {
        ThemeHelper.PrintHeader("DAILY SALES SUMMARY");
        Console.WriteLine(restaurant.DailySalesSummary());
    }
}