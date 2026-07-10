using RiverSideRestaurant.Enums;

namespace RiverSideRestaurant.Entities;

public class Manager(string fullName, string phone, DateOnly? hireDate = null, decimal? monthlySalary = null) : Staff(fullName, phone, StaffRole.Manager)
{
    public DateOnly? HireDate { get; private set; } = hireDate;

    public decimal? MonthlySalary { get; private set; } = monthlySalary;

    public override string DisplayDetails()
        => $"ID : {Id} | Name : {FullName} | Phone : {Phone} | Role : {Role} | Hire Date : {HireDate?.ToString() ?? "N/A"} | Monthly Salary : {(string.IsNullOrWhiteSpace(MonthlySalary?.ToString()) ? "N/A" : (MonthlySalary.ToString() + " EGP"))}";
}