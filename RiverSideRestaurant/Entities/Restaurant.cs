using System.Text;
using ConsoleTheme;
using RiverSideRestaurant.Extensions;
using RiverSideRestaurant.Enums;
using RiverSideRestaurant.Contracts;

namespace RiverSideRestaurant.Entities;

public class Restaurant : IDisplayable
{
    private readonly List<Order> _orders = [];
    public IReadOnlyList<Order> Orders => _orders;

    private readonly List<Staff> _staffs = [];
    public IReadOnlyList<Staff> Staffs => _staffs;
    
    private readonly int _counter = 0;
    
    public string Id { get; private set; }

    public string Name { get; private set; }

    public string Address { get; private set; }

    private string Phone { get; set; }

    private string OpeningHours { get; set; }

    private string ManagerName { get; set; }

    public Restaurant(string name, string address, string phone, string openingHours, Manager manager)
    {
        Id = $"REST-{++_counter:D2}";
        Name = name;
        Address = address;
        Phone = phone;
        OpeningHours = openingHours;
        ManagerName = manager.FullName;
    }
    
    public Order PlaceOrder(byte tableNumber, Server server, List<OrderLine> orderLines)
    {
        Order order = new Order(tableNumber, server, orderLines);

        _orders.Add(order);

        return order;
    }
    
    public bool IsTableNumberAvailable(byte tableNumber)
    {
        bool available = true;
        
        foreach (var orderItem in Orders)
        {
            if (orderItem.TableNumber == tableNumber &&
                orderItem.Status is not (OrderStatus.Served or OrderStatus.Cancelled))
                available = false;
        }
        
        return available;
    }

    public Staff AddStaff(string fullName, string phone, StaffRole role, DateOnly? hireDate = null, decimal? monthlySalary = null)
    {
        Staff staff;
        if (role == StaffRole.Manager)
            staff = new Manager(fullName, phone, hireDate, monthlySalary);
        else if (role == StaffRole.Chef)
            staff = new Chef(fullName, phone, hireDate);
        else
            staff = new Server(fullName, phone);
        
        _staffs.Add(staff);
        
        return staff;
    }
    
    public Staff AddStaff(Staff staff)
    {
        _staffs.Add(staff);
        
        return staff;
    }
    
    public Staff? FindStaffById(string id)
    {
        id = id.NormalizeId();
        
        Staff? returnedStaff = null;
        
        foreach(var staff in Staffs)
            if (staff.Id == id)
                returnedStaff = staff;

        return returnedStaff;
    }
    
    public string ViewAllStaff()
    {
        StringBuilder staffDetails = new StringBuilder();
            
        foreach (var staff in _staffs)
        {
            staffDetails.Append(staff.DisplayDetails());
            staffDetails.Append("\n");
        }

        return staffDetails.ToString();
    }
    
    public string ViewAllOrders()
    {
        StringBuilder ordersDetails = new StringBuilder();
            
        foreach (var order in _orders)
        {
            ordersDetails.Append(order.DisplayDetails());
            ordersDetails.Append("\n");
        }

        return ordersDetails.ToString();
    }
    
    public string DailySalesSummary()
    {
        List<Order> servedOrders = FindOrdersByStatus(OrderStatus.Served);
        
        int ordersCount = servedOrders.Count();
        decimal revenue = 0;
        int outOfStockItemsCount = 0;
        
        foreach (var order in servedOrders)
            revenue += order.Total;

        Menu menu = new Menu();
        foreach (var menuItem in menu.Items)
            if (menuItem.Quantity == 0)
                outOfStockItemsCount++;

        return $"Served orders: {ordersCount} | Revenue: {revenue:F2} EGP | Out of stock: {outOfStockItemsCount} item(s)";
    }
    
    public Order? FindOrderById(string id)
    {
        id = id.NormalizeId();
        
        Order? returnedOrder = null;
        
        foreach(var order in Orders)
            if (order.Id == id)
                returnedOrder = order;

        return returnedOrder;
    }
    
    public List<Order> FindOrdersByStatus(OrderStatus status)
    { 
        List<Order> ordersByStatus = [];

        foreach (var order in Orders)
            if (order.Status == status)
                ordersByStatus.Add(order);

        return ordersByStatus;
    }
    
    

    public string DisplayDetails()
        => $"""
            ID : {Id}
            Name : {Name}
            Address : {Address}
            Phone : {Phone}
            Opening Hours : {OpeningHours}
            Manager Name : {ManagerName}
            """;
}