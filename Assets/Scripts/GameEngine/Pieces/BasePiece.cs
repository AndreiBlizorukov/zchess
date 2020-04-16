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
    }
}