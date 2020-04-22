using System;
using System.Collections.Generic;
using GameEngine;
using GameEngine.Pieces;
using GameEngine.Rules;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class ViewManager : MonoBehaviour
    {
        public Board mBoard;
        public GameObject mPiecePrefab;
        public GameObject GameStateText;
        public Game GameEngine;

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
            GameEngine = engine;
            RenderPieces(GameEngine.GetBoard().mPieces);

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
            foreach (var highlightedPosition in GameEngine.GetAvailableCells(currentCell.position))
            {
                HighLightCell(highlightedPosition);
            }

            if (currentCell.mCurrentPiece != null && typeof(View.Pieces.King) == currentCell.mCurrentPiece.GetType())
            {
                var moves = Castling.GetMoves(currentCell.position, GameEngine);
                foreach (var move in moves)
                {
                    HighLightCell(move);
                }
            }
        }

        private void HighLightCell(Vector2Int position)
        {
            mBoard.mAllCells[position.x, position.y].mOutlineImage.enabled = true;
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

            var availableCells = GameEngine.GetAvailableCells(from);
            foreach (var position in availableCells)
            {
                var targetCell = mBoard.mAllCells[position.x, position.y];
                if (RectTransformUtility.RectangleContainsScreenPoint(targetCell.mRectTransform, mousePosition))
                {
                    MovePiece(currentCell, targetCell);
                    GameEngine.GetBoard()
                        .MovePiece(from, new Vector2Int(targetCell.position.x, targetCell.position.y));
                    
                    return true;
                }
            }

            var piece = GameEngine.GetBoard().mPieces[from.x, from.y];
            if (piece.GetType() == typeof(King))
            {
                var shortCastlingMoves = Castling.GetShortMoves(currentCell.position, GameEngine);
                foreach (var castlingMove in shortCastlingMoves)
                {
                    var targetCell = mBoard.mAllCells[castlingMove.x, castlingMove.y];
                    if (RectTransformUtility.RectangleContainsScreenPoint(targetCell.mRectTransform, mousePosition))
                    {
                        MovePiece(currentCell, targetCell);
                        GameEngine.GetBoard()
                            .MovePiece(from, new Vector2Int(targetCell.position.x, targetCell.position.y));

                        var rookCell = mBoard.mAllCells[7, currentCell.position.y];
                        var targetRookCell = mBoard.mAllCells[5, currentCell.position.y];
                        MovePiece(rookCell, targetRookCell);

                        GameEngine.GetBoard()
                            .MovePiece(rookCell.position, new Vector2Int(targetRookCell.position.x, targetRookCell.position.y));

                        return true;
                    }
                }

                
                var longCastlingMoves = Castling.GetLongMoves(currentCell.position, GameEngine);
                foreach (var castlingMove in longCastlingMoves)
                {
                    var targetCell = mBoard.mAllCells[castlingMove.x, castlingMove.y];
                    if (RectTransformUtility.RectangleContainsScreenPoint(targetCell.mRectTransform, mousePosition))
                    {
                        MovePiece(currentCell, targetCell);
                        GameEngine.GetBoard()
                            .MovePiece(from, new Vector2Int(targetCell.position.x, targetCell.position.y));
                        
                        var rookCell = mBoard.mAllCells[0, currentCell.position.y];
                        var targetRookCell = mBoard.mAllCells[3, currentCell.position.y];
                        MovePiece(rookCell, targetRookCell);
                        GameEngine.GetBoard()
                            .MovePiece(rookCell.position, new Vector2Int(targetRookCell.position.x, targetRookCell.position.y));
                        
                        return true;
                    }
                }
            }

            return false;
        }

        private void MovePiece(Cell current, Cell target)
        {
            if (target.mCurrentPiece != null)
            {
                target.mCurrentPiece.mCurrentCell = null;
                target.mCurrentPiece.gameObject.SetActive(false);
            }

            var currentPiece = current.mCurrentPiece;
            current.mCurrentPiece = null;
            currentPiece.mCurrentCell = target;
            target.mCurrentPiece = currentPiece;
            target.mCurrentPiece.mCurrentCell = target;
            target.mCurrentPiece.transform.position = target.transform.position;
        }

        public void EndOfTurn()
        {
            SetInteractive(Pieces[GameEngine.mCurrentPlayer.GetColor()], false);
            GameEngine.TogglePlayer();

            if (GameEngine.mState == Game.GameState.Checkmate)
            {
                GameStateText.GetComponent<Text>().text = $"checkmate, winner is {GameEngine.GetOppositePlayer().GetColorText()} player";
                GameStateText.GetComponent<Text>().enabled = true;
                GameStateText.SetActive(true);
                return;
            }

            if (GameEngine.mState == Game.GameState.Draw)
            {
                GameStateText.GetComponent<Text>().text = "draw";
                GameStateText.GetComponent<Text>().enabled = true;
                
                return;
            }
            
            SetInteractive(Pieces[GameEngine.mCurrentPlayer.GetColor()], true);
        }
    }
}