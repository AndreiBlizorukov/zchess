using System;
using System.Collections.Generic;
using GameEngine;
using GameEngine.Pieces;
using UnityEngine;

namespace View
{
    public class ViewManager : MonoBehaviour
    {
        public Board mBoard;
        public GameObject mPiecePrefab;
        public static Game _gameEngine;

        public Dictionary<Color, List<View.Pieces.BasePiece>> Pieces =
            new Dictionary<Color, List<View.Pieces.BasePiece>>
            {
                {Color.white, new List<View.Pieces.BasePiece>()},
                {Color.black, new List<View.Pieces.BasePiece>()},
            };

        private Dictionary<Type, Type> _modelMapping = new Dictionary<Type, Type>
        {
            {typeof(Pawn), typeof(View.Pieces.Pawn)},
            {typeof(Rook), typeof(View.Pieces.Rook)},
            {typeof(Knight), typeof(View.Pieces.Knight)},
            {typeof(Bishop), typeof(View.Pieces.Bishop)},
            {typeof(King), typeof(View.Pieces.King)},
            {typeof(Queen), typeof(View.Pieces.Queen)},
        };

        public void Setup(Game engine)
        {
            _gameEngine = engine;
            RenderPieces(_gameEngine.GetBoard().mPieces);

            SetInteractive(Pieces[engine.mCurrentPlayer.GetColor()], true);
            SetInteractive(Pieces[engine.GetOppositePlayer().GetColor()], false);
        }

        private void RenderPieces(IPiece[,] pieces)
        {
            for (var x = 0; x < 8; x++)
            {
                for (var y = 0; y < 8; y++)
                {
                    var piece = pieces[x, y];
                    if (piece != null)
                    {
                        var viewedPiece = CreatePiece(piece);
                        Pieces[piece.GetColor()].Add(viewedPiece);
                        mBoard.mAllCells[x, y].mCurrentPiece = viewedPiece;
                        viewedPiece.Place(mBoard.mAllCells[x, y]);
                    }
                }
            }
        }

        private View.Pieces.BasePiece CreatePiece(IPiece piece)
        {
            var pieceObject = Instantiate(mPiecePrefab, transform, true);
            pieceObject.transform.localScale = new Vector3(1, 1, 1);
            pieceObject.transform.localRotation = Quaternion.identity;

            var pieceType = _modelMapping[piece.GetType()];
            var pieceView = (View.Pieces.BasePiece) pieceObject.AddComponent(pieceType);
            pieceView.viewManager = this;
            pieceView.Setup(piece.GetColor(), ColorSchema.PieceColors[piece.GetColor()]);

            return pieceView;
        }


        private void SetInteractive(IEnumerable<View.Pieces.BasePiece> pieces, bool value)
        {
            foreach (var piece in pieces)
            {
                piece.enabled = value;
            }
        }

        public void CreateBoard()
        {
            mBoard.Create();
        }

        public void HighLightCells(Cell currentCell)
        {
            var highlightedPositions = _gameEngine
                .GetAvailableCells(currentCell.position);

            foreach (var highlightedPosition in highlightedPositions)
            {
                mBoard.mAllCells[highlightedPosition.x, highlightedPosition.y].mOutlineImage.enabled = true;
            }
        }

        public void ClearHighLightedCells()
        {
            foreach (var cell in mBoard.mAllCells)
            {
                cell.mOutlineImage.enabled = false;
            }
        }

        public bool MovePiece(Cell currentCell, Vector3 mousePosition)
        {
            var from = new Vector2Int
            {
                x = currentCell.position.x,
                y = currentCell.position.y
            };

            var highlightedPositions = _gameEngine
                .GetAvailableCells(from);

            foreach (var position in highlightedPositions)
            {
                var targetCell = mBoard.mAllCells[position.x, position.y];
                if (RectTransformUtility.RectangleContainsScreenPoint(targetCell.mRectTransform, mousePosition))
                {
                    if (targetCell.mCurrentPiece != null)
                    {
                        targetCell.mCurrentPiece.mCurrentCell = null;
                        targetCell.mCurrentPiece.gameObject.SetActive(false);
                    }

                    var currentPiece = currentCell.mCurrentPiece;
                    currentCell.mCurrentPiece = null;
                    currentPiece.mCurrentCell = targetCell;
                    targetCell.mCurrentPiece = currentPiece;
                    targetCell.mCurrentPiece.mCurrentCell = targetCell;
                    targetCell.mCurrentPiece.transform.position = targetCell.transform.position;


                    _gameEngine.GetBoard()
                        .MovePiece(from, new Vector2Int(targetCell.position.x, targetCell.position.y));
                    EndOfTurn();
                    return true;
                }
            }


            return false;
        }

        public void EndOfTurn()
        {
            SetInteractive(Pieces[_gameEngine.mCurrentPlayer.GetColor()], false);
            _gameEngine.TogglePlayer();
            SetInteractive(Pieces[_gameEngine.mCurrentPlayer.GetColor()], true);
        }
    }
}