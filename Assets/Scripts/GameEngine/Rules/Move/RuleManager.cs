using System.Collections.Generic;
using UnityEngine;

namespace GameEngine.Rules.Move
{
    public static class RuleManager
    {
        public static List<Vector2Int> GetPotentialMoves(Vector2Int position, Vector3Int movement, Board board)
        {
            // horizontal
            var cells = CreateCellPath(position, new Vector2Int(1, 0), movement.x, board);
            cells.AddRange(CreateCellPath(position, new Vector2Int(-1, 0), movement.x, board));

            // vertical
            cells.AddRange(CreateCellPath(position, new Vector2Int(0, 1), movement.y, board));
            cells.AddRange(CreateCellPath(position, new Vector2Int(0, -1), movement.y, board));

            // upper horizontal
            cells.AddRange(CreateCellPath(position, new Vector2Int(1, 1), movement.z, board));
            cells.AddRange(CreateCellPath(position, new Vector2Int(-1, 1), movement.z, board));

            // lower horizontal
            cells.AddRange(CreateCellPath(position, new Vector2Int(-1, -1), movement.z, board));
            cells.AddRange(CreateCellPath(position, new Vector2Int(1, -1), movement.z, board));

            return cells;
        }

        private static List<Vector2Int> CreateCellPath(Vector2Int position, Vector2Int deltaPosition, int movement, Board board)
        {
            int currentX = position.x;
            int currentY = position.y;

            var path = new List<Vector2Int>();
            for (int i = 1; i <= movement; i++)
            {
                currentX += deltaPosition.x;
                currentY += deltaPosition.y;

                path.Add(new Vector2Int(currentX, currentY));
            }

            return board.GetValidatedMoves(path, board.mPieces[position.x, position.y]);
        }
    }
}