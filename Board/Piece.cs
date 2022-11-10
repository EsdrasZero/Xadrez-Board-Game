namespace board {
  abstract class Piece {
    public Position position {get; set;}
    public Color color {get; protected set;}
    public int amountOfMovement {get; protected set;}
    public Board chessBoard {get; protected set;}

    public Piece(Board chessBoard, Color color)
    {
      this.position = null;
      this.chessBoard = chessBoard;
      this.color = color;
      this.amountOfMovement = 0;
    }

    public void increaseMovement() {
      amountOfMovement++;
    }
    public void decreaseMovement() {
      amountOfMovement--;
    }

    public bool therePossiblesMoves() {
      bool[,] mat = possibleMoves();
      for(int i = 0; i< chessBoard.lines; i++) {
        for(int j = 0; j<chessBoard.columns; j++) {
          if(mat[i, j]) {
            return true;
          }
        }
      }
      return false;
    }

    public bool possibleMovement(Position pos) {
      return possibleMoves()[pos.line, pos.column];
    }
    
    public abstract bool[,] possibleMoves(); //método que não tem implementação nessa classe e se uma classe tem um método abstrado, ela tem que passar a ser abstrata
  }
}