
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinals
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            player.Start();
            player.Update();
        }
    }
}
