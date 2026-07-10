using ConsoleTheme;
using RiverSideRestaurant.Entities;
using RiverSideRestaurant.Helpers;
using RiverSideRestaurant.Enums;

namespace RiverSideRestaurant.Services;

public static class AuthService
{
    public static Staff? CurrentUser { get; private set; }
    
    public static void LogInAs(Staff staff)
        => CurrentUser = staff;
    
    public static void LogOut()
        => CurrentUser = null;

    public static void HandleAuthorization(Menu menu, Restaurant restaurant, DisplayService display, RestaurantService restaurantService)
    {
        bool authorizing = true;
        
        while (authorizing)
        {
            try {
                Console.Clear();
                ConsoleHelper.ViewLoginMenu(restaurant);

                string? choice = Console.ReadLine()?.Trim();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        LogInAs(restaurant.Staffs[0]);
                        ThemeHelper.PrintSuccess($"Logged in with {restaurant.Staffs[0].FullName} profile with {restaurant.Staffs[0].Role} role");
                        Console.WriteLine();
                        StartSession(menu, restaurant, display, restaurantService);
                        authorizing = false;
                        break;

                    case "2":
                        Console.Clear();
                        LogInAs(restaurant.Staffs[1]);
                        ThemeHelper.PrintSuccess($"Logged in with {restaurant.Staffs[1].FullName} profile with {restaurant.Staffs[1].Role} role");
                        Console.WriteLine();
                        StartSession(menu, restaurant, display, restaurantService);
                        authorizing = false;
                        break;

                    case "3":
                        Console.Clear();
                        LogInAs(restaurant.Staffs[2]);
                        ThemeHelper.PrintSuccess($"Logged in with {restaurant.Staffs[2].FullName} profile with {restaurant.Staffs[2].Role} role");
                        Console.WriteLine();
                        StartSession(menu, restaurant, display, restaurantService);
                        authorizing = false;
                        break;

                    case "4":
                        Console.Clear();
                        LogInAs(restaurant.Staffs[3]);
                        ThemeHelper.PrintSuccess($"Logged in with {restaurant.Staffs[3].FullName} profile with {restaurant.Staffs[3].Role} role");
                        Console.WriteLine();
                        StartSession(menu, restaurant, display, restaurantService);
                        authorizing = false;
                        break;
                    
                    case "5":
                        Console.Clear();
                        ThemeHelper.PrintSuccess("Good Bye");
                        Environment.Exit(1);
                        break;

                    default:
                        Console.Clear();
                        ThemeHelper.PrintError("Invalid Choice!");
                        break;
                }
            }
            catch (Exception ex)
            {
                ThemeHelper.PrintError(ex.Message);
            }
            
            Console.WriteLine("\n Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
    
    public static void StartSession(Menu menu, Restaurant restaurant, DisplayService display, RestaurantService restaurantService)
    {
        try
                {
                    switch (CurrentUser.Role)
                    {
                        case StaffRole.Server:
                            HandleServerSession(menu, restaurant, display, restaurantService);
                            break;
                        
                        case StaffRole.Chef:
                            HandleChefSession(menu, restaurant, display, restaurantService);
                            break;
                        
                        case StaffRole.Manager:
                            HandleManagerSession(menu, restaurant, display, restaurantService);
                            break;
        
                        default:
                            ThemeHelper.PrintError("Current LoggedIn User role must be Server, Chef, or Manager.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ThemeHelper.PrintError(ex.Message);
                }
    }

    public static void HandleServerSession(Menu menu, Restaurant restaurant, DisplayService display, RestaurantService restaurantService)
    {
        bool activeServerSession = true;
        while (activeServerSession)
        {
            try
            {
                ConsoleHelper.ViewServerMenu();
                
                string? choice = Console.ReadLine()?.Trim();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        display.ViewAvailableMenuItems(menu);
                        Console.WriteLine();
                        break;

                    case "2":
                        Console.Clear();
                        restaurantService.ViewMenuByCategory();
                        break;

                    case "3":
                        Console.Clear();
                        restaurantService.HandleOrderPlacement();
                        break;
                    
                    case "4":
                        Console.Clear();
                        display.ViewActiveOrdersForLoggedInServer(restaurant, (Server)CurrentUser!);
                        break;
                    
                    case "5":
                        Console.Clear();
                        display.ViewReadyOrders(restaurant);
                        break;
                        
                    case "6":
                        Console.Clear();
                        restaurantService.HandleServing();
                        break;
                        
                    case "7":
                        Console.Clear();
                        restaurantService.HandleCancellation();
                        break;

                    case "0":
                        Console.Clear();
                        LogOut();
                        HandleAuthorization(menu, restaurant, display, restaurantService);
                        activeServerSession = false;
                        break;

                    default:
                        Console.Clear();
                        ThemeHelper.PrintError("Invalid Choice!");
                        break;
                }
            }
            catch (Exception ex)
            {
                ThemeHelper.PrintError(ex.Message);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
    
        public static void HandleChefSession(Menu menu, Restaurant restaurant, DisplayService display, RestaurantService restaurantService)
    {
        bool activeChefSession = true;
        while (activeChefSession)
        {
            try
            {
                ConsoleHelper.ViewChefMenu();
                
                string? choice = Console.ReadLine()?.Trim();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        display.ViewKitchenQueue(restaurant);
                        Console.WriteLine();
                        break;

                    case "2":
                        Console.Clear();
                        display.ViewOrdersInProgress(restaurant);
                        break;

                    case "3":
                        Console.Clear();
                        restaurantService.HandlePreparing();
                        break;
                    
                    case "4":
                        Console.Clear();
                        restaurantService.HandleMakingReady();
                        break;
                    
                    case "5":
                        Console.Clear();
                        display.ViewMenuStock(menu);
                        break;
                    
                    case "0":
                        Console.Clear();
                        LogOut();
                        HandleAuthorization(menu, restaurant, display, restaurantService);
                        activeChefSession = false;
                        break;

                    default:
                        Console.Clear();
                        ThemeHelper.PrintError("Invalid Choice!");
                        break;
                }
            }
            catch (Exception ex)
            {
                ThemeHelper.PrintError(ex.Message);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
    
    public static void HandleManagerSession(Menu menu, Restaurant restaurant, DisplayService display, RestaurantService restaurantService)
    {
        bool activeManagerSession = true;
        while (activeManagerSession)
        {
            try
            {
                ConsoleHelper.ViewManagerMenu();
                
                string? choice = Console.ReadLine()?.Trim();
                Console.WriteLine();

                switch (choice)
     
                {
                    case "1":
                        Console.Clear();
                        display.ViewRestaurantInfo(restaurant);
                        Console.WriteLine();
                        break;

                    case "2":
                        Console.Clear();
                        display.ViewAllStaff(restaurant);
                        break;

                    case "3":
                        Console.Clear();
                        display.ViewMenuStock(menu);
                        break;
                    
                    case "4":
                        Console.Clear();
                        display.ViewAllOrders(restaurant);
                        break;
                    
                    case "5":
                        Console.Clear();
                        restaurantService.HandleRestock();
                        break;
                        
                    case "6":
                        Console.Clear();
                        restaurantService.HandleCancellation();
                        break;
                        
                    case "7":
                        Console.Clear();
                        display.ViewDailySalesSummary(restaurant);
                        break;
                    
                    case "8":
                        Console.Clear();
                        restaurantService.HandleOrderPlacement();
                        break;

                    case "0":
                        Console.Clear();
                        LogOut();
                        HandleAuthorization(menu, restaurant, display, restaurantService);
                        activeManagerSession = false;
                        break;

                    default:
                        Console.Clear();
                        ThemeHelper.PrintError("Invalid Choice!");
                        break;
                }
            }
            catch (Exception ex)
            {
                ThemeHelper.PrintError(ex.Message);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}