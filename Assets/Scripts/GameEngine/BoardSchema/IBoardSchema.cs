using GameEngine.Pieces;

namespace GameEngine.BoardSchema
{
    public interface IBoardSchema
    {
        IPiece[,] CreatePieces();
    }
}