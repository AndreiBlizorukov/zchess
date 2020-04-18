using System;
using System.Collections.Generic;
using GameEngine.GameMode;
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

        public void Setup(IMode mode, Player.Human whitePlayer, Player.Human blackPlayer)
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
        /// <returns></returns>
        public List<Vector2Int> GetAvailableCells(Vector2Int position)
        {
            var piece = _board.mPieces[position.x, position.y];
            if (piece == null)
            {
                throw new Exception("It should be a piece");
            }

            var potentialMoves = piece.GetPotentialMoves(position);

            return potentialMoves;
        }
    }
}