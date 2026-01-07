using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TheFinals
{
    internal class WinLoseUI
    {
        private Player player;
        public WinLoseUI(Player player)
        {
            this.player = player;
        }
        
        public void Check()
        {
            
            if (player.day > 25)
            { Console.WriteLine("Took too long!"); LoseMessage(); return; }

            if (player.money < 0)
            { Console.WriteLine("You've run them pockets dry!"); LoseMessage(); return; }

            if (player.energy == 0)
            { Console.WriteLine("You've got no energy left!"); LoseMessage(); return; }

            if (player.reputation == 4)
            {
                Console.WriteLine("You've won! Congratulations!");
            }
        }

        public void LoseMessage()
        {
            Console.WriteLine("Your reputation was " + player.reputation + ". Better luck next time :)");
        }
    }
}
