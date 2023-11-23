using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    static class AIPlayer
    {
        static Possibility CurrentPossibility;

        public static void GetMoves(string[,] Board)
        {
            CurrentPossibility = null;
            CurrentPossibility = new Possibility(Board, "X", Util.GetEmptySpaces(Board));
            CurrentPossibility.UpdatePossibility();

            /*
            Console.Write(new string(' ', 100));
            Console.SetCursorPosition(0, Console.CursorTop);

            
            Util.DisplayPossibility(CurrentPossibility, 0);
            int l = 15;
            foreach (Possibility p in CurrentPossibility.Possibilities)
            {
                Util.DisplayPossibility(p, l);
                l += 13;
            }*/
        }

        public static Vector2 ChooseMove()
        {
            Possibility Best = CurrentPossibility.GetBestPossibility();

            return Best.MovePosition;
        }
    }

    class Possibility
    {
        public List<Possibility> Possibilities;

        public string[,] Board = { { "", "", "" }, { "", "", "" }, { "", "", "" } }; 

        public float Value = 0;

        bool PathEnd;

        public Vector2 MovePosition;

        public Possibility(string[,] Board, string LastTurn, List<Vector2> EmptySpaces, Vector2 MovePosition = null)
        {
            this.Board = (string[,])Board.Clone();
            this.MovePosition = MovePosition;

            if (EmptySpaces.Count == 0) 
                ClosePossibilty();
            else
                Possibilities = GetGamePosibilities(this.Board, LastTurn, new List<Vector2>(EmptySpaces));
        }

        public List<Possibility> GetGamePosibilities(string[,] Board, string LastTurn, List<Vector2> EmptySpaces)
        {
            List<Possibility> posibilities = new List<Possibility>();

            foreach (Vector2 vector in EmptySpaces.ToArray())
            {
                string[,] newBoard = (string[,])Board.Clone();
                string turn = LastTurn == "X" ? "O" : "X";

                newBoard[vector.y, vector.x] = turn;

                EmptySpaces.Remove(vector);

                Possibility posibility = new Possibility(newBoard, turn, EmptySpaces, vector);
                posibilities.Add(posibility);
            }

            return posibilities;
        }

        void ClosePossibilty()
        {
            PathEnd = true;
            string winner = Util.CheckForWinner(Board);
            if (winner == "O") Value = 1; //Win
            else if (winner == "D") Value = 0; //Draw
        }

        public Possibility GetBestPossibility()
        {
            Random rand = new Random();
            Possibility Best = Possibilities[rand.Next(0, Possibilities.Count)]; //Chooses Random Possibility So if they have all the same value it doesnt produce the same games
            foreach (Possibility p in Possibilities)
            {
                if (p.Value > Best.Value) Best = p; 
            }
            return Best;
        }
        
        public void UpdatePossibility()
        {
            if (Possibilities == null || Possibilities.Count == 0) return;
            foreach (Possibility p in Possibilities)
            {
                p.UpdatePossibility();
            }

            if (!PathEnd)
            {
                float Total = 0; //Changes this Possibilities value to an average of its childs
                foreach (Possibility p in Possibilities)
                {
                    Total += p.Value;
                }
                Value = Total / Possibilities.Count;
            }
        }
    }
}
