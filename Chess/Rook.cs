using board;
namespace chess {
  class Rook : Piece {
    public Rook(Board chessBoard, Color color) : base(chessBoard, color) {
    }
    public override string ToString() {
      return "R";
    }

        private bool canMove(Position pos) {
      Piece p = chessBoard.piece(pos);
      return p == null || p.color != this.color;
    }

     public override bool[,] possibleMoves() {
        bool[,] mat = new bool[chessBoard.lines, chessBoard.columns];
        Position pos = new Position(0,0);
       
       //above
       pos.setValues(position.line - 1, position.column);
       while(chessBoard.validPosition(pos) && canMove(pos)) {
         mat[pos.line, pos.column] = true;
         if(chessBoard.piece(pos) != null && chessBoard.piece(pos).color != color) {
           break;
         }
         pos.setValues(pos.line - 1, pos.column);
       }
       //right
       pos.setValues(position.line, position.column + 1);
       while(chessBoard.validPosition(pos) && canMove(pos)) {
         mat[pos.line, pos.column] = true;
         if(chessBoard.piece(pos) != null && chessBoard.piece(pos).color != color) {
           break;
         }
         pos.setValues(pos.line, pos.column + 1);
       }
       //down
       pos.setValues(position.line + 1, position.column);
       while(chessBoard.validPosition(pos) && canMove(pos)) {
         mat[pos.line, pos.column] = true;
         if(chessBoard.piece(pos) != null && chessBoard.piece(pos).color != color) {
           break;
         }
         pos.setValues(pos.line + 1, pos.column);
       }
       //left
       pos.setValues(position.line, position.column - 1);
       while(chessBoard.validPosition(pos) && canMove(pos)) {
         mat[pos.line, pos.column] = true;
         if(chessBoard.piece(pos) != null && chessBoard.piece(pos).color != color) {
           break;
         }
         pos.setValues(pos.line, pos.column - 1);
       }
       return mat;
     }
  }
}