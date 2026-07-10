using ConsoleTheme;
using RiverSideRestaurant.Enums;
using RiverSideRestaurant.Extensions;
using RiverSideRestaurant.Entities;

namespace RiverSideRestaurant.Services;

public class RestaurantService(Restaurant restaurant, DisplayService display, Menu menu)
{
    public void HandleOrderPlacement()
    {
        string tableNumberString = ThemeHelper.Prompt("Table Number");
        Server server;
        bool isTableNumberParsed = byte.TryParse(tableNumberString, out byte tableNumber);

        if (!restaurant.IsTableNumberAvailable(tableNumber))
            throw new InvalidOperationException($"Table [{tableNumber}] already has an active order.");
        else if(tableNumber is < 1 or > 20)
            throw new InvalidOperationException($"Table number must be between 1 and 20");
                    
        string serverId = ThemeHelper.Prompt("Server Id");
        if (!IsServerIdValid(serverId)) 
            throw new InvalidOperationException("Server Id is not valid!");
        
        server = (Server)restaurant.FindStaffById(serverId)!;
        
        List<OrderLine> orderLines = [];

        string option = "";
        do
        {
            orderLines.Add(TakeOrderItem());
            
            ThemeHelper.PrintOption("1. Add another item");
            ThemeHelper.PrintOption("2. Place Order");
            option = ThemeHelper.Prompt("your choice");
        } while (option != "2");
        
        Order order = restaurant.PlaceOrder(tableNumber, server, orderLines);
        display.ViewOrderPlacementSuccess(order);
    }

    private bool IsServerIdValid(string serverId)
    {
        serverId = serverId.NormalizeId();
        foreach (var staff in restaurant.Staffs)
            if (staff.Id == serverId && staff.Role == StaffRole.Server)
                return true;

        return false;
    }

    private OrderLine TakeOrderItem()
    {
        OrderLine orderLine = new OrderLine();

        string menuItemId = ThemeHelper.Prompt("Item ID").NormalizeId();
        string quantityToOrderString = ThemeHelper.Prompt("Quantity");
        bool isQuantityToOrderParsed = uint.TryParse(quantityToOrderString, out uint quantityToOrder);
        
        if (!isQuantityToOrderParsed)
            ThemeHelper.PrintError("Quantity to add must be an integer value!");

        MenuItem? menuItem = menu.FindItemById(menuItemId);
        if (menuItem is null)
            throw new InvalidOperationException("Menu item not found!");
        
        if (menuItem.Quantity < quantityToOrder)
            throw new InvalidOperationException("Menu Item quantity is smaller than the ordered quantity");
            
        menuItem.DeductQuantity(quantityToOrder);
        orderLine = new OrderLine(menuItem, quantityToOrder);

        return orderLine;
    }

    public void HandleCancellation()
    {
        display.ViewPendingOrders(restaurant);
        if (AuthService.CurrentUser!.Role == StaffRole.Manager)
            display.ViewPreparingOrders(restaurant);
        
        string orderId = ThemeHelper.Prompt("the Order ID").NormalizeId();
        
        Order? order = restaurant.FindOrderById(orderId);
        if (order is null)
            throw new InvalidOperationException("Order not found!");
        else
        {
            order.CancelOrder();

            foreach (var menuItem in order.OrderLines)
                menuItem.MenuItem.AddQuantity(menuItem.Quantity);
            
            ThemeHelper.PrintSuccess($"Order {order.Id} cancelled for Table {order.TableNumber}.");
        }
        
    }
    
    public void HandleServing()
    {
        display.ViewReadyOrders(restaurant);
        
        string orderId = ThemeHelper.Prompt("The Order ID").NormalizeId();
        
        Order? order = restaurant.FindOrderById(orderId);
        if (order is null)
            throw new InvalidOperationException("Order not found!");
            
        order.MarkAsServed();
        
        display.ViewOrderServedSuccess(order);
    }
    
    public void HandlePreparing()
    {
        display.ViewPendingOrders(restaurant);
        
        string orderId = ThemeHelper.Prompt("The Order ID").NormalizeId();
        
        Order? order = restaurant.FindOrderById(orderId);
        if (order is null)
            throw new InvalidOperationException("Order not found!");
            
        order.MarkAsPreparing();
    }
    
    public void HandleMakingReady()
    {
        display.ViewPreparingOrders(restaurant);
        
        string orderId = ThemeHelper.Prompt("The Order ID").NormalizeId();
        
        Order? order = restaurant.FindOrderById(orderId);
        if (order is null)
            throw new InvalidOperationException("Order not found!");
            
        order.MarkAsReady();
    }

    public void HandleRestock()
    {
        display.ViewMenuStock(menu);
        
        string menuItemId = ThemeHelper.Prompt("the Menu Item ID").NormalizeId();
        string quantityToAddString = ThemeHelper.Prompt("the Quantity to Add");
        bool isQuantityToAddParsed = uint.TryParse(quantityToAddString, out uint quantityToAdd);
        
        if (!isQuantityToAddParsed)
            throw new InvalidOperationException("Quantity to add must be an integer value!");

        MenuItem? menuItem = menu.FindItemById(menuItemId);
        if (menuItem is null)
            throw new InvalidOperationException("Menu item not found!");
        else
            menuItem.AddQuantity(quantityToAdd);
    }
    
    public void ViewMenuByCategory()
    {
        List<MenuItem> filteredItems = [];
        
        ThemeHelper.PrintOption("1. Appetizer");
        ThemeHelper.PrintOption("2. MainCourse");
        ThemeHelper.PrintOption("3. Drink");
        ThemeHelper.PrintOption("4. Dessert");
        
        string categoryString = ThemeHelper.Prompt("the Category to filter by");
        bool isCategoryParsed = Enum.TryParse(categoryString, out ItemCategory category);

        if (isCategoryParsed)
            filteredItems = menu.FindItemsByCategory(category);
        else
            throw new InvalidOperationException("Incorrect Category");

        foreach (var filteredItem in filteredItems)
            Console.WriteLine(filteredItem.DisplayDetails());
    }
}

/*
 //    private readonly Restaurant _restaurant = restaurant;
//    private readonly DisplayService _display = display;
 
 Deleted lines:
 
 public void ShowRestaurantInfo(Restaurant restaurant)
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
    
    public void ShowAvailableMenuItems(Menu menu)
    {
        ThemeHelper.PrintHeader("AVAILABLE MENU ITEMS");

        List<MenuItem> availableMenuItems = menu.FindAvailableItems();

        if (availableMenuItems.Count == 0)
        {
            ThemeHelper.PrintWarning("No available menu items found.");
            return;
        }

        for (int i = 0; i < availableMenuItems.Count; i++)
            Console.WriteLine(availableMenuItems[i].DisplayDetails());
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
            Console.WriteLine(order.DisplayDetails());
    }
 */