using System.Collections.Generic;
using UnityEngine;

namespace GameEngine.Pieces
{
    public class Bishop : BasePiece
    {
        private readonly Vector3Int _movement = new Vector3Int(0, 0, 7);
        public Bishop(Color color) : base(color)
        {
        }
        
        public override List<Vector2Int> GetAvailableMoves(Vector2Int position, Board board)
        {
            return Rules.Move.RuleManager.GetPotentialMoves(position, _movement, board);
        }

        public override IPiece Copy()
        {
            return new Bishop(GetColor());
        }
    }
}