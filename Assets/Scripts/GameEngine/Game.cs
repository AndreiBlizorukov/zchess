using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Mode;
using GameEngine.Pieces;
using GameEngine.Player;
using UnityEngine;

namespace GameEngine
{
    public class Game
    {
        private Board _board;
        private IPlayer _whitePlayer;
        private IPlayer _blackPlayer;
        public IPlayer mCurrentPlayer;
        public IPlayer _winner;
        private static Game _game;

        private Game()
        {
        }

        public static Game GetInstance()
        {
            if (_game == null)
            {
                _game = new Game();
            }

            return _game;
        }

        public GameState mState = GameState.None;

        public void Setup(IMode mode)
        {
            _board = new Board();
            _whitePlayer = mode.GetWhitePlayer();
            _blackPlayer = mode.GetBlackPlayer();
            mCurrentPlayer = _whitePlayer;

            var pieces =  mode.GetPositions().CreatePieces();
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

        public void GetGameState()
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
                    SetState(GameState.Draw);
                    return;
                }
            }

            // if enemy player have places to move
            foreach (var enemyPiecesPosition in enemyPiecesPositions)
            {
                var myPiece = _board.mPieces[enemyPiecesPosition.x, enemyPiecesPosition.y];
                var moves = FilterCheckmateMoves(enemyPiecesPosition, myPiece.GetAvailableMoves(enemyPiecesPosition, _board));

                // if enemy player have moves
                if (moves.Any())
                {
                    SetState(GameState.None);
                    return;
                }
            }

            // current player can't move and the king is under attack
            var myPiecePositions = _board.GetPiecesPositions(mCurrentPlayer.GetColor());
            foreach (var myPiecePosition in myPiecePositions)
            {
                var myPiece = _board.mPieces[myPiecePosition.x, myPiecePosition.y];
                var myMoves = myPiece.GetAvailableMoves(myPiecePosition, _board);
                foreach (var myMove in myMoves)
                {
                    var myMovePiece = _board.mPieces[myMove.x, myMove.y];
                    if (myMovePiece != null)
                    {
                        if (myMovePiece.GetType() == typeof(King))
                        {
                            SetState(GameState.Checkmate, mCurrentPlayer);
                            return;
                        }
                    }
                }
            }
            
            // current player can't move but king is not under attack
            SetState(GameState.Draw);
        }

        public IPlayer GetOppositePlayer()
        {
            var nextPlayer = _whitePlayer == mCurrentPlayer
                ? _blackPlayer
                : _whitePlayer;

            return nextPlayer;
        }

        public Color GetOppositeColor(Color color)
        {
            return color == Color.white
                ? Color.black
                : Color.white;
        }

        /// <summary>
        /// List of positions where its possible to move
        /// </summary>
        /// <param name="position">schema for piece what we're trying to move </param>
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
            var currentPiece = _board.mPieces[position.x, position.y];
            return moves.Where(move =>
            {
                var newBoard = _board.Copy();
                newBoard.MovePiece(position, move);

                var enemyPiecePositions = newBoard.GetPiecesPositions(GetOppositeColor(currentPiece.GetColor()));
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

        public void SetState(GameState state, IPlayer winner = null)
        {
            mState = state;
            if (winner != null)
            {
                _winner = winner;
            }
        }

        public enum GameState
        {
            None,
            Draw,
            Checkmate
        }
    }
}