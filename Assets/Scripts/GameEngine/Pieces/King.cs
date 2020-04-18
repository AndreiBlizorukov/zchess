using System.Collections.Generic;
using UnityEngine;

namespace GameEngine.Pieces
{
    public class King : BasePiece
    {
        private readonly Vector3Int _movement = Vector3Int.one;
        
        public King(Color color) : base(color)
        {
        }
        
        public override List<Vector2Int> GetAvailableMoves(Vector2Int position, Board board)
        {
            return Rules.Move.RuleManager.GetPotentialMoves(position, _movement, board);
        }
    }
}