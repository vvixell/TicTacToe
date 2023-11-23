using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    static class Util
    {
        public static void DisplayPossibility(Possibility possibility, int Left)
        {
            int t = Console.CursorTop;
            int l = Left;

            string Row = "  ───┼───┼───";
            string Column = "     │   │   ";


            Console.SetCursorPosition(l, t);
            Console.Write(Column);
            Console.SetCursorPosition(l, t + 1);
            Console.Write(Row);
            Console.SetCursorPosition(l, t + 2);
            Console.Write(Column);
            Console.SetCursorPosition(l, t + 3);
            Console.Write(Row);
            Console.SetCursorPosition(l, t + 4);
            Console.Write(Column);

            string[,] board = possibility.Board;
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    Console.SetCursorPosition(l + x * 4 + 3, y * 2 + t);
                    Console.Write(board[y, x]);
                }
            }
            Console.SetCursorPosition(l, t);
        }

        public static void DisplayGrid()
        {
            string Row = "  ───┼───┼───";
            string Column = "     │   │   ";

            Console.WriteLine();
            Console.WriteLine(Column);
            Console.WriteLine(Row);
            Console.WriteLine(Column);
            Console.WriteLine(Row);
            Console.WriteLine(Column);
        } 

        public static void DisplayTurn(string Player, Vector2 Position)
        {
            int x = Position.x * 4 + 3;
            int y = Position.y * 2 + 1;
            int ReturnLine = Console.CursorTop;
            Console.SetCursorPosition(x, y);
            Console.Write(Player);
            Console.SetCursorPosition(0, ReturnLine);
        }

        public static Vector2 GetUserGridInput()
        {
            int Input = -1;

            while (Input == -1)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Q:
                        Input = 0; break;
                    case ConsoleKey.W:
                        Input = 1; break;
                    case ConsoleKey.E:
                        Input = 2; break;
                    case ConsoleKey.A:
                        Input = 3; break;
                    case ConsoleKey.S:
                        Input = 4; break;
                    case ConsoleKey.D:
                        Input = 5; break;
                    case ConsoleKey.Z:
                        Input = 6; break;
                    case ConsoleKey.X:
                        Input = 7; break;
                    case ConsoleKey.C:
                        Input = 8; break;
                }
            }

            return new Vector2(Input % 3, Input / 3);
        }

        public static bool FreePosition(string[,] Board, Vector2 Position)
        {
            if (Board[Position.y, Position.x] == string.Empty) 
                return true;
            else 
                return false;
        }

        public static string CheckForWinner(string[,] Board)
        {
            //Rows
            for (int y = 0; y < 3; y++)
            {
                if (Board[y, 0] == Board[y, 1] && Board[y, 1] == Board[y, 2] && Board[y, 0] != string.Empty) return Board[y, 0];
            }
            //Columns
            for (int x = 0; x < 3; x++)
            {
                if (Board[0, x] == Board[1, x] && Board[1, x] == Board[2, x] && Board[0, x] != string.Empty) return Board[0, x];
            }

            if (Board[0, 0] == Board[1, 1] && Board[1, 1] == Board[2, 2] && Board[1, 1] != string.Empty) return Board[1, 1];
            if (Board[2, 0] == Board[1, 1] && Board[1, 1] == Board[0, 2] && Board[1, 1] != string.Empty) return Board[1, 1];

            if (GetEmptySpaces(Board).Count == 0)
                return "D";

            return string.Empty;
        }

        public static List<Vector2> GetEmptySpaces(string[,] board)
        {
            List<Vector2> EmptySpaces = new List<Vector2>();
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                    if (board[y, x] == string.Empty) EmptySpaces.Add(new Vector2(x, y));
            }
            return EmptySpaces;
        }
    }
}
