using System.Collections.Generic;
using UnityEngine;

namespace GameEngine.Rules.Move
{
    public static class Pawn
    {
        public static List<Vector2Int> GetPotentialMoves(Vector2Int position, Vector2Int direction, bool isFirstMove, Board board)
        {
            var moves = new List<Vector2Int>();
            for (int i = 1; i <= 2; i++)
            {
                var targetDirection = new Vector2Int(position.x, position.y + direction.y * i);
                var targetState = board.GetCellState(targetDirection, board.mPieces[position.x, position.y]);
                if (targetState == CellState.Free)
                {
                    moves.Add(targetDirection);
                }

                if (!isFirstMove)
                {
                    break;
                }
            }

            foreach (var attack in new[]{-1, 1})
            {
                var targetDirection = new Vector2Int(position.x - attack, position.y + direction.y);
                var targetState = board.GetCellState(targetDirection, board.mPieces[position.x, position.y]);
                if (targetState == CellState.Enemy)
                {
                    moves.Add(targetDirection);
                }
            }
            
            return moves;
        }
    }
}