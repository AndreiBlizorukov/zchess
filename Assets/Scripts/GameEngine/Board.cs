using GameEngine.Pieces;

namespace GameEngine
{
    public class Board
    {
        public IPiece[,] mPieces = new IPiece[8, 8];

        public void PlacePieces(IPiece[,] pieces)
        {
            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    var piece = pieces[y, x];
                    if (piece != null)
                    {
                        mPieces[x, y] = piece;
                    }
                }
            }
        }
    }
}