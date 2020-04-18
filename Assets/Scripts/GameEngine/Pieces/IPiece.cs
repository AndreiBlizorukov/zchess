using System.Collections.Generic;
using UnityEngine;

namespace GameEngine.Pieces
{
    public interface IPiece
    {
        Color GetColor();
        List<Vector2Int> GetPotentialMoves(Vector2Int position);
        void BeingMoved();
    }
}