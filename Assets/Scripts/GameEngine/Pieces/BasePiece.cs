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

        public abstract List<Vector2Int> GetPotentialMoves(Vector2Int position);

        public virtual void BeingMoved()
        {
        }
    }
}