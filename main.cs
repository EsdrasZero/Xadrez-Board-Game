using System;
using board;
using chess;

class Program
{
    public static void Main(string[] args)
    {
        try
        {
            ChessGame game = new ChessGame();

            while (!game.finished)
            {
                try
                {
                    Console.Clear();
                    Screen.printGame(game);

                    Console.WriteLine();
                    Console.WriteLine("Origin: ");
                    Position start = Screen.readChessPosition().toPosition();
                    game.validateStartPosition(start);


                    bool[,] possiblesPosition = game.board.piece(start).possibleMoves();

                    Console.Clear();
                    Screen.printBoard(game.board, possiblesPosition);

                    Console.WriteLine();
                    Console.WriteLine("Destiny: ");
                    Position end = Screen.readChessPosition().toPosition();
                    Console.WriteLine();
                    game.validateEndPosition(start, end);

                    game.makesMove(start, end);
                }
                catch (BoardException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
        }
        catch (BoardException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}