using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameEngine.Rules.Move
{
    public static class Knight
    {
        public static List<Vector2Int> GetPotentialMoves(Vector2Int position, Board board)
        {
            return new List<Vector2Int>
            {
                new Vector2Int(position.x - 2, position.y - 1),
                new Vector2Int(position.x - 2, position.y + 1),
                new Vector2Int(position.x - 1, position.y - 2),
                new Vector2Int(position.x - 1, position.y + 2),

                new Vector2Int(position.x + 1, position.y - 2),
                new Vector2Int(position.x + 1, position.y + 2),
                new Vector2Int(position.x + 2, position.y - 1),
                new Vector2Int(position.x + 2, position.y + 1),
            }.Where(target =>
            {
                var state = board.GetCellState(target, board.mPieces[position.x, position.y]);
                return state == CellState.Enemy || state == CellState.Free;
            }).ToList();
        }
    }
}