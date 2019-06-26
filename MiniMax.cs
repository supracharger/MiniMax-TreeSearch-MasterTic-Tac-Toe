using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToeConsole
{
    static class MiniMax
    {
        // Transverse Tree to find optimal Solution
        // plrPole: from the reference of the computer
        // branch: Only used within Function. Tells what level in the Tree it is on.
        public static Board Eval(Board B, int plrPole, int branch = 0)
        {
            // Get Future Boards
            // plrPole: is Correct. Value must be flipped from ref of computer to 
            //          ref of player. And plrPole is already flipped since it is the prev value.
            var boards = B.FutureBoards(plrPole);
            // Check For Win
            bool win = B.CheckWin();
            // If Leaf Node: Check for Win or Game Over
            if (win || boards.Count == 0)
            {   // Get Score of Leaf Node
                B._score = Convert.ToInt32(win) * plrPole;
                return B;
            }
            // Transverse the Tree
            boards.ForEach(bd => Eval(bd, plrPole * -1, branch + 1));
            // -- MiniMax --
            // Min() is not used becuase performance was much better with out it. The reason this is true
            //      may be that TicTacToe has limited state space compared to other games that have a vast state space.
            // Maximize computers Score
            Board bOut = Max(boards);
            // Fitness Function to Propagate up the Tree & play Competitively
            // Favors scores with a lower branch, & pentalizes scores with a higher Branch
            // Max branch number is: 8
            B._score = boards.Select(b => b._score * (10 - branch) / 10).Average();
            // Return Child Node
            return bOut;
        }

        // Find Minimum Score over boards
        static Board Min(List<Board> boards)
        {
            Board bd = boards[0];
            // Find Minimum Score over boards   i=1
            for (int i = 1; i < boards.Count; i++)
                if (boards[i]._score < bd._score)
                    bd = boards[i];
            return bd;
        }

        // Find Maximum Score over boards
        static Board Max(List<Board> boards)
        {
            Board bd = boards[0];
            // Find Maximum Score over boards   i=1
            for (int i = 1; i < boards.Count; i++)
                if (boards[i]._score > bd._score)
                    bd = boards[i];
            return bd;
        }
    }
}
