using System.Collections.Generic;
using System.Linq;
using GameEngine.Pieces;
using UnityEngine;

namespace GameEngine.Rules
{
    public class Castling
    {
        public static List<Vector2Int> GetShortMoves(Vector2Int kingPosition, Game game)
        {
            var moves = new List<Vector2Int>();
            for (var i = kingPosition.x + 1; i < 7; i++)
            {
                if (game.GetBoard().mPieces[i, kingPosition.y] != null)
                {
                    return moves;
                }
            }

            var enemyColor = game.GetOppositePlayer().GetColor();
            var enemyPiecePositions = game.GetBoard().GetPiecesPositions(enemyColor);

            var veryRightPiece = game.GetBoard().mPieces[7, kingPosition.y];
            if (veryRightPiece != null && typeof(Rook) == veryRightPiece.GetType())
            {
                var rightRook = (Rook) game.GetBoard().mPieces[7, kingPosition.y];
                if (rightRook.IsFirstMove)
                {
                    var results = enemyPiecePositions.Where(enemyPiecePosition =>
                    {
                        var enemyMoves = game.GetAvailableCells(enemyPiecePosition);
                        for (var i = kingPosition.x + 1; i < 8; i++)
                        {
                            if (enemyMoves.Any(o => o.x == i && o.y == kingPosition.y))
                            {
                                return true;
                            }
                        }

                        return false;
                    });

                    if (!results.Any())
                    {
                        moves.Add(new Vector2Int(kingPosition.x + 2, kingPosition.y));
                    }
                }
            }

            return moves;
        }

        public static List<Vector2Int> GetLongMoves(Vector2Int kingPosition, Game game)
        {
            var moves = new List<Vector2Int>();
            for (var i = 1; i < kingPosition.x; i++)
            {
                if (game.GetBoard().mPieces[i, kingPosition.y] != null)
                {
                    return moves;
                }
            }

            var enemyColor = game.GetOppositePlayer().GetColor();
            var enemyPiecePositions = game.GetBoard().GetPiecesPositions(enemyColor);

            var veryLeftPiece = game.GetBoard().mPieces[0, kingPosition.y];
            if (veryLeftPiece != null && typeof(Rook) == veryLeftPiece.GetType())
            {
                var leftRook = (Rook) veryLeftPiece;
                if (leftRook.IsFirstMove)
                {
                    var results = enemyPiecePositions.Where(enemyPiecePosition =>
                    {
                        var enemyMoves = game.GetAvailableCells(enemyPiecePosition);
                        for (var i = 2; i < kingPosition.x; i++)
                        {
                            if (enemyMoves.Any(o => o.x == i && o.y == kingPosition.y))
                            {
                                return true;
                            }
                        }

                        return false;
                    });

                    if (!results.Any())
                    {
                        moves.Add(new Vector2Int(kingPosition.x - 2, kingPosition.y));
                    }
                }
            }

            return moves;
        }

        private static bool CastlingAvailable(Vector2Int kingPosition, Game game)
        {
            var board = game.GetBoard();
            var piece = board.mPieces[kingPosition.x, kingPosition.y];
            if (piece == null || typeof(King) != piece.GetType())
            {
                return false;
            }

            var king = (King) piece;
            if (!king.IsFirstMove)
            {
                return false;
            }

            var enemyColor = game.GetOppositePlayer().GetColor();
            var enemyPiecePositions = game.GetBoard().GetPiecesPositions(enemyColor);
            foreach (var enemyPiecePosition in enemyPiecePositions)
            {
                var enemyMoves = game.GetAvailableCells(enemyPiecePosition);
                if (enemyMoves.Any(o => o == kingPosition))
                {
                    return false;
                }
            }

            return true;
        }


        public static List<Vector2Int> GetMoves(Vector2Int kingPosition, Game game)
        {
            var moves = new List<Vector2Int>();
            if (!CastlingAvailable(kingPosition, game)) return moves;

            moves.AddRange(GetShortMoves(kingPosition, game));
            moves.AddRange(GetLongMoves(kingPosition, game));

            return moves;
        }
    }
}