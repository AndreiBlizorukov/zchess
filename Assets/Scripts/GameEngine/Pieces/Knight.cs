using System.Collections.Generic;
using UnityEngine;

namespace GameEngine.Pieces
{
    public class Knight : BasePiece
    {
        public Knight(Color color) : base(color)
        {
        }

        public override List<Vector2Int> GetAvailableMoves(Vector2Int position, Board board)
        {
            return Rules.Move.Knight.GetPotentialMoves(position, board);
        }
    }
}