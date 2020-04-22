using System.Collections.Generic;
using UnityEngine;

namespace GameEngine.Pieces
{
    public class King : BasePiece
    {
        private readonly Vector3Int _movement = Vector3Int.one;
        public bool IsFirstMove = true;
        
        public King(Color color) : base(color)
        {
        }
        
        public override void BeingMoved()
        {
            base.BeingMoved();
            IsFirstMove = false;
        }
        
        public override List<Vector2Int> GetAvailableMoves(Vector2Int position, Board board)
        {
            return Rules.Move.RuleManager.GetPotentialMoves(position, _movement, board);
        }
        
        public override IPiece Copy()
        {
            return new King(GetColor());
        }
    }
}