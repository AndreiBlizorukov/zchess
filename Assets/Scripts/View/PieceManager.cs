using System;
using System.Collections.Generic;
using GameEngine;
using GameEngine.Pieces;
using UnityEngine;

namespace View
{
    public class PieceManager : MonoBehaviour
    {
        public Board mBoard;
        public GameObject mPiecePrefab;

        public Dictionary<Color, List<View.Pieces.BasePiece>> Pieces = new Dictionary<Color, List<View.Pieces.BasePiece>>
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
            RenderPieces(engine.GetBoard().mPieces);

            SetInteractive(Pieces[Color.white], true);
            SetInteractive(Pieces[Color.black], false);
        }

        private void RenderPieces(IPiece[,] pieces)
        {
            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    var piece = pieces[x, y];
                    if (piece != null)
                    {
                        var viewedPiece = CreatePiece(piece);
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
    }
}