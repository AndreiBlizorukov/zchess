using GameEngine.Pieces;
using UnityEngine;

namespace GameEngine
{
    public class Board
    {
        public IPiece[,] mPieces = new IPiece[8, 8];

        public void PlacePieces(IPiece[,] pieces)
        {
            for (var x = 0; x < 8; x++)
            {
                for (var y = 0; y < 8; y++)
                {
                    // white always starts in the bottom, (0,0) - left DOWN corner
                    var piece = pieces[y, x];
                    if (piece != null)
                    {
                        mPieces[x, 7 - y] = piece;
                    }
                }
            }
        }

        public void MovePiece(Vector2Int source, Vector2Int destination)
        {
            var piece = mPieces[source.x, source.y];
            mPieces[source.x, source.y] = null;
            
            mPieces[destination.x, destination.y] = piece;
            piece.BeingMoved();
        }
    }
}