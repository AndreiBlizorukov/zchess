using GameEngine.Pieces;
using static UnityEngine.Color;

namespace GameEngine.BoardSchema
{
    public class Test : IBoardSchema
    {
        public IPiece[,] CreatePieces()
        {
            return new IPiece[,]
            {
                {null, null, null, null, new King(black), null, null, null},
                {null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null},
                {new Rook(white), null, null, null, new King(white), null, new Knight(white), new Rook(white)},
            };
        }
    }
}