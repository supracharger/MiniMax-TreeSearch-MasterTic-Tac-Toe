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
            // If Leaf Node: Check for Win or Game Over
            if (B.CheckWin() || boards.Count == 0)
            {   // Get Score of Leaf Node
                B._score = new Dictionary<int, double>
                {
                    {1, GetScore(B, 1, branch) },
                    {-1, GetScore(B, -1, branch) }
                };
                return B;
            }
            // Transverse the Tree
            boards = boards.Select(bd => Eval(bd, plrPole * -1, branch + 1)).ToList();
            // -- MiniMax --
            // Keep on Defensive. Minimax is too complicated for Tic-Tac-Toe
            Board bOut;
            if (branch % 2 != 2)
                bOut = Max(boards, plr:1);      // Defensive
            else
                bOut = Max(boards);             // Offensive
            // Return Child Node
            return bOut;
        }

        // Get score for desired play 'O" or 'X'
        static double GetScore(Board B, int plr, int branch)
        {
            // Counts how many pos for desired player
            Func<int[], int, int> Count = (vals, p) 
                => vals.Select(v => (v == p) ? 1 : 0).Sum();
            Func<List<int>, int[], double> PosRatio = (b, a) =>
            {
                var val = new int[] { b[a[0]], b[a[1]], b[a[2]] };
                var cnt = Count(val, plr);
                var cntOpp = Count(val, plr * -1);
                if (cnt > cntOpp)
                    return cnt / (cntOpp + 1);
                else
                    return -cntOpp / (cnt + 1);
            };
            Func<int, int> IsPos = p => {
                if (p == 0) return 0;
                return (p == plr) ? 1 : -3; 
            };
            var scores = _possWin.Select(ar => PosRatio(B._board, ar)).ToList();
            //var magnitude = (plr == -1) ? scores.Max() : scores.Min();
            return (scores.Max() * 10 /*+ scores.Average()*/) / (branch + 1);
        }

        // Find Minimum Score over boards
        static Board Min(List<Board> boards)
        {
            Board bd = boards[0];
            // Find Minimum Score over boards   i=1
            for (int i = 1; i < boards.Count; i++)
                if (boards[i]._score[1] < bd._score[1])
                    bd = boards[i];
            return bd;
        }

        // Find Maximum Score over boards
        static Board Max(List<Board> boards, int plr = -1)
        {
            Board bd = boards[0];
            // Find Maximum Score over boards   i=1
            for (int i = 1; i < boards.Count; i++)
                if (boards[i]._score[plr] > bd._score[plr])
                    bd = boards[i];
            return bd;
        }

        static readonly List<int[]> _possWin = new List<int[]>
        {
            // Horizontal
            new int[]{0, 1, 2},
            new int[]{3, 4, 5},
            new int[]{6, 7, 8},
            // Vertical
            new int[]{0, 3, 6},
            new int[]{1, 4, 7},
            new int[]{2, 5, 8},
            // Diagonal
            new int[]{0, 4, 8},
            new int[]{2, 4, 6}
        };
    }
}
