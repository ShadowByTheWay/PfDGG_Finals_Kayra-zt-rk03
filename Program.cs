using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finals
{
    internal class Program
    {
        public static int energy = 5;
        public static int money = 100;
        public static int day = 1;
        public static int stop = 0;
        public static int start = 0;
        static void Main(string[] args)
        {
            Start();
            Update();
        }

        static void Start()
        {
            Console.WriteLine("Welcome to  (C) Sharp Adventure!");
            Console.WriteLine("The goal: \n -> Make it to stop 9 before; \n -> Your energy hits 0, \n -> You reach day 26, \n -> You hit negative gold.");
            Console.WriteLine("Shall we start?");
            Console.WriteLine("-> 1 = Yes");
            Console.WriteLine("-> 2 = No (Exits the system)");

            start = Convert.ToInt32(Console.ReadLine());

        }

        static void Update()
        {
            int decision = 0;
            while (true)
            {
                switch (start)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("And so it begins...");
                        while (day < 26 && energy > 0 && money >= 0)
                        {
                            Console.WriteLine("What will you do?");
                            Console.Write("Money: " + money + " || ");
                            Console.Write("Energy: " + energy + "/5 " + " || ");
                            Console.Write("Day: " + day + " || ");
                            Console.WriteLine("Stop: " + stop);
                            Console.WriteLine("[1] Travel -> Moves you to the next stop: Money -25, Energy -2, Day +1, Stop +1.");
                            Console.WriteLine("[2] Work -> Gold +20 , Energy −1, Day +1.");
                            Console.WriteLine("[3] Rest -> Energy +2, Day + 1.");
                            Console.WriteLine("[4] Gamble -> Try your luck to either win 35 gold, gain 3 energy, move ahead a stop or gain nothing, lose 40 gold or lose 3 energy. Gambling always forwards the day by 1.");
                            Console.WriteLine("[5] Help -> print rules; nothing else happens.");
                            Console.WriteLine("[6] Quit -> exit.");
                            decision = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();

                            switch (decision)
                            {
                                case 1:
                                    stop = stop + 1;
                                    money -= 25;
                                    energy -= 1;
                                    day += 1;
                                    break;
                                case 2:
                                    money += 20;
                                    energy -= 1;
                                    Energy();
                                    day += 1;
                                    break;
                                case 3:
                                    energy += 2;
                                    Energy();
                                    day += 1;
                                    break;
                                case 4:
                                    RollAndDecide();
                                    break;
                                case 5:
                                    Console.WriteLine("Win by reaching the final stop before day count goes over 30, without energy hitting 0 or money going into the negative");
                                    break;
                                case 6:
                                    Console.Clear();
                                    Console.Write("Good try and hope to see you again.");
                                    Environment.Exit(0);
                                    break;
                                default:
                                    Console.WriteLine("No such option, try again.");

                                    break;
                            }
                            if (stop > 8)
                            {
                                GameWin();
                            }

                        }
                        GameLost();

                        break;
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
        static (int Money, int Energy, int Day, int Stop) RollAndDecide()
        {
            int result = 0;
            Random roll = new Random();
            result = roll.Next(0, 100);
            if (result < 5)
            {
                Console.WriteLine("You moved onto the next stop! Congratulations!");
                stop += 1;
                day += 1;
            }
            else if (result >= 5 && result < 30)
            {
                Console.WriteLine("You won 35 gold! Congratulations!");
                money += 35;
                day += 1;
            }
            else if (result >= 30 && result < 50)
            {
                Console.WriteLine("You gained 3 energy! Congratulations!");
                energy += 2;
                day += 1;
            }
            else if (result >= 50 && result < 70)
            {
                Console.WriteLine("You lost 3 energy! Sucks to suck!");
                energy -= 3;
                Energy();
                day += 1;
            }
            else if (result >= 70 && result < 90)
            {
                Console.WriteLine("You lost 40 gold! Sucks to suck!");
                money -= 40;
                day += 1;
            }
            else if (result >= 90 && result < 100)
            {
                Console.WriteLine("The day has been advanced by 3! Sucks to suck!");
                day += 3;
            }
            return (money, energy, day, stop);
        }
        static void GameWin()
        {
            Console.WriteLine("You've won, congratulations!");

        }
        static void GameLost()
        {
            if (day > 26)
            { Console.WriteLine("Took too long!"); }

            if (money < 0)
            { Console.WriteLine("You've run them pockets dry!"); }

            if (energy == 0)
            { Console.WriteLine("You've got no energy left!"); }

            Console.WriteLine("You got to stop " + stop + " Better luck next time :)");
        }

        static void Energy()
        {
            if (energy >= 5)
            {
                energy = 5;
            }
;
        }
    }
}
