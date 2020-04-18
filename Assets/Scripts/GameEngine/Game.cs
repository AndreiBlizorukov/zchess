using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.GameMode;
using GameEngine.Pieces;
using GameEngine.Player;
using UnityEngine;

namespace GameEngine
{
    public class Game
    {
        private Board _board;
        private IMode _mode;
        private IPlayer _whitePlayer;
        private IPlayer _blackPlayer;
        public IPlayer mCurrentPlayer;

        public void Setup(IMode mode, IPlayer whitePlayer, IPlayer blackPlayer)
        {
            _board = new Board();
            _mode = mode;
            _whitePlayer = whitePlayer;
            _blackPlayer = blackPlayer;
            mCurrentPlayer = whitePlayer;

            var pieces = _mode.CreatePieces();
            _board.PlacePieces(pieces);
        }

        public Board GetBoard()
        {
            return _board;
        }

        public void TogglePlayer()
        {
            mCurrentPlayer = GetOppositePlayer();
        }

        public IPlayer GetOppositePlayer()
        {
            var nextPlayer = _whitePlayer == mCurrentPlayer
                ? _blackPlayer
                : _whitePlayer;

            return nextPlayer;
        }

        /// <summary>
        /// List of positions where its possible to move
        /// </summary>
        /// <param name="position">position for piece what we're trying to move </param>
        /// <param name="board"></param>
        /// <returns></returns>
        public List<Vector2Int> GetAvailableCells(Vector2Int position)
        {
            var piece = _board.mPieces[position.x, position.y];
            if (piece == null)
            {
                throw new Exception("It should be a piece");
            }

            var moves = piece.GetAvailableMoves(position, _board);
            return FilterCheckmateMoves(position, moves);
        }

        private List<Vector2Int> FilterCheckmateMoves(Vector2Int position, List<Vector2Int> moves)
        {
            return moves.Where(move =>
            {
                var newBoard = _board.Copy();
                newBoard.MovePiece(position, move);
                
                var enemyPiecePositions = newBoard.GetPiecesPositions(GetOppositePlayer().GetColor());
                foreach (var enemyPiecePosition in enemyPiecePositions)
                {
                    var enemyPiece = newBoard.mPieces[enemyPiecePosition.x, enemyPiecePosition.y];
                    var enemyMoves = enemyPiece.GetAvailableMoves(enemyPiecePosition, newBoard);
                    foreach (var enemyMove in enemyMoves)
                    {
                        // trying to find the figure which is under attack
                        var attackedPiece = newBoard.mPieces[enemyMove.x, enemyMove.y];
                        if (attackedPiece != null)
                        {
                            if (attackedPiece.GetType() == typeof(King))
                            {
                                return false;
                            }
                        }
                    }
                }

                return true;
            }).ToList();
        }
    }
}