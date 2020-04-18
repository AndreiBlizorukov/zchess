using System.Collections.Generic;
using UnityEngine;

namespace GameEngine.Rules.Move
{
    public static class Pawn
    {
        public static List<Vector2Int> GetPotentialMoves(Vector2Int position, Vector2Int direction, bool isFirstMove)
        {
            var moves = new List<Vector2Int>
            {
                new Vector2Int
                {
                    x = position.x,
                    y = position.y + direction.y
                }
            };

            if (isFirstMove)
            {
                moves.Add(
                    new Vector2Int
                    {
                        x = position.x,
                        y = position.y + direction.y * 2
                    }
                );
            }
            
            return moves;
        }
    }
}