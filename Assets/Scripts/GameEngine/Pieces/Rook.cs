using System.Collections.Generic;
using UnityEngine;

namespace GameEngine.Pieces
{
    public class Rook : BasePiece
    {
        private readonly Vector3Int _movement = new Vector3Int(7, 7, 0);
        public Rook(Color color) : base(color)
        {
        }
        
        public override List<Vector2Int> GetAvailableMoves(Vector2Int position, Board board)
        {
            return Rules.Move.RuleManager.GetPotentialMoves(position, _movement, board);
        }

        public override IPiece Copy()
        {
            return new Rook(GetColor());
        }
    }
}