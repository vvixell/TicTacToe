using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Game
    {
        static string[,] Board = { { "", "", "" }, { "", "", "" }, { "", "", "" } };
        static string[] Players = { "X", "O" };
        static bool AI = true;

        static void Main(string[] args)
        {
            Util.DisplayGrid();
            string winner = string.Empty;
            int CurrentPlayer = 0;
            while (winner == string.Empty)
            {
                if (CurrentPlayer == 1 && AI)
                {
                    AIPlayer.GetMoves(Board);
                    Vector2 MovePosition = AIPlayer.ChooseMove();
                    Board[MovePosition.y, MovePosition.x] = Players[CurrentPlayer];
                    Util.DisplayTurn(Players[CurrentPlayer], MovePosition);
                }
                else 
                    Turn(Players[CurrentPlayer]);
                
                winner = Util.CheckForWinner(Board);
                CurrentPlayer = (CurrentPlayer + 1) % 2;
            }
            if (winner != "D")
                Console.WriteLine($"{winner} Wins!");
            else
                Console.WriteLine("Game ended in a draw");
            Console.ReadKey();
        }

        static void Turn(string Player)
        {
            Vector2 Position = Util.GetUserGridInput();
            while(!Util.FreePosition(Board, Position))
                Position = Util.GetUserGridInput();

            Board[Position.y, Position.x] = Player;
            Util.DisplayTurn(Player, Position);
        }
    }
}
