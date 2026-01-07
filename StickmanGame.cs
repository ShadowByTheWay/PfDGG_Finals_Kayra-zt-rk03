using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinals
{
    internal class StickmanGame
    {
        private Player player;

        public StickmanGame(Player player)
        {
            this.player = player;
            
        }
        public static void StickMan(Player player)
        {
            int lives = 3;
            int wordPicked = 0;
            string[] wordList = { "GAME", "DEAD", "LOCK", "PLAY", "DELIVER", "PAY", "MONEY", "STICK", "MAN", "WOMAN" };
            Random wordRandomNumber = new Random();
            wordPicked = wordRandomNumber.Next(wordList.Length);
            string secretWord = wordList[wordPicked];
            char[] hiddenWord = new char[secretWord.Length];
            wordPicked = wordRandomNumber.Next(wordList.Length);
            Console.WriteLine("Hangman has begun! Good luck :)");

            for (int i = 0; i < hiddenWord.Length; i++)
            {
                hiddenWord[i] = '_';
            }
            while (lives != 0)
            {
                bool won = true;
                Console.WriteLine("Current lives: " + lives);
                for (int i = 0; i < hiddenWord.Length; i++)
                {
                    Console.Write(hiddenWord[i] + " ");
                }

                for (int i = 0; i < hiddenWord.Length; i++)
                {
                    if (hiddenWord[i] == '_')
                    {
                        won = false;
                        break;
                    }
                ;
                }
                if (won)
                {
                    Console.WriteLine("Congratulations! You've won! \n You've gained 3 sanity!");
                    player.sanity += 3;
                    break;
                }
                Console.WriteLine();
                Console.WriteLine("Enter a character for your guess.");
                char guessedLetter = Console.ReadLine().ToUpper()[0];



                bool found = false;
                for (int i = 0; i < secretWord.Length; i++)
                {
                    if (secretWord[i] == guessedLetter)
                    {
                        hiddenWord[i] = guessedLetter;
                        found = true;
                    }
                }

                if (found)
                {
                    Console.WriteLine("Correct guess!");

                }
                else
                {
                    Console.WriteLine("Wrong guess!");
                    lives -= 1;
                }

                if (lives == 0)
                {
                    Console.WriteLine("You've lost! \n You've lost 1 sanity!");
                    player.sanity -= 1;
                }
            }
        }
    }
}
