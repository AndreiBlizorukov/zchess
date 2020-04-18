using System.Collections.Generic;
using UnityEngine;

namespace GameEngine.Pieces
{
    public abstract class BasePiece : IPiece
    {
        private readonly Color _color;

        public BasePiece(Color color)
        {
            _color = color;
        }

        public Color GetColor()
        {
            return _color;
        }

        public abstract List<Vector2Int> GetAvailableMoves(Vector2Int position, Board board);

        public virtual void BeingMoved()
        {
        }

        public abstract IPiece Copy();
    }
}