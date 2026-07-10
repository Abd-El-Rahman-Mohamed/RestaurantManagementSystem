using System.Text;
using RiverSideRestaurant.Services;
using RiverSideRestaurant.Enums;
using RiverSideRestaurant.Contracts;

namespace RiverSideRestaurant.Entities;

public class Order : IDisplayable, IOrderable
{
    private static int _counter = 1000;

    public List<OrderLine> OrderLines = [];

    public string Id { get; private set; }

    public byte TableNumber { get; private set; }

    public Server Server { get; private set; }
    public string ServerName { get; private set; }

    public OrderStatus Status { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    
    public decimal Total { get; private set; } 

    public Order(byte tableNumber, Server server, List<OrderLine> orderLines)
    {
        Id = $"ORD-{++_counter}";
        
        if(IsTableNumberValid(tableNumber))
            TableNumber = tableNumber;
        else
            throw new ArgumentOutOfRangeException(nameof(tableNumber), "Table number must be between 1 and 20");
        
        Server = server;
        
        ServerName = server.FullName;
        Status = OrderStatus.Pending;
        CreatedAt = DateTime.UtcNow;
        
        if (!IsMenuItemQuantityAvailable(orderLines))
            throw new InvalidOperationException("Menu Item quantity is smaller than the ordered quantity");

        Total = CalculateTotal(orderLines);

        OrderLines = orderLines;
    }

    private bool IsTableNumberValid(byte tableNumber)
    {
        if (tableNumber is < 1 or > 20)
            return false;

        return true;
    }

    private bool IsMenuItemQuantityAvailable(List<OrderLine> orderLines)
    {
        bool isAvailable = true;
        
        foreach (var orderLine in orderLines)
        {
            if (orderLine.MenuItem.Quantity < orderLine.Quantity)
                isAvailable = false;
        }

        return isAvailable;
    }

    private decimal CalculateTotal(List<OrderLine> orderLines)
    {
        decimal Total = 0;
        
        foreach (var orderLine in orderLines)
            Total += (orderLine.MenuItem.Price * orderLine.Quantity);

        return Total;
    }

    public void MarkAsPreparing()
    {
        if ((AuthService.CurrentUser?.Role != StaffRole.Chef) || (Status != OrderStatus.Pending))
            throw new InvalidOperationException("Chefs only can mark Pending Orders only as Preparing.");

        Status = OrderStatus.Preparing;
    }
    
    public void MarkAsReady()
    {
        if ((AuthService.CurrentUser?.Role != StaffRole.Chef) || (Status != OrderStatus.Preparing))
            throw new InvalidOperationException("Chefs only can mark Preparing Orders only as Ready.");
        
        Status = OrderStatus.Ready;
    }
    
    public void MarkAsServed()
    {
        if ((AuthService.CurrentUser?.Role != StaffRole.Server) || (Status != OrderStatus.Ready))
            throw new InvalidOperationException("Servers only can mark Ready Orders only as Served.");
        
        Status = OrderStatus.Served;
    }

    public void CancelOrder()
    {
        var role = AuthService.CurrentUser?.Role;

        if (role == StaffRole.Chef)
            throw new InvalidOperationException("Only Managers and Servers can cancel orders");
        
        if (role == StaffRole.Server && Status != OrderStatus.Pending)
            throw new InvalidOperationException($"Servers can cancel only Pending Orders, while the current order status is {Status}");

        if (role == StaffRole.Manager && Status is not (OrderStatus.Pending or OrderStatus.Preparing))
            throw new InvalidOperationException("Only Pending and Preparing orders can be cancelled");

        Status = OrderStatus.Cancelled;
    }

    public string InvoiceDetails()
    {
        StringBuilder orderedItems = new StringBuilder();
        for (int i = 0; i < OrderLines.Count; i++)
        {
            var menuItem = OrderLines[i];
            
            if (i != OrderLines.Count-1)
            {
                orderedItems.Append($"\t{menuItem.MenuItem.Name} | Quantity: {menuItem.Quantity} | Total Price: {menuItem.MenuItem.Price * menuItem.Quantity} EGP\n");
            }
            else
            {
                orderedItems.Append($"\t{menuItem.MenuItem.Name} | Quantity: {menuItem.Quantity} | Total Price: {menuItem.MenuItem.Price * menuItem.Quantity} EGP");
            }
        }

        orderedItems.Append("\n======================================= ");

        return orderedItems.ToString();
    }

    public string DisplayDetails()
        => $"""
            ID : {Id}
            Table Number : {TableNumber}
            Server Name : {ServerName}
            Status : {Status}
            Created At : {CreatedAt}
            Items: {InvoiceDetails()}
            Total : {Total} EGP
            """;
}