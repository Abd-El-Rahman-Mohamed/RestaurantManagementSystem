using System.Net.Security;
using ConsoleTheme;
using RiverSideRestaurant.Entities;
using RiverSideRestaurant.Enums;
using RiverSideRestaurant.Helpers;
using RiverSideRestaurant.Seeder;
using RiverSideRestaurant.Services;

namespace RiverSideRestaurant;

class Program
{
    static void Main(string[] args)
    {
        Menu menu = new Menu();
        Restaurant restaurant = DataSeeder.Seed(menu);
        var display = new DisplayService();
        var restaurantService = new RestaurantService(restaurant, display, menu);

        AuthService.HandleAuthorization(menu, restaurant, display, restaurantService);
    }

    public static void CloseSystemQuietly(uint seconds)
    {
        for (uint i = seconds; i > 0; i--)
        {
            Console.WriteLine();
            
            Console.WriteLine(i != 1
                ? $"Application will be closed in {i} seconds..."
                : $"Application will be closed in {i} second...");
            
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }
    }
}