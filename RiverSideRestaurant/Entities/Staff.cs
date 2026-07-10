using RiverSideRestaurant.Enums;
using RiverSideRestaurant.Contracts;

namespace RiverSideRestaurant.Entities;

public abstract class Staff : IDisplayable
{
    private static int _counter = 0;

    public string Id { get; protected set; } = null!;

    public string FullName { get; protected set; } = null!;

    public string Phone { get; protected set; } = null!;

    public StaffRole Role { get; protected set; }

    protected Staff(string fullName, string phone, StaffRole role)
    {
        Id = $"EMP-{++_counter:D3}";
        FullName = fullName;
        Phone = phone;
        Role = role;
    }

    public abstract string DisplayDetails();
}