using System;
using System.Collections.Generic;
using board;
using chess;

class Screen {

  public static void printGame(ChessGame game) {
    printBoard(game.board);
    Console.WriteLine();
    printCapturedPieces(game);
    Console.WriteLine();
    Console.WriteLine("Current turn: " + game.turn);
    Console.WriteLine("Waiting player: " + game.currentPlayer); 
    if(game.check) {
      Console.WriteLine("CHECK!");
    }
  }

  public static void printCapturedPieces(ChessGame game) {
    Console.WriteLine("Captured pieces: ");
    Console.Write("White: ");
    printSet(game.capturedPieces(Color.White));
    Console.WriteLine();
    Console.Write("Black: ");
    ConsoleColor aux = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Yellow;
    printSet(game.capturedPieces(Color.Black));
    Console.ForegroundColor = aux;
    Console.WriteLine();
  }

  public static void printSet(HashSet<Piece> setOfPiece) {
    Console.Write("[");
    foreach(Piece x in setOfPiece) {
      Console.Write(x + " ");
    }
    Console.Write("]");
  }
  
  public static void printBoard(Board chessBoard) {
    for(int i = 0; i < chessBoard.lines; i++)
    {
      Console.Write(8-i + "  ");
      for(int j = 0; j < chessBoard.columns; j++)
      {
        printPiece(chessBoard.piece(i, j));
      }
      Console.WriteLine();
    }
      Console.WriteLine("   a b c d e f g h");
  }
  
  public static void printBoard(Board chessBoard, bool[,] possiblesPosition) {

    ConsoleColor bgOriginal = Console.BackgroundColor;
    ConsoleColor bgNew = ConsoleColor.DarkGray;
    
    for(int i = 0; i < chessBoard.lines; i++)
    {
      Console.Write(8-i + "  ");
      for(int j = 0; j < chessBoard.columns; j++)
      {
        if(possiblesPosition[i, j]) {
          Console.BackgroundColor = bgNew;
        }
        else {
          Console.BackgroundColor = bgOriginal;
        }
        printPiece(chessBoard.piece(i, j));
        Console.BackgroundColor = bgOriginal;
      }
      Console.WriteLine();
    }
      Console.WriteLine("   a b c d e f g h");
      Console.BackgroundColor = bgOriginal;
  }

  public static ChessPosition readChessPosition() {
    string s = Console.ReadLine();
    char line = s[0];
    int colunm = int.Parse(s[1] + "");
    return new ChessPosition(line, colunm);
  }

  public static void printPiece(Piece piece) {
    if(piece == null) {
      Console.Write("- ");
    } 
    else {
      
      if(piece.color == Color.White) {
        Console.Write(piece);
      }
      else {
        ConsoleColor aux = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(piece);
        Console.ForegroundColor = aux;
      }
      Console.Write(" ");
    }
  }
  
} 