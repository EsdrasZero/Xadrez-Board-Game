using board;

namespace chess {
  class Pawn : Piece {
    private ChessGame game;
    public Pawn(Board chessBoard, Color color, ChessGame game) : base(chessBoard, color) {
      this.game = game;
    }

    public override string ToString() {
      return "P";
    }

    private bool thereAnEnemy(Position pos) {
      Piece p = chessBoard.piece(pos);
      return p != null && p.color != color;
    }

    private bool withoutPiece(Position pos) {
      return chessBoard.piece(pos) == null;
    }

    public override bool[,] possibleMoves() {
      bool[,] mat = new bool[chessBoard.lines, chessBoard.columns];

      Position pos = new Position(0, 0);

      if(color == Color.White) {
          pos.setValues(position.line - 1, position.column);
          if(chessBoard.validPosition(pos) && withoutPiece(pos)) {
            mat[pos.line, pos.column] = true;
          }
        
        pos.setValues(position.line - 2, position.column);
        Position pos2 = new Position(position.line - 1, position.column);
        if(chessBoard.validPosition(pos2) && withoutPiece(pos2) && chessBoard.validPosition(pos) && withoutPiece(pos) && amountOfMovement == 0) {
          mat[pos.line, pos.column] = true;
        }
    
        pos.setValues(position.line - 1, position.column - 1);
        if(chessBoard.validPosition(pos) && thereAnEnemy(pos)) {
          mat[pos.line, pos.column] = true;
        }
    
        pos.setValues(position.line - 1, position.column + 1);
        if(chessBoard.validPosition(pos) && thereAnEnemy(pos)) {
          mat[pos.line, pos.column] = true;
        }

        //#special move -> en passant
        if(position.line == 3) {
          Position left = new Position(position.line, position.column - 1);
          if(chessBoard.validPosition(left) && thereAnEnemy(left) && chessBoard.piece(left) ==  game.vulnerablePieceEnPassant) {
            mat[left.line - 1, left.column] = true;
          }
          Position right = new Position(position.line, position.column + 1);
          if(chessBoard.validPosition(right) && thereAnEnemy(right) && chessBoard.piece(right) ==  game.vulnerablePieceEnPassant) {
            mat[right.line - 1, right.column] = true;
          }
        }
        
        return mat;
      }
      else {
          pos.setValues(position.line + 1, position.column);
          if(chessBoard.validPosition(pos) && withoutPiece(pos)) {
            mat[pos.line, pos.column] = true;
          }
        
        pos.setValues(position.line + 2, position.column);
        Position pos2 = new Position(position.line + 1, position.column);
        if(chessBoard.validPosition(pos2) && withoutPiece(pos2) && chessBoard.validPosition(pos) && withoutPiece(pos) && amountOfMovement == 0) {
          mat[pos.line, pos.column] = true;
        }
    
        pos.setValues(position.line + 1, position.column - 1);
        if(chessBoard.validPosition(pos) && thereAnEnemy(pos)) {
          mat[pos.line, pos.column] = true;
        }
    
        pos.setValues(position.line + 1, position.column + 1);
        if(chessBoard.validPosition(pos) && thereAnEnemy(pos)) {
          mat[pos.line, pos.column] = true; 
        }
        
        //#special move -> en passant
        if(position.line == 4) {
          Position left = new Position(position.line, position.column - 1);
          if(chessBoard.validPosition(left) && thereAnEnemy(left) && chessBoard.piece(left) ==  game.vulnerablePieceEnPassant) {
            mat[left.line + 1, left.column] = true;
          }
          Position right = new Position(position.line, position.column + 1);
          if(chessBoard.validPosition(right) && thereAnEnemy(right) && chessBoard.piece(right) ==  game.vulnerablePieceEnPassant) {
            mat[right.line + 1, right.column] = true;
          }
        }
        
        return mat;
      }
    }
  }
}