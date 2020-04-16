using GameEngine.Pieces;

namespace GameEngine.GameMode
{
    public interface IMode
    {
        IPiece[,] CreatePieces();
    }
}