using System.Collections.Generic;
using UnityEngine;

namespace GameEngine.Pieces
{
    public class Bishop : BasePiece
    {
        public Bishop(Color color) : base(color)
        {
        }
        
        public override List<Vector2Int> GetPotentialMoves(Vector2Int position)
        {
            return new List<Vector2Int>();
        }
    }
}