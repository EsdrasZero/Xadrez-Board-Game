using System.Collections.Generic;
using board;

namespace chess
{
    class ChessGame
    {
        public Board board { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured; //conjunto -> colecao de dados que obedecem as regras dos conjuntos
        public bool check { get; private set; }
        public Piece vulnerablePieceEnPassant { get; private set; }

        public ChessGame()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            check = false;
            vulnerablePieceEnPassant = null;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            putPieces();
        }
        public Piece implementMovement(Position start, Position end)
        {
            Piece p = board.removePiece(start);
            p.increaseMovement();
            Piece capturedPiece = board.removePiece(end);
            board.putPiece(p, end);
            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }
            //#special move -> little Castling
            if (p is King && end.column == start.column + 2)
            {
                Position startR = new Position(start.line, start.column + 3);
                Position endR = new Position(start.line, start.column + 1);
                Piece R = board.removePiece(startR);
                R.increaseMovement();
                board.putPiece(R, endR);
            }
            //#special move -> big Castling
            if (p is King && end.column == start.column - 2)
            {
                Position startR = new Position(start.line, start.column - 4);
                Position endR = new Position(start.line, start.column - 1);
                Piece R = board.removePiece(startR);
                R.increaseMovement();
                board.putPiece(R, endR);
            }

            //#special move -> en passant
            if (p is Pawn)
            {
                if (start.column != end.column && capturedPiece == null)
                {
                    Position posP;
                    if (p.color == Color.White)
                    {
                        posP = new Position(end.line + 1, end.column);
                    }
                    else
                    {
                        posP = new Position(end.line - 1, end.column);
                    }
                    capturedPiece = board.removePiece(posP);
                    captured.Add(capturedPiece);
                }
            }


            return capturedPiece;
        }

        public void undoMovement(Position start, Position end, Piece capturedPiece)
        {
            Piece p = board.removePiece(end);
            p.decreaseMovement();
            if (capturedPiece != null)
            {
                board.putPiece(capturedPiece, end);
                captured.Remove(capturedPiece);
            }
            board.putPiece(p, start);

            //#special move -> little Castling
            if (p is King && end.column == start.column + 2)
            {
                Position startR = new Position(start.line, start.column + 3);
                Position endR = new Position(start.line, start.column + 1);
                Piece R = board.removePiece(endR);
                R.decreaseMovement();
                board.putPiece(R, startR);
            }
            //#special move -> big Castling
            if (p is King && end.column == start.column + 2)
            {
                Position startR = new Position(start.line, start.column - 4);
                Position endR = new Position(start.line, start.column - 1);
                Piece R = board.removePiece(endR);
                R.decreaseMovement();
                board.putPiece(R, startR);
            }

            //#special move -> en passant
            if (p is Pawn)
            {
                if (start.column != end.column && capturedPiece == vulnerablePieceEnPassant)
                {
                    Piece pawn = board.removePiece(end);
                    Position posP;
                    if (p.color == Color.White)
                    {
                        posP = new Position(3, end.column);
                    }
                    else
                    {
                        posP = new Position(4, end.column);
                    }
                    board.putPiece(pawn, posP);
                }
            }
        }

        public void makesMove(Position start, Position end)
        {
            Piece capturedPiece = implementMovement(start, end);
            if (isInCheck(currentPlayer))
            {
                undoMovement(start, end, capturedPiece);
                throw new BoardException("You can't put yourself in check");
            }
            if (isInCheck(opponent(currentPlayer)))
            {
                check = true;
            }
            else
            {
                check = false;
            }

            Piece p = board.piece(end);
            //#special move - promotion
            if (p is Pawn)
            {
                if ((p.color == Color.White && end.line == 0) || (p.color == Color.Black && end.line == 7))
                {
                    p = board.removePiece(end);
                    pieces.Remove(p);
                    Piece queen = new Queen(board, p.color);
                    board.putPiece(queen, end);
                    pieces.Add(queen);
                }
            }

            if (testCheckmate(opponent(currentPlayer)))
            {
                finished = true;
            }
            else
            {
                turn++;
                changePlayer();
            }


            //#special move -> en passant
            if (p is Pawn && (end.line == start.line + 2 || end.line == start.line - 2))
            {
                vulnerablePieceEnPassant = p;
            }
            else
            {
                vulnerablePieceEnPassant = null;
            }
        }

        public void validateStartPosition(Position pos)
        {
            if (board.piece(pos) == null)
            {
                throw new BoardException("There is no piece in the chosen origin position");
            }
            if (currentPlayer != board.piece(pos).color)
            {
                throw new BoardException("The piece chosen is not yours");
            }
            if (!board.piece(pos).therePossiblesMoves())
            {
                throw new BoardException("The destination piece has no moviments");
            }
        }

        public void validateEndPosition(Position start, Position end)
        {
            if (!board.piece(start).possibleMovement(end))
            {
                throw new BoardException("Invalid target position");
            }
        }

        private void changePlayer()
        {
            if (currentPlayer == Color.White)
            {
                currentPlayer = Color.Black;
            }
            else
            {
                currentPlayer = Color.White;
            }
        }

        public HashSet<Piece> capturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in captured)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> piecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in pieces)
            {
                if (x.color == color)
                    aux.Add(x);
            }
            aux.ExceptWith(capturedPieces(color));
            return aux;
        }

        private Color opponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece king(Color color)
        {
            foreach (Piece x in piecesInGame(color))
            {
                if (x is King)
                { // is -> se a peca "x" é uma instancia da classe rei, ou seja, se x é um rei
                    return x;
                }
            }
            return null;
        }

        public bool isInCheck(Color color)
        {
            Piece K = king(color);
            if (K == null)
            {
                throw new BoardException("There's no king of color " + color + " on the board");
            }
            foreach (Piece x in piecesInGame(opponent(color)))
            {
                bool[,] mat = x.possibleMoves();
                if (mat[K.position.line, K.position.column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool testCheckmate(Color color)
        {
            if (!isInCheck(color))
            {
                return false;
            }
            foreach (Piece x in piecesInGame(color))
            {
                bool[,] mat = x.possibleMoves();
                for (int i = 0; i < board.lines; i++)
                {
                    for (int j = 0; j < board.columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position end = new Position(i, j);
                            Piece capturedPiece = implementMovement(x.position, end);
                            bool testCheck = isInCheck(color);
                            undoMovement(x.position, end, capturedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }


        public void putNewPiece(char column, int line, Piece piece)
        {
            board.putPiece(piece, new ChessPosition(column, line).toPosition());
            pieces.Add(piece);
        }

        private void putPieces()
        {

            putNewPiece('a', 1, new Rook(board, Color.White));
            putNewPiece('b', 1, new Horse(board, Color.White));
            putNewPiece('c', 1, new Bishop(board, Color.White));
            putNewPiece('d', 1, new Queen(board, Color.White));
            putNewPiece('e', 1, new King(board, Color.White, this));
            putNewPiece('f', 1, new Bishop(board, Color.White));
            putNewPiece('g', 1, new Horse(board, Color.White));
            putNewPiece('h', 1, new Rook(board, Color.White));
            putNewPiece('a', 2, new Pawn(board, Color.White, this));
            putNewPiece('b', 2, new Pawn(board, Color.White, this));
            putNewPiece('c', 2, new Pawn(board, Color.White, this));
            putNewPiece('d', 2, new Pawn(board, Color.White, this));
            putNewPiece('e', 2, new Pawn(board, Color.White, this));
            putNewPiece('f', 2, new Pawn(board, Color.White, this));
            putNewPiece('g', 2, new Pawn(board, Color.White, this));
            putNewPiece('h', 2, new Pawn(board, Color.White, this));

            putNewPiece('a', 8, new Rook(board, Color.Black));
            putNewPiece('b', 8, new Horse(board, Color.Black));
            putNewPiece('c', 8, new Bishop(board, Color.Black));
            putNewPiece('d', 8, new Queen(board, Color.Black));
            putNewPiece('e', 8, new King(board, Color.Black, this));
            putNewPiece('f', 8, new Bishop(board, Color.Black));
            putNewPiece('g', 8, new Horse(board, Color.Black));
            putNewPiece('h', 8, new Rook(board, Color.Black));
            putNewPiece('a', 7, new Pawn(board, Color.Black, this));
            putNewPiece('b', 7, new Pawn(board, Color.Black, this));
            putNewPiece('c', 7, new Pawn(board, Color.Black, this));
            putNewPiece('d', 7, new Pawn(board, Color.Black, this));
            putNewPiece('e', 7, new Pawn(board, Color.Black, this));
            putNewPiece('f', 7, new Pawn(board, Color.Black, this));
            putNewPiece('g', 7, new Pawn(board, Color.Black, this));
            putNewPiece('h', 7, new Pawn(board, Color.Black, this));
        }
    }
}