using RiverSideRestaurant.Enums;

namespace RiverSideRestaurant.Entities;

public class Chef(string fullName, string phone, DateOnly? hireDate = null) : Staff(fullName, phone, StaffRole.Chef)
{
    public DateOnly? HireDate { get; private set; } = hireDate;
    
    public override string DisplayDetails()
        => $"ID : {Id} | Name : {FullName} | Phone : {Phone} | Role : {Role}  | Hire Date : {HireDate?.ToString() ?? "N/A"}";
}