namespace board
{
    class Board
    {
        public int lines { get; set; }
        public int columns { get; set; }

        private Piece[,] pieces;

        public Piece piece(int line, int column)
        {
            return pieces[line, column];
        }


        public Piece piece(Position pos)
        {
            return pieces[pos.line, pos.column];
        }

        public bool thereAPiece(Position pos)
        {
            validatePosition(pos);
            return piece(pos) != null;
        }

        public void putPiece(Piece p, Position pos)
        {
            if (thereAPiece(pos))
            {
                throw new BoardException("There is already a piece in that positionAlre");
            }
            pieces[pos.line, pos.column] = p;
            p.position = pos;
        }

        public Piece removePiece(Position pos)
        {
            if (piece(pos) == null)
            {
                return null;
            }
            Piece aux = piece(pos);
            aux.position = null;
            pieces[pos.line, pos.column] = null;
            return aux;
        }

        public bool validPosition(Position pos)
        {
            if (pos.line < 0 || pos.line >= lines || pos.column < 0 || pos.column >= columns)
                return false;
            else
                return true;
        }

        public void validatePosition(Position pos)
        {
            if (!validPosition(pos))
            {
                throw new BoardException("Invalid position");
            }
        }

        public Board(int lines, int colunms)
        {
            this.lines = lines;
            this.columns = colunms;
            pieces = new Piece[lines, colunms];
        }

    }
}