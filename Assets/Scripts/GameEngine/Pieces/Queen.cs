using System.Collections.Generic;
using UnityEngine;

namespace GameEngine.Pieces
{
    public class Queen : BasePiece
    {
        public Queen(Color color) : base(color)
        {
        }

        public override List<Vector2Int> GetPotentialMoves(Vector2Int position)
        {
            return new List<Vector2Int>();
        }
    }
}