using board;

namespace chess {
  class Horse : Piece {
    public Horse(Board chessBoard, Color color) : base(chessBoard, color) {
      
    }

    public override string ToString() {
      return "H";
    }

    private bool canMove(Position pos) {
      Piece p = chessBoard.piece(pos);
      return p == null || p.color != color;
    }

    public override bool[,] possibleMoves() {
      bool[,] mat = new bool[chessBoard.lines, chessBoard.columns];
      
      Position pos = new Position(0, 0);

      pos.setValues(position.line - 1, position.column -2);
      if(chessBoard.validPosition(pos) && canMove(pos)) {
        mat[pos.line, pos.column] = true;
      }
      pos.setValues(position.line - 2, position.column -1);
      if(chessBoard.validPosition(pos) && canMove(pos)) {
        mat[pos.line, pos.column] = true;
      }
      
      pos.setValues(position.line - 2, position.column +1);
      if(chessBoard.validPosition(pos) && canMove(pos)) {
        mat[pos.line, pos.column] = true;
      }
      
      pos.setValues(position.line - 1, position.column +2);
      if(chessBoard.validPosition(pos) && canMove(pos)) {
        mat[pos.line, pos.column] = true;
      }
      
      pos.setValues(position.line + 1, position.column +2);
      if(chessBoard.validPosition(pos) && canMove(pos)) {
        mat[pos.line, pos.column] = true;
      }
      
      pos.setValues(position.line + 2, position.column + 1);
      if(chessBoard.validPosition(pos) && canMove(pos)) {
        mat[pos.line, pos.column] = true;
      }
      
      pos.setValues(position.line + 2, position.column -1);
      if(chessBoard.validPosition(pos) && canMove(pos)) {
        mat[pos.line, pos.column] = true;
      }
      
      pos.setValues(position.line + 1, position.column -2);
      if(chessBoard.validPosition(pos) && canMove(pos)) {
        mat[pos.line, pos.column] = true;
      }

      return mat;
    }
    
  }
}