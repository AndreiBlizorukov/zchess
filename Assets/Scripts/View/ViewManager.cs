using System;
using System.Collections.Generic;
using GameEngine;
using GameEngine.Pieces;
using GameEngine.Player;
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
        public Text whiteTimer;
        public Text blackTimer;

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
            SetupTimer(GameEngine.mCurrentPlayer);
            SetupTimer(GameEngine.GetOppositePlayer());

            SetInteractive(engine.mCurrentPlayer.GetColor(), true);
            SetInteractive(engine.GetOppositePlayer().GetColor(), false);
        }

        private void SetupTimer(IPlayer player)
        {
            var timerText = GetTimer(player.GetColor());
            var minutes = ((int)player.GetTimer() / 60).ToString("00");
            var seconds = Math.Round(player.GetTimer() % 60).ToString("00");

            timerText.text = $"{minutes}:{seconds}";
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


        public void SetInteractive(Color color, bool value)
        {
            foreach (var piece in Pieces[color])
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
                        var rookCell = mBoard.mAllCells[7, currentCell.position.y];
                        var targetRookCell = mBoard.mAllCells[5, currentCell.position.y];
                        MovePiece(rookCell, targetRookCell);

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
                        var rookCell = mBoard.mAllCells[0, currentCell.position.y];
                        var targetRookCell = mBoard.mAllCells[3, currentCell.position.y];
                        MovePiece(rookCell, targetRookCell);
                        
                        return true;
                    }
                }
            }

            return false;
        }

        public void MovePiece(Cell current, Cell target)
        {
            var from = new Vector2Int
            {
                x = current.position.x,
                y = current.position.y
            };
            
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
            
            GameEngine.GetBoard()
                .MovePiece(from, new Vector2Int(target.position.x, target.position.y));
        }

        public bool CheckStopGame()
        {
            if (GameEngine.mState == Game.GameState.Checkmate)
            {
                GameStateText.GetComponent<Text>().text = $"checkmate, winner is {GameEngine._winner.GetColorText()} player";
                GameStateText.GetComponent<Text>().enabled = true;
                GameStateText.SetActive(true);
                return true;
            }

            if (GameEngine.mState == Game.GameState.Draw)
            {
                GameStateText.GetComponent<Text>().text = "draw";
                GameStateText.GetComponent<Text>().enabled = true;
                return true;
            }

            return false;
        }

        public Text GetTimer(Color color)
        {
            return color == Color.white
                ? whiteTimer
                : blackTimer;
        }
    }
}