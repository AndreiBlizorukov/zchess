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

        public GameState mState = GameState.None;

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
            GetGameState();
        }

        private void GetGameState()
        {
            var myPiecesPositions = _board.GetPiecesPositions(mCurrentPlayer.GetColor()).ToList();
            var enemyPiecesPositions = _board.GetPiecesPositions(GetOppositePlayer().GetColor()).ToList();

            // if it's only 2 pieces and those both are kings
            if (myPiecesPositions.Count == 1 && enemyPiecesPositions.Count == 1)
            {
                var myPiecePosition = myPiecesPositions.First();
                var enemyPosition = enemyPiecesPositions.First();

                if (_board.mPieces[myPiecePosition.x, myPiecePosition.y].GetType() == typeof(King)
                    && _board.mPieces[enemyPosition.x, enemyPosition.y].GetType() == typeof(King))
                {
                    mState = GameState.Draw;
                    return;
                }
            }

            // if current player have places to move
            foreach (var myPiecesPosition in myPiecesPositions)
            {
                var myPiece = _board.mPieces[myPiecesPosition.x, myPiecesPosition.y];
                var moves = FilterCheckmateMoves(myPiecesPosition, myPiece.GetAvailableMoves(myPiecesPosition, _board));

                // if current player have moves
                if (moves.Any())
                {
                    mState = GameState.None;
                    return;
                }
            }

            // current player can't move and the king is under attack
            var enemyPieces = _board.GetPiecesPositions(GetOppositePlayer().GetColor());
            foreach (var enemyPiecePosition in enemyPieces)
            {
                var enemyPiece = _board.mPieces[enemyPiecePosition.x, enemyPiecePosition.y];
                var enemyMoves = enemyPiece.GetAvailableMoves(enemyPiecePosition, _board);
                foreach (var enemyMove in enemyMoves)
                {
                    var enemyMovePiece = _board.mPieces[enemyMove.x, enemyMove.y];
                    if (enemyMovePiece != null)
                    {
                        if (enemyMovePiece.GetType() == typeof(King))
                        {
                            mState = GameState.Checkmate;
                            return;
                        }
                    }
                }
            }
            
            // current player can't move but king is not under attack
            mState = GameState.Draw;
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

        public enum GameState
        {
            None,
            Draw,
            Checkmate
        }
    }
}