using System.Collections.Generic;
using UnityEngine;

namespace GameEngine.Pieces
{
    public interface IPiece
    {
        Color GetColor();
        List<Vector2Int> GetAvailableMoves(Vector2Int position, Board board);
        void BeingMoved();
        IPiece Copy();
    }
}