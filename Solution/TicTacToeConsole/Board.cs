using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToeConsole
{
    class Board
    {
        List<int> _board;                           // Game Board Represented by Ints
        public double _score;                       // Score from MiniMax
        public int _lastPos { get; private set; }   // Last Position Idx
        // STATIC Possible Wins Index's
        static int[][] _winPattern = new int[8][]
            {
                new int[]{0, 1, 2},
                new int[]{3, 4, 5},
                new int[]{6, 7, 8},
                new int[]{0, 3, 6},
                new int[]{1, 4, 7},
                new int[]{2, 5, 8},
                new int[]{0, 4, 8},
                new int[]{2, 4, 6},
            };

        public Board()
        {
            // Empty Board
            _board = new int[9].ToList();
            _lastPos = -1;
        }

        // Play Board Position with index: board position; plrPole: for  the 
        // int representation of 'X' or 'O'
        public void Play(int index, int plrPole)
        {
            // Invalid Player Pole Value
            if (plrPole != 1 && plrPole != -1) throw new Exception("ERROR!: Invalid plrPole.");
            // Already a Position
            if (_board[index] != 0) throw new Exception("ERROR!: There is already a Position Placed.");
            // Place Position
            _board[index] = plrPole;
            _lastPos = index;           // Last Position
        }
        
        // Get all possible future Board States as Board Obj.
        // plrPole: for the int representation of 'X' or 'O'
        public List<Board> FutureBoards(int plrPole)
        {
            // Get Future Positons as Indexs
            List<int> pos = new List<int>();
            for (int i = 0; i < _board.Count; i++)
                if (_board[i] == 0) pos.Add(i);
            // Create a new Board for each Position Index
            var boards = new List<Board>(pos.Count);
            foreach (int p in pos)
            {
                var B = Clone();        // Clone Board
                B.Play(p, plrPole);     // Play Position 1 into the Future
                boards.Add(B);          // Append
            }
            return boards;
        }

        // Make a copy of the Board
        Board Clone()
        {
            var B = new Board();
            B._board = _board.ToList();
            return B;
        }

        public override string ToString()
        {
            return $"Brd l{_lastPos} s{Math.Round(_score, 1)}";
        }

        // Bool Check for a Win & returns the int symbol value of the 
        // wining player
        public bool CheckWin()
        {
            //sym = 0;
            // Loop each set of winning Pattern of Indexs
            foreach (int[] pat in _winPattern)
            {
                int first = _board[pat[0]];
                // If nothing in 1st position, no possible win: continue
                if (first == 0) continue;
                // If All values are equal
                if (_board[pat[1]] == first && _board[pat[2]] == first)
                {
                    // Winner
                    //sym = first;
                    return true;
                }
            }
            // No Winner
            return false;
        }
        
        // Checks if there is any Board Positions Left
        public bool isBoardPos()
        {
            int len = _board.Where(b => b == 0).ToArray().Length;
            return len != 0;
        }

        // Prints out the Game Board to Console
        public void PrintConsole()
        {
            // Convert Board of Ints to its Respective 'X' or 'O'
            // Or its board number if empty
            Func<int, int, string> ToSym = (b, i) =>
            {
                if (b > 0) return "X";
                else if (b < 0) return "O";
                else return (i + 1).ToString();
            };
            // Convert Board of Ints to its Respective Symbol
            string[] board = _board.Select(ToSym).ToArray();
            // Create Board as String
            string msg = "\n ";
            for (int i = 0; i < board.Length; i++)
            {
                msg += board[i];                                        // Include Board Value
                if (i % 3 != 2) msg += " | ";                           // Verticle Separator
                if (i % 3 == 2 && i != 8) msg += "\n ----------\n ";    // Horizontal Separator
            }
            // --- Print the Board to Console ---
            Console.WriteLine(msg);
        }
    }
}
