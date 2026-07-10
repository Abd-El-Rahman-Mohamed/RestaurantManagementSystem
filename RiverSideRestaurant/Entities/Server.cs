using RiverSideRestaurant.Enums;

namespace RiverSideRestaurant.Entities;

public class Server(string fullName, string phone) : Staff(fullName, phone, StaffRole.Server)
{
    
    public override string DisplayDetails()
        => $"ID : {Id} | Name : {FullName} | Phone : {Phone} | Role : {Role}";
}