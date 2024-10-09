using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models;
using TicTacToe.Minimax;
using TicTacToe.Builder;
using TicTacToe.Controllers;

namespace TicTacToe
{
    namespace ITDranik.CodingInterview.Solvers.Games.TicTacToe
    {
        class Program
        {
            static void Main(string[] args)
            {
                
                var game = new Game();
                var scoreFunction = new MinimaxScoreFunction();
                var minimaxPlayer = new MinimaxPlayerController(game, scoreFunction); 

                Console.WriteLine("Welcome to the Tic-Tac-Toe game!");

                
                PrintBoard(game);

                
                while (game.State == Models.GameState.FirstPlayerTurn || game.State == Models.GameState.SecondPlayerTurn)
                {
                    if (game.State == Models.GameState.FirstPlayerTurn)
                    {
                        
                        Console.WriteLine("Player X's move");
                        var cell = GetPlayerMove();
                        game.MakeMove(game.FirstPlayer, cell);
                    }
                    else if (game.State == Models.GameState.SecondPlayerTurn)
                    {
                        
                        Console.WriteLine("Player's move is O (AI)");
                        minimaxPlayer.MakeMove();
                    }

                    
                    PrintBoard(game);

                    
                    if (game.State == Models.GameState.FirstPlayerVictory)
                    {
                        Console.WriteLine("Player X has won!");
                        break;
                    }
                    else if (game.State == Models.GameState.SecondPlayerVictory)
                    {
                        Console.WriteLine("Player O has won!");
                        break;
                    }
                    else if (game.State == Models.GameState.Draw)
                    {
                        Console.WriteLine("It's a draw!");
                        break;
                    }
                }
            }

            
            static void PrintBoard(Game game)
            {
                for (int row = 0; row < Models.Board.Size; row++)
                {
                    for (int column = 0; column < Models.Board.Size; column++)
                    {
                        var mark = game.GetMark(row, column); 
                        var markSymbol = mark.HasValue
                            ? (mark.Value == Models.PlayerMark.X ? "X" : "O") 
                            : "."; 

                        Console.Write(markSymbol + " ");
                    }
                    Console.WriteLine();
                }
            }

             
            static Models.Cell GetPlayerMove()
            {
                int row, column;
                while (true)
                {
                    Console.Write("Enter the row number (0-2): ");
                    if (int.TryParse(Console.ReadLine(), out row) && row >= 0 && row < 3)
                    {
                        break;
                    }
                    Console.WriteLine("Incorrect input. Try again.");
                }

                while (true)
                {
                    Console.Write("Enter the column number (0-2):");
                    if (int.TryParse(Console.ReadLine(), out column) && column >= 0 && column < 3)
                    {
                        break;
                    }
                    Console.WriteLine("Incorrect input. Try again.");
                }

                return new Models.Cell(row, column);
            }
        }

    }
}
