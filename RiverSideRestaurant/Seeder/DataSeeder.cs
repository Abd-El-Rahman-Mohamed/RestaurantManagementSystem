using RiverSideRestaurant.Services;
using RiverSideRestaurant.Entities;
using RiverSideRestaurant.Enums;

namespace RiverSideRestaurant.Seeder;

internal class DataSeeder
{
    public static Restaurant Seed(Menu menu)
    {
        Manager manager = new Manager(
            fullName: "Shakir Shalhoub", phone: "01155559729",
            monthlySalary : 13000m, hireDate:new DateOnly(2026, 1, 22));
        
        Chef chef = new Chef(
            fullName: "Seif Ghannam", phone: "01055583475", hireDate:new DateOnly(2026, 1, 22));
        
        Server serverOne = new Server(
            fullName: "Hamed Mustafa", phone: "01551170305");
        
        Server serverTwo = new Server(
            fullName: "Ismael Asfour", phone: "01255547321");
        
        Restaurant restaurant = new(
            "RiverSide Restaurant", "12 Nile St, Zamalek", "01012345678",
            "Daily 11:00-23:00", manager);
        
        restaurant.AddStaff(manager);
        restaurant.AddStaff(chef);
        restaurant.AddStaff(serverOne);
        restaurant.AddStaff(serverTwo);

        menu = new Menu();
        
        MenuItem redSauceMacaroni = new MenuItem("Red Sauce Macaroni", ItemCategory.MainCourse, 40m, 12,
            "A beloved, comforting dish featuring tender elbow macaroni coated in a rich, tangy tomato-based sauce");

        MenuItem cheeseBurgerSandwich = new MenuItem("Cheese Burger", ItemCategory.MainCourse, 85m, 9,
            "a classic American sandwich with cheese slice");

        MenuItem potatoSandwich = new MenuItem("Potatoes Sandwich", ItemCategory.MainCourse, 35m, 23);

        MenuItem potatoesPacket = new MenuItem("Potatoes Packet", ItemCategory.Appetizer, 45m, 44);

        MenuItem margheritaPizza = new MenuItem("Margherita Pizza", ItemCategory.MainCourse, 120m, 0);
        
        MenuItem mozzarellaSticks = new MenuItem("Mozzarella Sticks", ItemCategory.Appetizer, 50m, 20);

        MenuItem blueCuracao = new MenuItem("Blue Curacao", ItemCategory.Drink, 50m, 10,
            "A vibrant, tropical orange-flavored liqueur.");

        MenuItem lemonMint = new MenuItem("Lemon Mint", ItemCategory.Drink, 50m, 21);

        MenuItem borio = new MenuItem("Borio", ItemCategory.Drink, 50m, 18);

        MenuItem moltenCake = new MenuItem("Molten Cake", ItemCategory.Dessert, 70m, 11);

        MenuItem tiramisuCake = new MenuItem("Tiramisu Cake", ItemCategory.Dessert, 120m, 27,
            "A famous, no-bake Italian dessert consisting of coffee-soaked ladyfingers");
        
        menu.AddMenuItem(redSauceMacaroni);
        menu.AddMenuItem(cheeseBurgerSandwich);
        menu.AddMenuItem(potatoSandwich);
        menu.AddMenuItem(potatoesPacket);
        menu.AddMenuItem(margheritaPizza);
        menu.AddMenuItem(mozzarellaSticks);
        menu.AddMenuItem(blueCuracao);
        menu.AddMenuItem(lemonMint);
        menu.AddMenuItem(borio);
        menu.AddMenuItem(moltenCake);
        menu.AddMenuItem(tiramisuCake);

        OrderLine orderLine = new OrderLine(redSauceMacaroni, 12);
        
        Order lunch = restaurant.PlaceOrder(11, serverOne, new List<OrderLine>([
            new OrderLine(redSauceMacaroni, 1),
            new OrderLine(potatoesPacket, 2),
            new OrderLine(lemonMint, 1)
        ]));
        
        Order dessertCraving = restaurant.PlaceOrder(12, serverTwo, new List<OrderLine>([
            new OrderLine(moltenCake, 1),
            new OrderLine(tiramisuCake, 1),
            new OrderLine(borio, 1)
        ]));
        
        Order thirstyOne = restaurant.PlaceOrder(13, serverOne, new List<OrderLine>([
            new OrderLine(lemonMint, 1)
        ]));

        AuthService.LogInAs(chef);
        dessertCraving.MarkAsPreparing();
        AuthService.LogOut();

        return restaurant;
    }
}