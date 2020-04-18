using System.Collections.Generic;
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

        public CellState GetCellState(Vector2Int target, IPiece piece)
        {
            if (target.x < 0 || target.y > 7)
            {
                return CellState.OutOfBounds;
            }

            if (target.x > 7 || target.y < 0)
            {
                return CellState.OutOfBounds;
            }

            var targetPiece = mPieces[target.x, target.y];
            if (targetPiece != null)
            {
                return piece.GetColor() == targetPiece.GetColor()
                    ? CellState.Friendly
                    : CellState.Enemy;
            }

            return CellState.Free;
        }

        public List<Vector2Int> GetValidatedMoves(List<Vector2Int> targets, IPiece piece)
        {
            var path = new List<Vector2Int>();
            foreach (var target in targets)
            {
                var state = GetCellState(target, piece);
                if (state == CellState.Enemy)
                {
                    path.Add(target);
                    break;
                }

                if (state != CellState.Free)
                {
                    break;
                }    
                
                path.Add(target);
            }

            return path;
        }
    }

    public enum CellState
    {
        None,
        Friendly,
        Enemy,
        Free,
        OutOfBounds
    }
}