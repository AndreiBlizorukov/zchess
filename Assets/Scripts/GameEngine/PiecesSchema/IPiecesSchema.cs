using GameEngine.Pieces;

namespace GameEngine.PiecesSchema
{
    public interface IPiecesSchema
    {
        IPiece[,] CreatePieces();
    }
}