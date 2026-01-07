using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TheFinals
{
    internal class Player
    {
        public int energy = 5;
        public int money = 50;
        public int day = 1;
        public int reputation = 0;
        public int sanity = 3;
        public string[] placeNames = { "Apartment", "Lab", "Library", "Parc", "Concert", "THE Cave", "Workplace", "School", "Marketplace", "Home", "Bus Stop" };
        public int placeNameIndex = 0;
        static Random rng = new Random(); // According to chatgpt if the same random is called it might use the same seed, giving the same result even though it's supposed to be random.
        static int start = 0;
        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Welcome to my game: No More Deliveries Please!");
            Console.WriteLine("Reach reputation 4 by completing deliveries without your energy hitting 0, money going into the negatives and reach it before day 26.");
            Console.WriteLine("Watch out for sanity");
            Console.WriteLine("Shall we start?");
            Console.ResetColor();
            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine("-> 1 = Yes");
            Console.ResetColor();
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine("-> 2 = No (Exits the system)");
            Console.ResetColor();

            start = Convert.ToInt32(Console.ReadLine());
        }
        public void Update()
        {
            int decision = 0;
            while (true)
            {
                switch (start)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("And so it begins...");
                        while (day <= 25 && energy > 0 && money >= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("What will you do?");
                            Console.ResetColor();
                            DrawHUD();

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("[1] View Orders -> Shows the list of what orders are there.");
                            Console.WriteLine("[2] Deliver -> Can be done if the requirements are met. -1 Energy, +1 Day, -1 Sanity (We hate jobs).");
                            Console.WriteLine("[3] Rest -> Energy +2, Day + 1, Sanity +1.");
                            Console.WriteLine("[4] Help -> print rules; nothing else happens.");
                            Console.WriteLine("[5] Hangman -> To increase sanity by winning the mini game.");
                            Console.WriteLine("[6] Change Location -> Spend 10 dollars to go to a random location. Uses 1 energy.   ");
                            Console.WriteLine("[7] Quit -> exits the game.");
                            Console.ResetColor();

                            decision = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();

                            switch (decision)
                            {
                                case 1:
                                    AvailableOrders();
                                    break;
                                case 2:
                                    
                                    Console.WriteLine("Current location: " + placeNames[placeNameIndex]);
                                    Console.WriteLine("Which order do you want to deliver?");
                                    for (int i = 0; i < orders.Length; i++)
                                    {

                                        if (orders[i].completed)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine($"[X] {orders[i].from} -> {orders[i].to} || COMPLETED");
                                            Console.ResetColor();
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine($"[{i + 1}] {orders[i].from} -> {orders[i].to} || " + $"{orders[i].pay} Dollars || Expires: Day {orders[i].deadline}");
                                            Console.ResetColor();
                                        }
                                    }
                                    string input = Console.ReadLine();
                                    int choice; // Player sees the list start from 1 but arrays start from 0 so if I don't do this it gives exceptions.
                                    
                                    if (!int.TryParse(input, out choice))
                                    {
                                        Console.WriteLine("Please enter a number.");
                                        break;
                                    }
                                    choice -= 1;

                                    if (choice < 0 || choice >= orders.Length)
                                    {
                                        Console.Write("Invaild order."); break;
                                    }
                                    if (orders[choice].completed)
                                    {
                                        Console.WriteLine("Order already delivered."); break;
                                    }
                                    DeliverySystem.TryDeliver(this, orders[choice]);
                                    break;
                                case 3:
                                    energy += 2;
                                    day += 1;
                                    sanity += 1;
                                    break;
                                case 4:
                                    Console.WriteLine("Check orders to see whats quests are available, the dates of said quests and the pay for completing them.");
                                    Console.WriteLine("Win by reaching reputation 4 without the day count going over 25, money going into the negatives or energy hitting 0.");
                                    Console.WriteLine("Energy can't go over 5.");
                                    Console.WriteLine("If sanity goes below 0, it will be reset to 0 and you will advance 5 days");
                                    break;
                                case 5:
                                    StickmanGame.StickMan(this);
                                    break;
                                case 6: //I soft locked the player and honestly if I don't add this there is only going to be a single way to win.

                                    placeNameIndex = rng.Next(placeNames.Length);
                                    money -= 10;
                                    energy -= 1;
                                    break;
                                case 7:
                                    Console.Clear();
                                    Console.Write("Good try and hope to see you again.");
                                    Environment.Exit(0);
                                    break;
                                default:
                                    Console.WriteLine("No such option, try again.");

                                    break;

                            }
                            if (reputation == 4)
                            {
                                WinLoseUI checker = new WinLoseUI(this);
                                checker.Check();
                                return;
                            }
                            ClampStats();
                        }
                        WinLoseUI check = new WinLoseUI(this);
                        check.Check();
                        return;
                    case 2:
                        Console.WriteLine("Alright so that's that I guess :C");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Please pick one of the presented options");
                        Console.WriteLine("-> 1 = Yes");
                        Console.WriteLine("-> 2 = No (Exits the system)");
                        start = int.Parse(Console.ReadLine());
                        break;
                }
            }
        }

        private void DrawHUD()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Money: " + money + " || ");
            Console.Write("Energy: " + energy + "/5 " + " || ");
            Console.Write("Day: " + day + " || ");
            Console.Write("Reputation: " + reputation + " || ");
            Console.WriteLine("Sanity: " + sanity + "/3" + " || ");
            Console.WriteLine("Current location: " + placeNames[placeNameIndex]);
            Console.ResetColor();
        }

        public void ClampStats()
        {
            if (sanity < 0)
            {
                day += 5;
                sanity = 0;
                Console.WriteLine("You've gone insane! Day count has advanced by 5.");
            }
            if (sanity > 3)
            {
                sanity = 3;
            }
            if (energy > 5)
            {
                energy = 5;
            }
        }
        void AvailableOrders()
        {

            Console.WriteLine("Left -> Where to pickup || Right -> Where to deliver \n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            for (int i = 0; i < orders.Length; i++)
            {
                if (orders[i].completed)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[X] {orders[i].from} -> {orders[i].to} || COMPLETED");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[{i + 1}] {orders[i].from} -> {orders[i].to} || " + $"{orders[i].pay} Dollars || Expires: Day {orders[i].deadline}");
                    Console.ResetColor();
                }
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.WriteLine("\n");
        }

        Order[] orders = new Order[] //Learned from AI. Helps us set up orders without having to hard code them. I literally could not have figured this out on my own lol.
        {
         new Order { from = "Marketplace", to = "Library", pay = 15, deadline = 15, completed = false },
         new Order { from = "Workplace", to = "THE Cave", pay = 25, deadline = 6, completed = false },
         new Order { from = "Apartment", to = "Bus Stop", pay = 15, deadline = 13, completed = false },
         new Order { from = "Library", to = "Marketplace", pay = 10, deadline = 5, completed = false },
         new Order { from = "Parc", to = "Apartment", pay = 30, deadline = 12, completed = false },
         new Order { from = "Concert", to = "Lab", pay = 10, deadline = 20, completed = false },
         new Order { from = "Bus Stop", to = "School", pay = 5, deadline = 24, completed = false },
         new Order { from = "School", to = "Home", pay = 0, deadline = 9, completed = false },
         new Order { from = "Home", to = "Apartment", pay = 0, deadline = 2, completed = false },
         new Order { from = "Lab", to = "Home", pay = 10, deadline = 11, completed = false }
        };
      
    }
    
}
