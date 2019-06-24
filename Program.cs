using System;

namespace TicTacToeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Print Title of game & Create Board
            Title();
            var B = new Board();

            // Continuous Loop
            while (true)
            {
                B.PrintConsole();                                               // Display Game Board
                // ............ Players Turn ................
                try
                {
                    Console.Write("Number to Play: ");
                    int place = int.Parse(Console.ReadLine());                  // Get User Play Input
                    B.Play(place - 1, 1);                                       // Play & Add User Input to Game Board
                }
                catch { Console.WriteLine("Invalid Input!");     continue; }    // Error Catch Invalid Input
                if (EndGame(B, "X")) break;                                     // Check for GameOver or a Tie

                // ............ Computers Turn ..............
                // Use MiniMax Tree Search to Evaluate the best play for the computer
                int comp = MiniMax.Eval(B, -1)._lastPos; // plrPole: ref from the computer point of view
                B.Play(comp, -1);                                               // Play & Computer Input to Game Board
                if (EndGame(B, "O")) break;                                     // Check for GameOver or a Tie
            }
            B.PrintConsole();
        }

        // Check for Game End with either a Win or a Tie
        static bool EndGame(Board B, string symbol)
        {
            // Check for a Win
            if (B.CheckWin())
                Console.WriteLine($"\n !!! WINNER '{symbol}' !!! \n --- GAME OVER  ---");
            // Check for a Tie
            else if (!B.isBoardPos())
                Console.WriteLine("\n --- TIE --- \n --- GAME OVER  ---");
            // Game Continues
            else
                return false;
            // GameOver Win or Tie
            return true;
        }

        // Print the Title of the Game
        static void Title()
        {
            string boarder = " +------------------+\n";
            string msg = " | AUTO TIC-TAC-TOE |\n";
            msg += " |                  |\n";
            msg += " | By Andrew Quaife |\n";
            Console.WriteLine("\n" + boarder + msg + boarder);
        }
    }
}
