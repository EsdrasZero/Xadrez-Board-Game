using board;

namespace chess {
  class King : Piece {

    private ChessGame game;
    
    public King(Board chessBoard, Color color, ChessGame game) : base(chessBoard, color) {
      this.game = game;      
    }
    public override string ToString() {
      return "K";
    }

    private bool canMove(Position pos) {
      Piece p = chessBoard.piece(pos);
      return p == null || p.color != this.color;
    }

    private bool testRookForCastling(Position pos) {
      Piece p = chessBoard.piece(pos);
      return p != null && p is Rook && p.color == color && amountOfMovement == 0;
    }

     public override bool[,] possibleMoves() {
        bool[,] mat = new bool[chessBoard.lines, chessBoard.columns];
        Position pos = new Position(0,0);
       
       //above
       pos.setValues(position.line - 1, position.column);
       if(chessBoard.validPosition(pos) && canMove(pos)) {
         mat[pos.line, pos.column] = true;
       }
       //ne
       pos.setValues(position.line - 1, position.column + 1);
       if(chessBoard.validPosition(pos) && canMove(pos)) {
         mat[pos.line, pos.column] = true;
       }
       //right
       pos.setValues(position.line, position.column + 1);
       if(chessBoard.validPosition(pos) && canMove(pos)) {
         mat[pos.line, pos.column] = true;
       }
       //se
       pos.setValues(position.line + 1, position.column + 1);
       if(chessBoard.validPosition(pos) && canMove(pos)) {
         mat[pos.line, pos.column] = true;
       }
       //down
       pos.setValues(position.line + 1, position.column);
       if(chessBoard.validPosition(pos) && canMove(pos)) {
         mat[pos.line, pos.column] = true;
       }
       //so
       pos.setValues(position.line + 1, position.column - 1);
       if(chessBoard.validPosition(pos) && canMove(pos)) {
         mat[pos.line, pos.column] = true;
       }
       //left
       pos.setValues(position.line, position.column - 1);
       if(chessBoard.validPosition(pos) && canMove(pos)) {
         mat[pos.line, pos.column] = true;
       }
       //no
       pos.setValues(position.line - 1, position.column - 1);
       if(chessBoard.validPosition(pos) && canMove(pos)) {
         mat[pos.line, pos.column] = true;
       }

       // #special move -> castling
       if(amountOfMovement == 0 && !game.check) {
         //# special move -> little castling
         Position posR1 = new Position(position.line, position.column + 3);
         if(testRookForCastling(posR1)) {
           Position p1 = new Position(position.line, position.column + 1);
           Position p2 = new Position(position.line, position.column + 2);
           if(chessBoard.piece(p1) == null && chessBoard.piece(p2) == null) {
             mat[position.line, position.column + 2] = true;
           }
         }
         //# special move -> big castling
         Position posR2 = new Position(position.line, position.column - 4);
         if(testRookForCastling(posR2)) {
           Position p1 = new Position(position.line, position.column - 1);
           Position p2 = new Position(position.line, position.column - 2);
           Position p3 = new Position(position.line, position.column - 3);
           if(chessBoard.piece(p1) == null && chessBoard.piece(p2) == null && chessBoard.piece(p3) == null) {
             mat[position.line, position.column - 2] = true;
           }
         }
       }
       
       return mat;
     }
  }
}