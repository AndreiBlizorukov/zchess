using System;
using System.Collections.Generic;
using Pieces;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    public bool mIsKingAlive = true;
    public GameObject mPiecePrefab;
    private List<BasePiece> mWhitePieces;
    private List<BasePiece> mBlackPieces;

    private string[] mPieceOrder = {
        "P", "P", "P", "P", "P", "P", "P", "P",
        "R", "KN", "B", "Q", "K", "B", "KN", "R"
    };

    private Dictionary<string, Type> mPieceLibrary = new Dictionary<string, Type>()
    {
        {"P", typeof(Pawn)},
        {"R", typeof(Rook)},
        {"KN", typeof(Knight)},
        {"B", typeof(Bishop)},
        {"K", typeof(King)},
        {"Q", typeof(Queen)}
    };

    public void Setup(Board board)
    {
        mWhitePieces = CreatePieces(Color.white, new Color32(80, 124, 159, 255));
        mBlackPieces = CreatePieces(Color.black, new Color32(210, 95, 64, 255));

        PlacePieces(1, 0, mWhitePieces, board);
        PlacePieces(6, 7, mBlackPieces, board);

        SetInteractive(mWhitePieces, false);
        SetInteractive(mBlackPieces, false);
    }

    private List<BasePiece> CreatePieces(Color teamColor, Color32 spriteColor)
    {
        var pieces = new List<BasePiece>();
        for (var i = 0; i < mPieceOrder.Length; i++)
        {
            GameObject pieceObject = Instantiate(mPiecePrefab, transform, true);
            pieceObject.transform.localScale = new Vector3(1, 1, 1);
            pieceObject.transform.localRotation = Quaternion.identity;

            var pieceType = mPieceLibrary[mPieceOrder[i]];
            var piece = (BasePiece) pieceObject.AddComponent(pieceType);
            pieces.Add(piece);
            piece.Setup(teamColor, spriteColor, this);
        }

        return pieces;
    }

    private void PlacePieces(int pawnRow, int royaltyRow, List<BasePiece> pieces, Board board)
    {
        for (var i = 0; i < 8; i++)
        {
            pieces[i].Place(board.mAllCells[i, pawnRow]);
            pieces[i + 8].Place(board.mAllCells[i, royaltyRow]);
        }
    }

    public void SwitchSides(Color currentColorMove)
    {
        if (!mIsKingAlive)
        {
            /*ResetPieces();
                mIsKingAlive = true;
                */
        }

        bool isWhiteTurn = currentColorMove == Color.white;
        SetInteractive(mWhitePieces, !isWhiteTurn);
        SetInteractive(mBlackPieces, isWhiteTurn);
    }

    private void ResetPieces()
    {
        foreach (var piece in mWhitePieces)
        {
            piece.Reset();
        }
            
        foreach (var piece in mBlackPieces)
        {
            piece.Reset();
        }
    }

    private void SetInteractive(IEnumerable<BasePiece> pieces, bool value)
    {
        foreach (var piece in pieces)
        {
            piece.enabled = value;
        }
    }

    public void StartGame()
    {
        SetInteractive(mWhitePieces, true);
    }
}